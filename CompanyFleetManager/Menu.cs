using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManager
{
    internal class Menu
    {
        private Dictionary<string, Program.MenuCallback> Actions;

        public Menu(Dictionary<string, Program.MenuCallback> actions)
        {
            Actions = actions;
        }

        public void Handle()
        {
            Console.WriteLine("Select an option: ");
            Console.WriteLine("\t1. Check if database has been created");
            var choice = Console.ReadLine();

            if (choice != null && Actions.ContainsKey(choice))
            {
                Actions[choice].Invoke();
            }
            else
                Console.WriteLine("Invalid option!");
        }
    }
}
