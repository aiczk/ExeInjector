using System;
using System.IO;
using ExeInjector.Analyzer;
using ExeInjector.Helper;

namespace ExeInjector
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.Title = "ExeInjector v0.2";
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
            
            var menu = new Menu
            (
                new MenuItem("Next", "Welcome to ExeInjector!", () => Console.WriteLine("Select the target assembly.")), 
                new MenuItem("Exit", "Terminates the process and closes the window.", () => Environment.Exit(0))
            );
            
            menu.ShowMenu();

            do
            {
                var libPath = DialogHelper.SelectFileDialog("Library(.dll)|*.dll");
                
                if(Path.GetFileName(libPath) == "Assembly-CSharp.dll")
                {
                    Console.Clear();
                    Console.WriteLine("Analyzing...");
                    
                    var analyzer = new MethodAnalyzer(libPath);
                    var methods = analyzer.FindMethod();
                    var targetMethods = new MenuItem[methods.Count];

                    for (var i = 0; i < targetMethods.Length; i++)
                        targetMethods[i] = new MenuItem(methods[i].FullName, "Select the method to inject the code into.", () => Console.WriteLine("Please select an executable file."));

                    var methodsMenu = new Menu(targetMethods);
                    methodsMenu.ShowMenu(true);

                    var exePath = DialogHelper.SelectFileDialog("Exe(.exe)|*.exe");
                    var writer = new MethodWriter(exePath);
                    writer.Inject(methods[methodsMenu.Index]);

                    Console.Clear();
                    Console.WriteLine("Target assembly overwrite complete!");
                    analyzer.Finish();
                    break;
                }
                
                if(libPath == string.Empty)
                    break;
                
                Console.WriteLine("Assembly-CSharp.dll");
            } while (true);

            Console.ReadKey();
        }
    }
}

