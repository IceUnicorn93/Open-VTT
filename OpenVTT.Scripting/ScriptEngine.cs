﻿using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using OpenVTT.Common;
using OpenVTT.Logging;
using OpenVTT.Session;
using OpenVTT.Settings;
using OpenVTT.StreamDeck;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
            Logger.Log("Class: ScriptEngine | Constructor");

            CleanUnusedFolders();

            var appStartPath = Application.StartupPath;

            if (!Directory.Exists(Path.Combine(appStartPath, "Scripts")))
            {
                Directory.CreateDirectory(Path.Combine(appStartPath, "Scripts"));
            }

            if (!Directory.Exists(Path.Combine(appStartPath, "Scripts", "_Sample Script")))
            {
                Directory.CreateDirectory(Path.Combine(appStartPath, "Scripts", "_Sample Script"));

                File.WriteAllText(Path.Combine(appStartPath, "Scripts", "_Sample Script", "Main.cs"), GetDefaultScript());
                ScriptConfig.SaveDefault(Path.Combine(appStartPath, "Scripts", "_Sample Script", "ScriptConfig.xml"));
            }

            CreateDocumentation();
        }

        static private string GetDefaultScript()
        {
            Logger.Log("Class: ScriptEngine | GetDefaultScript");

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
            Logger.Log("Class: ScriptEngine | CleanUnusedFolders");

            var appStartPath = Application.StartupPath;

            if (Directory.Exists(Path.Combine(appStartPath, "cs"))) Directory.Delete(Path.Combine(appStartPath, "cs"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "de"))) Directory.Delete(Path.Combine(appStartPath, "de"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "es"))) Directory.Delete(Path.Combine(appStartPath, "es"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "fr"))) Directory.Delete(Path.Combine(appStartPath, "fr"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "it"))) Directory.Delete(Path.Combine(appStartPath, "it"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "ja"))) Directory.Delete(Path.Combine(appStartPath, "ja"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "ko"))) Directory.Delete(Path.Combine(appStartPath, "ko"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "pl"))) Directory.Delete(Path.Combine(appStartPath, "pl"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "pt-BR"))) Directory.Delete(Path.Combine(appStartPath, "pt-BR"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "ru"))) Directory.Delete(Path.Combine(appStartPath, "ru"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "tr"))) Directory.Delete(Path.Combine(appStartPath, "tr"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "zh-Hans"))) Directory.Delete(Path.Combine(appStartPath, "zh-Hans"), true);
            if (Directory.Exists(Path.Combine(appStartPath, "zh-Hant"))) Directory.Delete(Path.Combine(appStartPath, "zh-Hant"), true);
        }

        static private void CreateDocumentation()
        {
            Logger.Log("Class: ScriptEngine | CreateDocumentation");

            string text = "";

            var appStartPath = Application.StartupPath;

            var list = new List<Type>()
            {
                typeof(CustomObject), typeof(CustomObjectData), typeof(CustomSettings),
                typeof(DisplayType), typeof(FogOfWar.FogOfWar), typeof(FogState), typeof(Layer), typeof(Scene), typeof(ScreenInformation),
                typeof(ScriptConfig), typeof(ScriptHost), typeof(Session.Session), typeof(Settings.Settings), typeof(StreamDeckStatics),
                typeof(XmlColor)
            };

            foreach (var item in list)
                text += GetDocumentationForType(item);

            File.WriteAllText(Path.Combine(appStartPath, "Scripts", "Documentation.txt"), text);
        }

        static private string GetDocumentationForType(Type type)
        {
            Logger.Log("Class: ScriptEngine | GetDocumentationForType");

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
            Logger.Log("Class: ScriptEngine | RunScripts");

            var appStartPath = Application.StartupPath;

            SubDirectorys.Clear();
            CalculatedHosts.Clear();
            Calculated = false;

            SubDirectorys.AddRange(Directory.GetDirectories(Path.Combine(appStartPath, "Scripts"), "*", SearchOption.TopDirectoryOnly));
            SubDirectorys.RemoveAll(n => n.Contains("Sample Script"));
            SubDirectorys.RemoveAll(n => n.Contains("DLL References"));

            var runAsync = false;
            if(runAsync)
            {
                Logger.Log("Class: ScriptEngine | RunScripts Async");

                var list = new List<Task>();
                foreach (var script in SubDirectorys)
                    list.Add(Task.Run(() => RunScript(script)));

                await Task.WhenAll(list);
            }
            else
            {
                Logger.Log("Class: ScriptEngine | RunScripts Sync");

                foreach (var script in SubDirectorys)
                    RunScript(script);
            }

            Calculated = true;

            if (HostsCalculated != null)
                    HostsCalculated();
        }

        static internal ScriptHost RunScript(string path, bool isRerun = false)
        {
            Logger.Log("Class: ScriptEngine | RunScript");

            var appStartPath = Application.StartupPath;

            if (File.Exists(Path.Combine(path, "_Error.txt"))) File.Delete(Path.Combine(path, "_Error.txt"));
            if (File.Exists(Path.Combine(path, "_Script.txt"))) File.Delete(Path.Combine(path, "_Script.txt"));

            //Loading the Configuration for the Script
            Logger.Log("Class: ScriptEngine | RunScript | Load Config");
            var config = ScriptConfig.Load(Path.Combine(path, "ScriptConfig.xml"));

            if(config.isActive == false) return null; // No need to load the Script if it's not active

            //Adding Usings & Common used Types (Settings, Session & StreamDeck Access)
            Logger.Log("Class: ScriptEngine | RunScript | Add Using References");
            var so = ScriptOptions.Default.AddImports(config.Using_References);

            var list = new List<Assembly>();

            try { list.Add(typeof(Settings.Settings).GetTypeInfo().Assembly); }
            catch { Logger.Log("Class: ScriptEngine | RunScript | Fail at Settings"); }

            try { list.Add(typeof(Session.Session).GetTypeInfo().Assembly); }
            catch { Logger.Log("Class: ScriptEngine | RunScript | Fail at Session"); }

            try { list.Add(typeof(CustomSettings).GetTypeInfo().Assembly); }
            catch { Logger.Log("Class: ScriptEngine | RunScript | Fail at CustomSettings"); }

            try { list.Add(typeof(CustomObject).GetTypeInfo().Assembly); }
            catch { Logger.Log("Class: ScriptEngine | RunScript | Fail at CustomObject"); }

            try { list.Add(typeof(CustomObjectData).GetTypeInfo().Assembly); }
            catch { Logger.Log("Class: ScriptEngine | RunScript | Fail at CustomObjectData"); }

            try { list.Add(typeof(StreamDeckStatics).GetTypeInfo().Assembly); }
            catch { Logger.Log("Class: ScriptEngine | RunScript | Fail at StreamDeckStatics"); }

            try { list.Add(typeof(Documentation).GetTypeInfo().Assembly); }
            catch { Logger.Log("Class: ScriptEngine | RunScript | Fail at Documentation"); }

            try { list.Add(typeof(Form).GetTypeInfo().Assembly); }
            catch { Logger.Log("Class: ScriptEngine | RunScript | Fail at Form"); }

            try { list.Add(typeof(Point).GetTypeInfo().Assembly); }
            catch { Logger.Log("Class: ScriptEngine | RunScript | Fail at Point"); }

            //TODO: More Testing for Linux
            /*
             * Copy used DLLs to a Temp Folder in that Script Folder.
             * Collect Locations for these DLLs
             * Add these Locations to DLL References
             * Alternative
             * Add them to the ScriptOptions
             */

            list.Distinct().ToList().ForEach(n => config.DLL_References.Add(n.Location));

            //so = so.AddReferences(new[]
            //{
            //typeof(Settings.Settings).GetTypeInfo().Assembly,
            //typeof(Session.Session).GetTypeInfo().Assembly,
            //typeof(CustomSettings).GetTypeInfo().Assembly,
            //typeof(CustomObject).GetTypeInfo().Assembly,
            //typeof(CustomObjectData).GetTypeInfo().Assembly,
            //typeof(StreamDeckStatics).GetTypeInfo().Assembly,
            //typeof(Documentation).GetTypeInfo().Assembly,
            //typeof(Form).GetTypeInfo().Assembly,
            //typeof(Point).GetTypeInfo().Assembly,
            //});

            var script = "";

            Logger.Log("Class: ScriptEngine | RunScript | Create #r Reference");
            //Adding DLL References using #r
            foreach (var dll in config.DLL_References) script += $"#r \"{Path.Combine(path, dll)}\"" + Environment.NewLine;

            Logger.Log("Class: ScriptEngine | RunScript | Create FileReferences");
            script += Environment.NewLine;
            foreach (var file in config.File_References)
            {
                script += $"//---- File: {file}";
                script += Environment.NewLine;
                Logger.Log($"Class: ScriptEngine | RunScript | Read File: {Path.Combine(path, file)}");
                script += File.ReadAllText(Path.Combine(path, file));
                
                script += Environment.NewLine;
            }

            script += $"//---- Need to Add a 'return' value for the script without a ; Don't worry, thats correct";
            script += Environment.NewLine;
            script += "null";

            Logger.Log("Class: ScriptEngine | RunScript | Create ScriptHost");
            var host = new ScriptHost { Config = config, path = Path.Combine(path) };
            if (isRerun == false)
                CalculatedHosts.Add(host);

            try
            {
                var r = CSharpScript.EvaluateAsync<object>(code: script, globals: host, options: so).Result;

                host.hasSuccessfullyRun = true;
                host.exception = null;
            }
            catch (Exception ex)
            {
                host.exception = ex;
                host.hasSuccessfullyRun = false;

                var errMessage = "Please see _Script.txt to see what file to fix! I recommend Notepad++" + Environment.NewLine;
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

            return host;
        }

        static internal async Task<T> RunUiScript<T>(string code, UiScriptHost host)
        {
            Logger.Log("Class: ScriptEngine | RunUiScript<T>");

            var script = "";

            var so = ScriptOptions.Default.AddImports("System",
                "System.Drawing",
                "System.ComponentModel",
                "System.IO",
                "System.Runtime.CompilerServices",
                "System.Text.Json",
                "System.Windows.Forms");

            Logger.Log("Class: ScriptEngine | Added Usings");

            List<string> dlls = new List<string>();
            if (Directory.Exists(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\"))
                dlls = Directory.GetFileSystemEntries(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\", "System*.dll", SearchOption.TopDirectoryOnly).ToList();
            else
            {
                Logger.Log("Class: ScriptEngine | Missing .net 4.7.2");

                var dirs = Directory.GetDirectories(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\\");
                var versions = dirs.Select(n => n.Replace(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\\v", "").Replace(".","")).ToList();
                versions.RemoveAll(n => !int.TryParse(n, out _));
                var maxVersion = versions.Max().ToString().Cast<char>();
                var stringVersion = string.Join(".", maxVersion);
                dlls = Directory.GetFileSystemEntries(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\\v" + stringVersion + @"\", "System*.dll", SearchOption.TopDirectoryOnly).ToList();
            }
            dlls.RemoveAll(n => n.EndsWith("System.EnterpriseServices.Wrapper.dll"));
            dlls.RemoveAll(n => n.EndsWith("System.EnterpriseServices.Thunk.dll"));

            dlls.Add(Path.Combine(Application.StartupPath, "System.Text.Json.dll"));
            dlls.Add(Path.Combine(Application.StartupPath, "OpenVTT.UiDesigner.dll"));

            foreach (var dll in dlls) script += $"#r \"{dll}\"" + Environment.NewLine;

            Logger.Log("Class: ScriptEngine | Added References");

            script += Environment.NewLine;

            script += code;
            script = script.Replace("Submission#0.", "");

            Logger.Log($"Class: ScriptEngine | Run Script with host = {host != null}");
            if (host == null)
                return await CSharpScript.EvaluateAsync<T>(code: script, options: so);
            else
                return await CSharpScript.EvaluateAsync<T>(code: script, globals: host, options: so);
        }
    }
}
