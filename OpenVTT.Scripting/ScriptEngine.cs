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
            if (!Directory.Exists(Path.Combine(".", "Scrips"))) Directory.CreateDirectory(Path.Combine(".", "Scrips"));

            SubDirectorys.AddRange(Directory.GetDirectories(Path.Combine(".", "Scrips"), "*", SearchOption.TopDirectoryOnly));
            SubDirectorys.RemoveAll(n => n.Contains("Sample Script"));
            SubDirectorys.RemoveAll(n => n.Contains("DLL References"));
        }

        static internal async Task RunScripts()
        {
            //ScriptConfig.Save();

            var list = new List<Task>();
            foreach (var script in SubDirectorys)
                list.Add(Task.Run(() => RunScript(script)));

            await Task.WhenAll(list);
            
            calculated = true;
            HostsCalculated?.Invoke();
        }

        static private async Task RunScript(string path)
        {
            //Loading the Configuration for the Script
            var config = ScriptConfig.Load(Path.Combine(path, "ScriptConfig.xml"));

            //Adding Usings
            var so = ScriptOptions.Default.AddImports(config.Using_References);

            var script = "";

            //Adding DLL References using #r
            foreach (var dll in config.DLL_References) script += $"#r \"{dll}\"" + Environment.NewLine;

            script += Environment.NewLine;

            foreach (var file in config.File_References)
            {
                script += File.ReadAllText(Path.Combine(path, file));
                script += Environment.NewLine;
            }

            script += "null";

            var host = new ScriptHost { Config = config };
            await CSharpScript.EvaluateAsync<object>(code: script, globals: host, options: so);
            CalculatedHosts.Add(host);
        }
    }
}
