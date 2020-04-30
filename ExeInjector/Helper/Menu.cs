using System;

namespace ExeInjector.Helper
{
    internal class Menu
    {
        internal int Index { get; private set; }
        
        private static readonly int MaxSize = 7;
        private int ItemLength { get; }
        private MenuItem[] MenuItems { get; }

        public Menu(params MenuItem[] menuItem)
        {
            Index = 0;
            MenuItems = menuItem;
            ItemLength = menuItem.Length;
        }

        internal void ShowMenu()
        {
            Console.Clear();

            var enter = false;
            while (!enter)
            {
                Console.Clear();
                RefreshMenu();
                
                var consoleKey = Console.ReadKey();
                switch (consoleKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        
                        if (Index <= 0)
                            Index = ItemLength - 1;
                        else
                            Index--;
                        
                        break;
                
                    case ConsoleKey.DownArrow:

                        if (Index == ItemLength - 1)
                            Index = 0;
                        else
                            Index++;
                        
                        break;
                
                    case ConsoleKey.Enter:
                        Console.Clear();
                        MenuItems[Index].Action();
                        enter = true;
                        break;
                }
            }
        }

        private void RefreshMenu()
        {
            for (var i = 0; i < ItemLength; i++)
            {
                var menuItem = MenuItems[i];
                
                if (Index == i)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(menuItem.Label.PadRight(MaxSize, ' '));
                Console.ResetColor();
            }
            
            Console.WriteLine("______________________________\n");
            Console.WriteLine($"{MenuItems[Index].Description}");
        }
    }
}