using System.Text.RegularExpressions;
using ExeInjector.Helper;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace ExeInjector.Analyzer
{
    //Awake,Main,Start,OnEnable
    internal class MethodAnalyzer
    {
        private ModuleDefinition targetModule;
        private string path;
        
        internal MethodAnalyzer(string path)
        {
            this.path = path;
            targetModule = ModuleDefinition.ReadModule(path, AssemblyHelper.ReadAndWrite());
        }

        internal Collection<MethodDefinition> FindMethod()
        {
            //class in initMethod
            var methods = new Collection<MethodDefinition>();

            foreach (var classDefinition in targetModule.Types)
            {
                if(!classDefinition.HasNestedTypes)
                    continue;
                
                foreach (var method in classDefinition.Methods)
                {
                    if(method.Parameters.Count != 0)
                        continue;
                    
                    //if (!Regex.IsMatch(method.Name,"Awake|Start|OnEnable"))
                    //    continue;
                    
                    methods.Add(method);
                }
            }

            return methods;
        }

        internal void End() => targetModule.Write(path);
    }
}