using System;

namespace ExeInjector.Helper
{
    internal class Menu
    {
        internal int Index => menuController.CurrentIndex;
        private MenuController menuController;

        public Menu(params MenuItem[] menuItem)
        {
            menuController = new MenuController(menuItem, 7);
        }

        internal void ShowMenu(bool isScroll = false)
        {
            Console.Clear();

            do
            {
                if (!menuController.Update(isScroll)) 
                    continue;
                
                Console.Clear();
                menuController.MenuItems[Index].Action();
                break;

            } while (true);
        }
    }
}