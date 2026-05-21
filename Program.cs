using MovieRental;

namespace Project
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Client.LoadFromFile();
            Movie.LoadFromFile();
            Rental.LoadFromFile();
            Application.Run(new MenuForm());
        }
    }
}