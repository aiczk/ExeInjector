using System.Linq;
using ExeInjector.Helper;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ExeInjector.Analyzer
{
    internal class MethodWriter
    {
        private string exePath;

        public MethodWriter(string exePath)
        {
            this.exePath = exePath;
        }

        internal void Inject(MethodDefinition method)
        {
            var methodBody = method.Body;
            var processor = methodBody.GetILProcessor();
            var initFirstIl = methodBody.Instructions.First();
            var processMethods = AssemblyHelper.GetSystemModule().GetType("System.Diagnostics", "Process").Methods;
            var processStart = method.Module.ImportReference(processMethods.First(x => x.Name == "Start" && x.Parameters.Count == 1));

            //Insert IL in reverse order.
            processor.InsertAfter(initFirstIl, Instruction.Create(OpCodes.Pop));
            processor.InsertAfter(initFirstIl, Instruction.Create(OpCodes.Call, processStart));
            processor.InsertAfter(initFirstIl, Instruction.Create(OpCodes.Ldstr, exePath));
        }
    }
}