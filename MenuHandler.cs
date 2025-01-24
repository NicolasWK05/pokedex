namespace pokedex;

public class MenuHandler
{
    private User user = new User();
    public bool IsLoggedIn { get; set; } = false;

    public void Initialise()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Pokedex");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Search Pokemon");
        Console.WriteLine("4. List Pokemon");
        Console.WriteLine("5. Exit");

        string username = "";
        string password = "";
        bool success = false;

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        char input = keyInfo.KeyChar;

        switch (input)
        {
            case '1':
                Console.Write("\nEnter username: ");
                username = Console.ReadLine() ?? string.Empty;
                Console.Write("\nEnter password: ");
                password = Console.ReadLine() ?? string.Empty;

                success = user.Login(username, password);
                if (success) IsLoggedIn = true;
                else Console.WriteLine("\nLogin failed");

                Console.WriteLine("\nSuccess logging in");
                break;
            case '2':
                Console.Write("\nEnter username: ");
                username = Console.ReadLine() ?? string.Empty;
                Console.Write("\nEnter password: ");
                password = Console.ReadLine() ?? string.Empty;

                User.Register(username, password);
                break;
            case '3':
                // ID search or name/type search
                Console.Write("\nEnter search term: ");
                string search = Console.ReadLine() ?? string.Empty;

                ListPokemonMenu(Pokedex.GetPokemonBySearch(search));
                break;
            case '4':
                ListPokemonMenu(Pokedex.GetAllPokemon());
                break;
            case '5':
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("\nInvalid input");
                break;
        }
    }

    // ListPokemonMenu will display a paginated list of pokemon
    protected void ListPokemonMenu(List<Pokemon> pokemon)
    {

        int page = 0;
        int pageSize = 10;
        int maxPage = pokemon.Count / pageSize;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Pokemon List");
            Console.WriteLine("Page " + (page + 1) + " of " + (maxPage + 1));
            Console.WriteLine("ID | Name | Type | Base Stat Total");

            for (int i = page * pageSize; i < (page + 1) * pageSize; i++)
            {
                if (i < pokemon.Count)
                {
                    Pokemon p = pokemon[i];
                    Console.WriteLine(p.ID + " | " + p.Name + " | " + p.Type + " | " + p.BaseStatTotal);
                }
            }

            Console.WriteLine("Press '←' for previous page, '→' for next page, or 'q' to quit");
            ConsoleKeyInfo input = Console.ReadKey();


            if (input.Key == ConsoleKey.RightArrow)
            {
                if (page < maxPage)
                {
                    page++;
                }
            }
            else if (input.Key == ConsoleKey.LeftArrow)
            {
                if (page > 0)
                {
                    page--;
                }
            }
            else if (input.Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }

    public void AuthorisedMenu()
    {
        // If the user is logged in, this menu will be displayed
        // This menu will allow the user to add, delete, edit, and view pokemon

        Console.Clear();
        Console.WriteLine("Authorised Menu");
        Console.WriteLine("1. Add Pokemon");
        Console.WriteLine("2. Delete Pokemon");
        Console.WriteLine("3. Edit Pokemon");
        Console.WriteLine("4. View Pokemon");
        Console.WriteLine("5. Logout");
        Console.WriteLine("6. Exit");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        char input = keyInfo.KeyChar;

        switch (input)
        {
            case '1':
                AddPokemon();
                break;
            case '2':
                DeletePokemon();
                break;
            case '3':
                EditPokemon();
                break;
            case '4':
                ViewPokemon();
                break;
            case '5':
                user.Logout();
                IsLoggedIn = false;
                break;
            case '6':
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("\nInvalid input");
                break;
        }

    }

    protected void AddPokemon()
    {
        Console.Clear();
        Console.WriteLine("Add Pokemon");
        Console.WriteLine("Enter ID, write 'quit' to return:");
        string input = Console.ReadLine() ?? string.Empty;
        if (input == "" || input == "quit") return;
        int id = Int32.Parse(input);
        Console.WriteLine("Enter Name:");
        string name = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter Type:");
        string type = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter Base Stat Total:");
        int baseStatTotal = Int32.Parse(Console.ReadLine() ?? string.Empty);

        Pokemon newPokemon = new Pokemon { ID = id, Name = name, Type = type, BaseStatTotal = baseStatTotal };
        Pokedex.AddPokemon(newPokemon);
    }

    protected void DeletePokemon()
    {
        Console.Clear();
        Console.WriteLine("Delete Pokemon");
        Console.WriteLine("Enter ID:");
        int id = Int32.Parse(Console.ReadLine() ?? string.Empty);

        Pokedex.DeletePokemon(id);
    }

    protected void EditPokemon()
    {
        Console.Clear();
        Console.WriteLine("Edit Pokemon");
        Console.WriteLine("Enter ID:");
        int id = Int32.Parse(Console.ReadLine() ?? string.Empty);

        Pokedex.EditPokemon(id);
    }

    protected void ViewPokemon()
    {
        // Ask if they want to search or view all
        // If search, ask whether to use search term or ID
        // If view all, display all pokemon

        Console.Clear();
        Console.WriteLine("View Pokemon");
        Console.WriteLine("1. Search Pokemon");
        Console.WriteLine("2. View All Pokemon");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        char input = keyInfo.KeyChar;

        switch (input)
        {
            case '1':
                Console.WriteLine("\nEnter search term: ");
                string search = Console.ReadLine() ?? string.Empty;

                ListPokemonMenu(Pokedex.GetPokemonBySearch(search));
                break;
            case '2':
                ListPokemonMenu(Pokedex.GetAllPokemon());
                break;
            default:
                Console.WriteLine("\nInvalid input");
                break;
        }
    }
}
