using Mono.Cecil;

namespace ExeInjector.Helper
{
    public static class AssemblyHelper
    {
        public static ReaderParameters ReadAndWrite() => 
            new ReaderParameters
        {
            ReadWrite = true,
            InMemory = true,
            ReadingMode = ReadingMode.Immediate
        };

        public static ModuleDefinition GetSystemModule() => ModuleDefinition.ReadModule(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.dll");
    }
}
