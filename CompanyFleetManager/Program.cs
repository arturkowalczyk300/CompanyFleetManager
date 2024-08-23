namespace CompanyFleetManager
{
    internal class Program
    {
        public delegate void MenuCallback();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var actions = new Dictionary<string, MenuCallback>()
            {
                { "1", () => { Console.WriteLine("First option launched!");} }
            };

            var menu = new Menu(actions);

            while (true)
            {
                menu.Handle();
            }
        }
    }
}
