using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using OpenVTT.Common;
using OpenVTT.Session;
using OpenVTT.Settings;
using OpenVTT.StreamDeck;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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

            if (!Directory.Exists(Path.Combine(".", "Scrips")))
            {
                Directory.CreateDirectory(Path.Combine(".", "Scrips"));
                Directory.CreateDirectory(Path.Combine(".", "Scrips", "_DLL References"));
            }

            if (!Directory.Exists(Path.Combine(".", "Scrips", "_Sample Script")))
            {
                Directory.CreateDirectory(Path.Combine(".", "Scrips", "_Sample Script"));

                File.WriteAllText(Path.Combine(".", "Scrips", "_Sample Script", "Main.txt"), "Page.Text = Config.Name;");
                ScriptConfig.Save(Path.Combine(".", "Scrips", "_Sample Script", "ScriptConfig.xml"));
                //CreateDocumentation();
            }
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

            text += GetDocumentationForType(typeof(ScriptHost));
            text += GetDocumentationForType(typeof(ScriptConfig));
            text += GetDocumentationForType(typeof(StreamDeckStatics));
            text += GetDocumentationForType(typeof(Session.Session));
            text += GetDocumentationForType(typeof(Scene));
            text += GetDocumentationForType(typeof(Layer));
            text += GetDocumentationForType(typeof(FogOfWar.FogOfWar));
            text += GetDocumentationForType(typeof(FogState));
            text += GetDocumentationForType(typeof(Settings.Settings));
            text += GetDocumentationForType(typeof(ScreenInformation));
            text += GetDocumentationForType(typeof(DisplayType));
            text += GetDocumentationForType(typeof(XmlColor));

            File.WriteAllText(Path.Combine(".", "Scrips", "_Sample Script", "Documentation.txt"), text);
        }

        static private string GetDocumentationForType(Type type)
        {
            var ret = $"{type}{Environment.NewLine}";

            ret += type.CustomAttributes.SingleOrDefault()?.ConstructorArguments.SingleOrDefault().Value as string ?? "";
            ret += Environment.NewLine;
            var lst = type.GetFields()[0].CustomAttributes.First().ConstructorArguments.ToList();

            foreach (var item in type.GetFields())
            {
                var name = item.Name;
                var propType = item.FieldType.Name;
                var description = item.CustomAttributes.FirstOrDefault(n => n.GetType() == typeof(Documentation))?.ConstructorArguments.SingleOrDefault().Value as string ?? "";
                //var getPrivate = item.GetMethod.IsPrivate;
                //var setPrivate = item.SetMethod.IsPrivate;
                // Get:{(getPrivate ? "private" : "public")} Set:{(setPrivate ? "private" : "public")}
                ret += $"{name} | {propType} | {description}";
                ret += Environment.NewLine;
            }

            //Propertys
            //Methds

            ret += Environment.NewLine;
            ret += Environment.NewLine;

            return ret;
        }

        static internal async Task RunScripts()
        {
            SubDirectorys.Clear();
            CalculatedHosts.Clear();
            Calculated = false;

            SubDirectorys.AddRange(Directory.GetDirectories(Path.Combine(".", "Scrips"), "*", SearchOption.TopDirectoryOnly));
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
                typeof(StreamDeckStatics).GetTypeInfo().Assembly,
                typeof(Documentation).GetTypeInfo().Assembly,
            });

            var script = "";

            //Adding DLL References using #r
            foreach (var dll in config.DLL_References) script += $"#r \"{Path.Combine(".", "Scrips", "DLL References", dll)}\"" + Environment.NewLine;

            script += Environment.NewLine;

            foreach (var file in config.File_References)
            {
                script += $"//---- File: {file}";
                script += Environment.NewLine;
                script += File.ReadAllText(Path.Combine(path, file));
                script += Environment.NewLine;
            }

            script += $"//---- Need to Add a 'return' value for the script without a ; Don't worry, thats correct";
            script += "null";

            var host = new ScriptHost { Config = config };
            try { await CSharpScript.EvaluateAsync<object>(code: script, globals: host, options: so); }
            catch (Exception ex)
            {
                var errMessage = "Please see _Script.txt to see what file to fix! I recommend Notepad++";
                errMessage = ex.Message + Environment.NewLine + "#####################################################################" + Environment.NewLine;
                errMessage = ex.Message + Environment.NewLine + Environment.NewLine;

                var innerEx = ex.InnerException;
                while (innerEx != null)
                {
                    errMessage = innerEx.Message + Environment.NewLine + Environment.NewLine;
                    innerEx = innerEx.InnerException;
                }

                File.WriteAllText(Path.Combine(path, "_Error.txt"), errMessage);
                File.WriteAllText(Path.Combine(path, "_Script.txt"), script);
            }
            CalculatedHosts.Add(host);
        }
    }
}
