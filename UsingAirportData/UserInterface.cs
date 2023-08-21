using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UsingAirportData
{
    internal class UserInterface
    {
        public static void Start()
        {
            List<Option> options = new List<Option>
            {
                new Option{ Text = "Print foo", Func = () => {Console.WriteLine("FOO"); } },
                new Option{ Text = "Print bar", Func = () => {Console.WriteLine("BAR"); } }
            };

            MenuPage mainMenu = new MenuPage(options);
            mainMenu.Display();

            bool exit = false;
            do
            {
                var key = Console.ReadKey(true);
                mainMenu.CheckInput(key);
            } while (!exit);
            /*
            Console.Clear();
            Console.WriteLine("1. Show airports by elevation");
            Console.WriteLine("2. Apply filter");

            bool exit = false;
            do
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        ElevationMenu();
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("two");
                        break;
                    case ConsoleKey.Q:
                        exit = true;
                        break;
                    default:
                        break;
                }
            } while (!exit);
            */
        }
        
        private static void ElevationMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Display n highest elevation airports");
            Console.WriteLine("2. Display n lowest elevation airports");

            bool exit = false;
            do
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        ElevationMenu();
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("two");
                        break;
                    case ConsoleKey.Q:
                        exit = true;
                        break;
                    default:
                        break;
                }
            } while (!exit);
        }
    }

    internal delegate void OptionFunction();
    internal class Option
    {
        public String Text { get; set; }
        public OptionFunction Func { get; set; }

        public void Action()
        {
            Func();
        }

    }

    internal class MenuPage
    {
        //contains options
        //displays these options
        List<Option> _options;

        public MenuPage(List<Option> options)
        {
            _options = options;
        }

        public void Display()
        {
            for (int i = 0; i < _options.Count; i++)
            {
                Console.Write((i+1) + ". ");
                Console.WriteLine(_options[i].Text);
            }
        }
        public void CheckInput(ConsoleKeyInfo key)
        {
            int choice = KeyToInt(key);
            Console.WriteLine(choice);
            if (choice < _options.Count)
            {
                _options[choice-1].Action();
            }
        }

        private int KeyToInt(ConsoleKeyInfo key)
        {
            const int D0_ID = 48;
            const int D9_ID = 57;
            int id = Convert.ToInt32(key.KeyChar);

            if (id >= D0_ID && id <= D9_ID)
            {
                return Convert.ToInt32(key.KeyChar) - D0_ID;
            }
            else
            {
                return -1;
            }
        }
    }
}
