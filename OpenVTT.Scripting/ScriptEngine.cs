using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpenVTT.Scripting
{
    static internal class ScriptEngine
    {
        static List<string> SubDirectorys = new List<string>();
        static List<ScriptHost> CalculatedHosts = new List<ScriptHost>();

        static internal bool calculated = false;
        static internal Action HostsCalculated;

        static ScriptEngine()
        {
            CleanUnusedFolders();

            if (!Directory.Exists(Path.Combine(".", "Scrips")))
            {
                Directory.CreateDirectory(Path.Combine(".", "Scrips"));
                Directory.CreateDirectory(Path.Combine(".", "Scrips", "_Sample Script"));
                Directory.CreateDirectory(Path.Combine(".", "Scrips", "_DLL References"));

                File.WriteAllText(Path.Combine(".", "Scrips", "_Sample Script", "Main.txt"), "Config.Version = 2;");
                ScriptConfig.Save(Path.Combine(".", "Scrips", "_Sample Script", "ScriptConfig.xml"));
            }

            SubDirectorys.AddRange(Directory.GetDirectories(Path.Combine(".", "Scrips"), "*", SearchOption.TopDirectoryOnly));
            SubDirectorys.RemoveAll(n => n.Contains("Sample Script"));
            SubDirectorys.RemoveAll(n => n.Contains("DLL References"));
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

        static internal async Task RunScripts()
        {
            var list = new List<Task>();
            foreach (var script in SubDirectorys)
                list.Add(Task.Run(() => RunScript(script)));

            await Task.WhenAll(list);
            
            calculated = true;
            HostsCalculated?.Invoke();
        }

        static private async Task RunScript(string path)
        {
            if (File.Exists(Path.Combine(path, "_Error.txt"))) File.Delete(Path.Combine(path, "_Error.txt"));
            if (File.Exists(Path.Combine(path, "_Script.txt"))) File.Delete(Path.Combine(path, "_Script.txt"));

            //Loading the Configuration for the Script
            var config = ScriptConfig.Load(Path.Combine(path, "ScriptConfig.xml"));

            if(config.isActive == false) return; // No need to load the Script if it's not active

            //Adding Usings
            var so = ScriptOptions.Default.AddImports(config.Using_References);

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

            script += "null";

            var host = new ScriptHost { Config = config };
            try { await CSharpScript.EvaluateAsync<object>(code: script, globals: host, options: so); }
            catch (Exception ex)
            {
                var errMessage = "Please see _Script.txt to see what file to fix!";
                errMessage = ex.Message + Environment.NewLine + Environment.NewLine;
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
