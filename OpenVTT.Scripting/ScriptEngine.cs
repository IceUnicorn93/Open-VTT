using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using OpenVTT.Common;
using OpenVTT.Session;
using OpenVTT.Settings;
using OpenVTT.StreamDeck;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenVTT.Scripting
{
    static internal class ScriptEngine
    {
        static List<string> SubDirectorys = new List<string>();
        static internal List<ScriptHost> CalculatedHosts = new List<ScriptHost>();

        static internal bool Calculated = false;
        static internal Action HostsCalculated;

        static ScriptEngine()
        {
            CleanUnusedFolders();

            if (!Directory.Exists(Path.Combine(".", "Scripts")))
            {
                Directory.CreateDirectory(Path.Combine(".", "Scripts"));
                Directory.CreateDirectory(Path.Combine(".", "Scripts", "_DLL References"));
            }

            if (!Directory.Exists(Path.Combine(".", "Scripts", "_Sample Script")))
            {
                Directory.CreateDirectory(Path.Combine(".", "Scripts", "_Sample Script"));

                File.WriteAllText(Path.Combine(".", "Scripts", "_Sample Script", "Main.cs"), GetDefaultScript());
                ScriptConfig.Save(Path.Combine(".", "Scripts", "_Sample Script", "ScriptConfig.xml"));
            }

            CreateDocumentation();
        }

        static private string GetDefaultScript()
        {
            return
            @"Page.Text = Config.Name;

Page.Size = new Size(500, 500);
var pageSize = Page.Size;

var lblAuthor = new Label();
lblAuthor.Name = ""lblAuthor"";
lblAuthor.Text = $""Made By; {Config.Author}"";
lblAuthor.Location = new Point(pageSize.Width - 200, pageSize.Height - 200);
lblAuthor.Size = new Size(195, lblAuthor.Size.Height);
lblAuthor.Anchor = (AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right));

var lblNameAndVersion = new Label();
lblNameAndVersion.Name = ""lblNameAndVersion"";
lblNameAndVersion.Text = $""{Config.Name} | {Config.Version}"";
lblNameAndVersion.Location = new Point(pageSize.Width - 200, pageSize.Height - 170);
lblNameAndVersion.Size = new Size(195, lblNameAndVersion.Size.Height);
lblNameAndVersion.Anchor = (AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right));

var tbDescription = new TextBox();
tbDescription.Name = ""tbDescription"";
tbDescription.Text = Config.Description;
tbDescription.Multiline = true;
tbDescription.Location = new Point(pageSize.Width - 200, pageSize.Height - 140);
tbDescription.Size = new Size(195, 135);
tbDescription.Anchor = (AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right));
tbDescription.ReadOnly = true;

Page.Controls.Add(lblAuthor);
Page.Controls.Add(lblNameAndVersion);
Page.Controls.Add(tbDescription);";
        }

        static private void CleanUnusedFolders()
        {
            if (Directory.Exists(Path.Combine(".", "cs"))) Directory.Delete(Path.Combine(".", "cs"), true);
            if (Directory.Exists(Path.Combine(".", "de"))) Directory.Delete(Path.Combine(".", "de"), true);
            if (Directory.Exists(Path.Combine(".", "es"))) Directory.Delete(Path.Combine(".", "es"), true);
            if (Directory.Exists(Path.Combine(".", "fr"))) Directory.Delete(Path.Combine(".", "fr"), true);
            if (Directory.Exists(Path.Combine(".", "it"))) Directory.Delete(Path.Combine(".", "it"), true);
            if (Directory.Exists(Path.Combine(".", "ja"))) Directory.Delete(Path.Combine(".", "ja"), true);
            if (Directory.Exists(Path.Combine(".", "ko"))) Directory.Delete(Path.Combine(".", "ko"), true);
            if (Directory.Exists(Path.Combine(".", "pl"))) Directory.Delete(Path.Combine(".", "pl"), true);
            if (Directory.Exists(Path.Combine(".", "pt-BR"))) Directory.Delete(Path.Combine(".", "pt-BR"), true);
            if (Directory.Exists(Path.Combine(".", "ru"))) Directory.Delete(Path.Combine(".", "ru"), true);
            if (Directory.Exists(Path.Combine(".", "tr"))) Directory.Delete(Path.Combine(".", "tr"), true);
            if (Directory.Exists(Path.Combine(".", "zh-Hans"))) Directory.Delete(Path.Combine(".", "zh-Hans"), true);
            if (Directory.Exists(Path.Combine(".", "zh-Hant"))) Directory.Delete(Path.Combine(".", "zh-Hant"), true);
        }

        static private void CreateDocumentation()
        {
            string text = "";

            var list = new List<Type>()
            {
                typeof(CustomObject), typeof(CustomObjectData), typeof(CustomSettings),
                typeof(DisplayType), typeof(FogOfWar.FogOfWar), typeof(FogState), typeof(Layer), typeof(Scene), typeof(ScreenInformation),
                typeof(ScriptConfig), typeof(ScriptHost), typeof(Session.Session), typeof(Settings.Settings), typeof(StreamDeckStatics),
                typeof(XmlColor)
            };

            foreach (var item in list)
                text += GetDocumentationForType(item);

            File.WriteAllText(Path.Combine(".", "Scripts", "Documentation.txt"), text);
        }

        static private string GetDocumentationForType(Type type)
        {
            Documentation documentation = (Documentation)Attribute.GetCustomAttribute(type, typeof(Documentation));
            var ret = "";
            ret += $"{documentation?.Name ?? ""}";
            ret += Environment.NewLine;
            ret += $"{documentation?.Description ?? ""}";
            ret += Environment.NewLine;

            var docuList = new List<Documentation>();
            foreach (var item in type.GetMembers()) docuList.Add((Documentation)Attribute.GetCustomAttribute(item, typeof(Documentation)));

            var nonStaticFields = docuList.Where(d => d != null && d.IsStatic == false && d.IsField).OrderBy(d => d.DataType).ThenBy(d => d.Name).ToList();
            var StaticFields = docuList.Where(d => d != null && d.IsStatic == true && d.IsField).OrderBy(d => d.DataType).ThenBy(d => d.Name).ToList();
            var nonStaticProperys = docuList.Where(d => d != null && d.IsStatic == false && d.IsProperty).OrderBy(d => d.DataType).ThenBy(d => d.Name).ToList();
            var StaticProperys = docuList.Where(d => d != null && d.IsStatic == true && d.IsProperty).OrderBy(d => d.DataType).ThenBy(d => d.Name).ToList();
            var nonStaticMethods = docuList.Where(d => d != null && d.IsStatic == false && d.IsMethod).OrderBy(d => d.DataType).ThenBy(d => d.Name).ToList();
            var StaticMethods = docuList.Where(d => d != null && d.IsStatic == true && d.IsMethod).OrderBy(d => d.DataType).ThenBy(d => d.Name).ToList();

            if(nonStaticFields.Any())
            { 
                ret += "Non-Static Fields";
                ret += Environment.NewLine;
                
                var maxLengthDataType = nonStaticFields.Max(n => n.DataType.Length);
                var maxLengthName = nonStaticFields.Max(n => n.Name.Length);
                var maxLengthDescription = nonStaticFields.Max(n => n.Description.Length);
                var header = new string('-', maxLengthDataType) + "-|-" + new string('-', maxLengthName) + "-|-" + new string('-', maxLengthDescription);

                ret += header;
                ret += Environment.NewLine;

                foreach (var docu in nonStaticFields)
                {
                    var name = (docu.Name + new string(' ', maxLengthName)).Substring(0, maxLengthName);
                    var description = (docu.Description + new string(' ', maxLengthDescription)).Substring(0, maxLengthDescription);
                    var dataType = (docu.DataType + new string(' ', maxLengthDataType)).Substring(0, maxLengthDataType);
                    ret += $"{dataType} | {name} | {description}";
                    ret += Environment.NewLine;
                }
            }
            if(StaticFields.Any())
            {
                if(nonStaticFields.Any()) ret += Environment.NewLine;
                ret += "Static Fields";
                ret += Environment.NewLine;

                var maxLengthDataType = StaticFields.Max(n => n.DataType.Length);
                var maxLengthName = StaticFields.Max(n => n.Name.Length);
                var maxLengthDescription = StaticFields.Max(n => n.Description.Length);
                var header = new string('-', maxLengthDataType) + "-|-" + new string('-', maxLengthName) + "-|-" + new string('-', maxLengthDescription);

                ret += header;
                ret += Environment.NewLine;

                foreach (var docu in StaticFields)
                {
                    var name = (docu.Name + new string(' ', maxLengthName)).Substring(0, maxLengthName);
                    var description = (docu.Description + new string(' ', maxLengthDescription)).Substring(0, maxLengthDescription);
                    var dataType = (docu.DataType + new string(' ', maxLengthDataType)).Substring(0, maxLengthDataType);
                    ret += $"{dataType} | {name} | {description}";
                    ret += Environment.NewLine;
                }
            }
            if(nonStaticProperys.Any())
            {
                if (nonStaticFields.Any() || StaticFields.Any()) ret += Environment.NewLine;
                ret += "Non-Static Propertys";
                ret += Environment.NewLine;

                var maxLengthDataType = nonStaticProperys.Max(n => n.DataType.Length);
                var maxLengthName = nonStaticProperys.Max(n => n.Name.Length);
                var maxLengthDescription = nonStaticProperys.Max(n => n.Description.Length);
                var header = new string('-', maxLengthDataType) + "-|-" + new string('-', maxLengthName) + "-|-" + new string('-', maxLengthDescription);

                ret += header;
                ret += Environment.NewLine;

                foreach (var docu in nonStaticProperys)
                {
                    var name = (docu.Name + new string(' ', maxLengthName)).Substring(0, maxLengthName);
                    var description = (docu.Description + new string(' ', maxLengthDescription)).Substring(0, maxLengthDescription);
                    var dataType = (docu.DataType + new string(' ', maxLengthDataType)).Substring(0, maxLengthDataType);
                    ret += $"{dataType} | {name} | {description}";
                    ret += Environment.NewLine;
                }
            }
            if(StaticProperys.Any())
            {
                if (nonStaticFields.Any() || StaticFields.Any() || nonStaticProperys.Any()) ret += Environment.NewLine;
                ret += "Static Propertys";
                ret += Environment.NewLine;

                var maxLengthDataType = StaticProperys.Max(n => n.DataType.Length);
                var maxLengthName = StaticProperys.Max(n => n.Name.Length);
                var maxLengthDescription = StaticProperys.Max(n => n.Description.Length);
                var header = new string('-', maxLengthDataType) + "-|-" + new string('-', maxLengthName) + "-|-" + new string('-', maxLengthDescription);

                ret += header;
                ret += Environment.NewLine;

                foreach (var docu in StaticProperys)
                {
                    var name = (docu.Name + new string(' ', maxLengthName)).Substring(0, maxLengthName);
                    var description = (docu.Description + new string(' ', maxLengthDescription)).Substring(0, maxLengthDescription);
                    var dataType = (docu.DataType + new string(' ', maxLengthDataType)).Substring(0, maxLengthDataType);
                    ret += $"{dataType} | {name} | {description}";
                    ret += Environment.NewLine;
                }
            }
            if(nonStaticMethods.Any())
            {
                if (nonStaticFields.Any() || StaticFields.Any() || nonStaticProperys.Any() ||StaticProperys.Any()) ret += Environment.NewLine;
                ret += "Non-Static Methods";
                ret += Environment.NewLine;

                var maxLengthReturnType = nonStaticMethods.Max(n => n.ReturnType.Length);
                var maxLengthName = nonStaticMethods.Max(n => n.Name.Length + n.Parameters.Length + 2);
                var maxLengthDescription = nonStaticMethods.Max(n => n.Description.Length);
                var header = new string('-', maxLengthReturnType) + "-|-" + new string('-', maxLengthName) + "-|-" + new string('-', maxLengthDescription);

                ret += header;
                ret += Environment.NewLine;

                foreach (var docu in nonStaticMethods)
                {
                    var name = (docu.Name + new string(' ', maxLengthName)).Substring(0, maxLengthName);
                    var description = (docu.Description + new string(' ', maxLengthDescription)).Substring(0, maxLengthDescription);
                    var parameters = docu.Parameters;
                    var returnType = (docu.ReturnType + new string(' ', maxLengthReturnType)).Substring(0, maxLengthReturnType);
                    ret += $"{returnType} | {name}({parameters}) | {description}";
                    ret += Environment.NewLine;
                }
            }
            if(StaticMethods.Any())
            {
                if (nonStaticFields.Any() || StaticFields.Any() || nonStaticProperys.Any() || StaticProperys.Any() || nonStaticMethods.Any()) ret += Environment.NewLine;
                ret += "Static Methods";
                ret += Environment.NewLine;

                var maxLengthReturnType = StaticMethods.Max(n => n.ReturnType.Length);
                var maxLengthName = StaticMethods.Max(n => n.Name.Length + n.Parameters.Length + 2);
                var maxLengthDescription = StaticMethods.Max(n => n.Description.Length);
                var header = new string('-', maxLengthReturnType) + "-|-" + new string('-', maxLengthName) + "-|-" + new string('-', maxLengthDescription);

                ret += header;
                ret += Environment.NewLine;

                foreach (var docu in StaticMethods)
                {
                    var name = (docu.Name + "(" + docu.Parameters + ")" + new string(' ', maxLengthName)).Substring(0, maxLengthName);
                    var description = (docu.Description + new string(' ', maxLengthDescription)).Substring(0, maxLengthDescription);
                    var parameters = docu.Parameters;
                    var returnType = (docu.ReturnType + new string(' ', maxLengthReturnType)).Substring(0, maxLengthReturnType);
                    ret += $"{returnType} | {name} | {description}";
                    ret += Environment.NewLine;
                }
            }

            ret += Environment.NewLine;
            ret += Environment.NewLine;
            ret += Environment.NewLine;

            return ret;
        }

        static internal async Task RunScripts()
        {
            SubDirectorys.Clear();
            CalculatedHosts.Clear();
            Calculated = false;

            SubDirectorys.AddRange(Directory.GetDirectories(Path.Combine(".", "Scripts"), "*", SearchOption.TopDirectoryOnly));
            SubDirectorys.RemoveAll(n => n.Contains("Sample Script"));
            SubDirectorys.RemoveAll(n => n.Contains("DLL References"));

            var list = new List<Task>();
            foreach (var script in SubDirectorys)
                list.Add(Task.Run(() => RunScript(script)));

            await Task.WhenAll(list);
            
            Calculated = true;
            HostsCalculated?.Invoke();
        }

        static private async Task RunScript(string path)
        {
            if (File.Exists(Path.Combine(path, "_Error.txt"))) File.Delete(Path.Combine(path, "_Error.txt"));
            if (File.Exists(Path.Combine(path, "_Script.txt"))) File.Delete(Path.Combine(path, "_Script.txt"));

            //Loading the Configuration for the Script
            var config = ScriptConfig.Load(Path.Combine(path, "ScriptConfig.xml"));

            if(config.isActive == false) return; // No need to load the Script if it's not active

            //Adding Usings & Common used Types (Settings, Session & StreamDeck Access)
            var so = ScriptOptions.Default.AddImports(config.Using_References);
            so = so.AddReferences(new[]
            {
                typeof(Settings.Settings).GetTypeInfo().Assembly,
                typeof(Session.Session).GetTypeInfo().Assembly,
                typeof(CustomSettings).GetTypeInfo().Assembly,
                typeof(CustomObject).GetTypeInfo().Assembly,
                typeof(CustomObjectData).GetTypeInfo().Assembly,
                typeof(StreamDeckStatics).GetTypeInfo().Assembly,
                typeof(Documentation).GetTypeInfo().Assembly,
                typeof(Form).GetTypeInfo().Assembly,
                typeof(Point).GetTypeInfo().Assembly,
            });

            var script = "";

            //Adding DLL References using #r
            foreach (var dll in config.DLL_References) script += $"#r \"{Path.Combine(".", "Scripts", "DLL References", dll)}\"" + Environment.NewLine;

            script += Environment.NewLine;

            foreach (var file in config.File_References)
            {
                script += $"//---- File: {file}";
                script += Environment.NewLine;
                script += File.ReadAllText(Path.Combine(path, file));
                script += Environment.NewLine;
            }

            script += $"//---- Need to Add a 'return' value for the script without a ; Don't worry, thats correct";
            script += Environment.NewLine;
            script += "null";

            var host = new ScriptHost { Config = config };
            try { await CSharpScript.EvaluateAsync<object>(code: script, globals: host, options: so); }
            catch (Exception ex)
            {
                var errMessage = "Please see _Script.txt to see what file to fix! I recommend Notepad++";
                errMessage += ex.Message + Environment.NewLine + "#####################################################################" + Environment.NewLine;
                errMessage += ex.Message + Environment.NewLine + Environment.NewLine;

                var innerEx = ex.InnerException;
                while (innerEx != null)
                {
                    errMessage += innerEx.Message + Environment.NewLine + Environment.NewLine;
                    innerEx = innerEx.InnerException;
                }

                File.WriteAllText(Path.Combine(path, "_Error.txt"), errMessage);
                File.WriteAllText(Path.Combine(path, "_Script.txt"), script);
            }
            CalculatedHosts.Add(host);
        }
    }
}
