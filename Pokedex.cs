using System;

namespace pokedex
{
    class Pokedex
    {
        public static bool AddPokemon(Pokemon pokemon)
        {
            List<Pokemon> pokemons = ReadPokemon();

            foreach (Pokemon p in pokemons)
            {
                if (p.ID == pokemon.ID)
                {
                    Console.WriteLine("Pokemon ID already exists in the pokedex");
                    return false;
                }
            }

            if (pokemons.Contains(pokemon))
            {
                Console.WriteLine("Pokemon already exists in the pokedex");
                return false;
            }

            pokemons.Add(pokemon);

            using (StreamWriter sw = new StreamWriter("pokemon.csv"))
            {
                WritePokemon(pokemons);
            }

            return true;
        }

        public static bool EditPokemon(int PokemonID)
        {
            List<Pokemon> pokemons = ReadPokemon();

            foreach (Pokemon p in pokemons)
            {
                if (p.ID == PokemonID)
                {
                    Pokemon modifiedPokemon = ModifyPokemon(p);
                    WritePokemon(pokemons);
                    return true;
                }
            }

            Console.WriteLine("Pokemon ID does not exist in the pokedex");
            return false;
        }

        public static bool DeletePokemon(int PokemonID)
        {
            List<Pokemon> pokemons = ReadPokemon();

            foreach (Pokemon p in pokemons)
            {
                if (p.ID == PokemonID)
                {
                    pokemons.Remove(p);
                    WritePokemon(pokemons);
                    return true;
                }
            }

            Console.WriteLine("Pokemon ID does not exist in the pokedex");
            return false;
        }

        public static List<Pokemon> GetPokemonBySearch(string search)
        {
            List<Pokemon> searchResults = SearchPokemon(search);

            return searchResults;
        }

        public static void GetPokemonByID(int PokemonID)
        {
            List<Pokemon> pokemons = ReadPokemon();

            foreach (Pokemon p in pokemons)
            {
                if (p.ID == PokemonID)
                {
                    Console.WriteLine($"ID: {p.ID}, Name: {p.Name}, Type: {p.Type}, Base Stat Total: {p.BaseStatTotal}");
                    return;
                }
            }

            Console.WriteLine("Pokemon ID does not exist in the pokedex");
        }

        public static List<Pokemon> GetAllPokemon()
        {
            List<Pokemon> pokemons = ReadPokemon();
            return pokemons;
        }

        protected static List<Pokemon> ReadPokemon()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            using (StreamReader sr = new StreamReader("pokemon.csv"))
            {


                string file = sr.ReadToEnd();

                // reading from CSV
                string[] lines = file.Split("\n");


                foreach (string line in lines)
                {
                    if (line == "")
                    {
                        continue;
                    }

                    string[] values = line.Split(",");
                    Pokemon pokemon = new Pokemon();
                    pokemon.ID = Int32.Parse(values[0]);
                    pokemon.Name = values[1];
                    pokemon.Type = values[2];
                    pokemon.BaseStatTotal = Int32.Parse(values[3]);
                    pokemons.Add(pokemon);
                }
            }
            return pokemons;
        }

        protected static void WritePokemon(List<Pokemon> pokemons)
        {

            // Delete file and make it empty
            File.Delete("pokemon.csv");
            File.Create("pokemon.csv").Close();

            using (StreamWriter sw = new StreamWriter("pokemon.csv"))
            {

                foreach (Pokemon pokemon in pokemons)
                {
                    sw.WriteLine($"{pokemon.ID},{pokemon.Name},{pokemon.Type},{pokemon.BaseStatTotal}");
                }
            }
        }

        protected static Pokemon ModifyPokemon(Pokemon pokemon)
        {
            // Ask what to modify
            Console.WriteLine("What would you like to modify?");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Type");
            Console.WriteLine("3. Base Stat Total");
            Console.WriteLine("4. All");

            int choice = Int32.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter new name: ");
                    pokemon.Name = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Enter new type: ");
                    pokemon.Type = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Enter new base stat total: ");
                    pokemon.BaseStatTotal = Int32.Parse(Console.ReadLine());
                    break;
                case 4:
                    Console.WriteLine("Enter new name: ");
                    pokemon.Name = Console.ReadLine();
                    Console.WriteLine("Enter new type: ");
                    pokemon.Type = Console.ReadLine();
                    Console.WriteLine("Enter new base stat total: ");
                    pokemon.BaseStatTotal = Int32.Parse(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            return pokemon;
        }

        protected static List<Pokemon> SearchPokemon(string search)
        {
            List<Pokemon> pokemons = ReadPokemon();
            List<Pokemon> searchResults = new List<Pokemon>();

            foreach (Pokemon p in pokemons)
            {
                if (p.Name.Contains(search) || p.Type.Contains(search))
                {
                    searchResults.Add(p);
                }
            }

            return searchResults;
        }
    }
}
