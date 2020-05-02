using System;

namespace ExeInjector.Helper
{
    internal class MenuController
    {
        internal int CurrentIndex { get; private set; }
        internal MenuItem[] MenuItems { get; }
        private int elementCount;
        private readonly int maxDisplaySize;

        public MenuController(MenuItem[] menuItems, int maxDisplaySize)
        {
            MenuItems = menuItems;
            elementCount = menuItems.Length;
            this.maxDisplaySize = maxDisplaySize;
        }

        internal bool Receive(bool isScroll)
        {
            Console.Clear();
            RefreshMenu(isScroll);
            
            var consoleKey = Console.ReadKey();
            switch (consoleKey.Key)
            {
                case ConsoleKey.UpArrow:
                        
                    if (CurrentIndex <= 0)
                        CurrentIndex = elementCount - 1;
                    else
                        CurrentIndex--;
                    
                    return false;
                
                case ConsoleKey.DownArrow:

                    if (CurrentIndex == elementCount - 1)
                        CurrentIndex = 0;
                    else
                        CurrentIndex++;

                    return false;
                
                case ConsoleKey.Enter:
                    return true;
                
                default:
                    return false;
            }
        }
        
        private void RefreshMenu(bool isScroll)
        {
            if(isScroll)
                ScrollMenu();
            else
                DefaultMenu();
        }

        private void DefaultMenu()
        {
            for (var i = 0; i < elementCount; i++)
            {
                var menuItem = MenuItems[i];

                if (CurrentIndex == i)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(menuItem.Label);
                Console.ResetColor();
            }

            Console.WriteLine("______________________________\n");
            Console.WriteLine($"{MenuItems[CurrentIndex].Description}");
        }

        private void ScrollMenu()
        {
            for (var i = Math.Min(elementCount - maxDisplaySize, CurrentIndex); i < CurrentIndex + maxDisplaySize; i++)
            {
                if(i > CurrentIndex + maxDisplaySize)
                    break;

                if (i >= elementCount)
                    break;

                var menuItem = MenuItems[i];

                if (CurrentIndex == i)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(menuItem.Label);
                Console.ResetColor();
            }

            Console.WriteLine("______________________________\n");
            Console.WriteLine($"{MenuItems[CurrentIndex].Description}");
        }
    }
}