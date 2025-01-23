namespace pokedex;

class Program
{
    static void Main(string[] args)
    {

        MenuHandler menuHandler = new MenuHandler();
        while (menuHandler != null)
        {
            if (!menuHandler.IsLoggedIn) menuHandler.Initialise();
            else menuHandler.AuthorisedMenu();
        }
    }
}
