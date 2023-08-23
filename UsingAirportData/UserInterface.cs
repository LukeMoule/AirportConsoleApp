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
            //List<Option> options = new List<Option>
            //{
            //    new Option{ Text = "Print foo", Func = () => {Console.WriteLine("FOO"); } },
            //    new Option{ Text = "Print bar", Func = () => {Console.WriteLine("BAR"); } }
            //};

            //MenuPage mainMenu = new MenuPage(options);
            //mainMenu.Display();

            //bool exit = false;
            //do
            //{
            //    var key = Console.ReadKey(true);
            //    mainMenu.CheckInput(key);
            //} while (!exit);
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
            List<IMenuComponent> empty = new List<IMenuComponent>();
            FilterPage Name = new FilterPage("Name", "Enter string to filter name by:");
            FilterPage Country = new FilterPage("Country", "Enter string to filter country by:");
            FilterPage Continent = new FilterPage("Continent", "Enter string to filter continent by:");
            MenuPage CompassMenu = new MenuPage("Display airports by cardinal direction");
            MenuPage ElevationMenu = new MenuPage("Display airports by elevation");
            MenuPage FiltersMenu = new MenuPage("Apply filters", new List<IMenuComponent> { Name,Country,Continent});
            MenuPage DisplayMenu = new MenuPage("Display airports", new List<IMenuComponent> { ElevationMenu, CompassMenu});
            MenuPage MainMenu = new MenuPage("", new List<IMenuComponent> { FiltersMenu, DisplayMenu});

            FiltersMenu.AddComponents(new List<IMenuComponent> { MainMenu } );

            Router.SetNextPage(MainMenu);
            Router.Go();
        }
    }

    public static class Router
    {
        private static Stack<IMenuComponent> pageStack = new Stack<IMenuComponent>();
        private static bool exit = false;
        public static void Exit()
        {
            exit = true;
        }
        public static void SetNextPage(IMenuComponent page)
        {
            pageStack.Push(page);
        }
        public static void Go()
        {
            while (!exit)
            {
                pageStack.Peek().Load();
            } 
        }
    }

    public class FilterPage : IMenuComponent
    {
        private string description;
        private string content;
        public FilterPage(string description, string content)
        {
            this.description = description;
            this.content = content;
        }
        public string GetDescription()
        {
            return description;
        }
        public void Load()
        {
            Console.Clear();
            Console.WriteLine(content);
            string? input = Console.ReadLine();
        }
    }

    public class MenuPage : IMenuComponent
    {
        private string description;
        private List<IMenuComponent> subComponents;
        private IMenuComponent nextPage;
        public MenuPage(string description, List<IMenuComponent> menuComponents)
        {
            // limit menucomponent to max. 9
            this.description = description;
            subComponents = new List<IMenuComponent>(menuComponents);
            nextPage = this;
        }
        public MenuPage(string description) :
            this(description, new List<IMenuComponent>())
        { 
        }
        public string GetDescription()
        {
            return description;
        }
        public void AddComponents(List<IMenuComponent> menuComponents)
        {
            subComponents.AddRange(menuComponents);
        }
        public void Load()
        {
            Console.Clear();
            WritePage();
            while (ParseInput(Console.ReadKey(true).Key)) { }
        }
        private void WritePage()
        {
            for (int i = 0; i < subComponents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {subComponents[i].GetDescription()}");
            }
            Console.WriteLine("Press <q> to exit...");
        }
        public bool ParseInput(ConsoleKey key)
        {
            
            int index = Keys.Lookup(key);

            if (index == -1)
            {
                Router.Exit();
                return false;
            }
            if (index < subComponents.Count)
            {
                Router.SetNextPage(subComponents[index]);
                return false;
            }
            return true;
        }
    }
    public interface IMenuComponent
    {
        string GetDescription();
        void Load();
    }

    public class Keys
    {
        private static readonly Dictionary<ConsoleKey, int> Dict = new Dictionary<ConsoleKey, int>
        {
            {ConsoleKey.D1, 0},
            {ConsoleKey.D2, 1},
            {ConsoleKey.D3, 2},
            {ConsoleKey.D4, 3},
            {ConsoleKey.D5, 4},
            {ConsoleKey.D6, 5},
            {ConsoleKey.D7, 6},
            {ConsoleKey.D8, 7},
            {ConsoleKey.D9, 8},
            {ConsoleKey.Q, -1}
        };

        public static int Lookup(ConsoleKey key)
        {
            try
            {
                return Dict[key];
            }
            catch (Exception e)
            {
                return 999;
            }
            
        }
    }
}
