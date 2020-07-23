using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment2a
{
    public class PokeDex : List<Pokemon>, IPeristence
    {
        public bool Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("No input file specified.");
                return false;
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine("The file specified does not exist.");
                return false;
            }
            else
            {
                Console.WriteLine("Loading data from {0}", fileName);
                using (StreamReader reader = new StreamReader(fileName))
                {
                    // Skip the first line because header does not need to be parsed.                
                    string header = reader.ReadLine();

                    // Parse each line and add to list
                    while (reader.Peek() > 0)
                    {
                        if (Pokemon.TryParse(reader.ReadLine(), out Pokemon pokemon))
                        {
                            this.Add(pokemon);
                        }
                    }
                }
                return true;
            }
        }

        public bool Save(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("Nat,Pokemon,HP,Atk,Def,SpA,SpD,Spe,Total,TypeI,TypeII");

                // write data
                for (int i = 0; i < Count; i++)
                {
                    writer.WriteLine(this[i]); // write line
                }

                writer.Close();
            }
            return true;
        }

        public Pokemon GetHighestDefense()
        {
            this.Sort(Pokemon.CompareByPokemonDefense);

            return this[index: Count - 1];
        }

        public Pokemon GetHighestAttack()
        {
            this.Sort(Pokemon.CompareByPokemonAttack);

            return this[Count - 1];
        }

        public Pokemon GetHighestHP()
        {
            this.Sort(Pokemon.CompareByPokemonHP);

            return this[Count - 1];
        }

        public Pokemon GetHighestSpeed()
        {
            this.Sort(Pokemon.CompareByPokemonSpeed);

            return this[Count - 1];
        }

        public Pokemon GetHighestTotal()
        {
            this.Sort(Pokemon.CompareByPokemonTotal);

            return this[Count - 1];
        }

        public Pokemon GetHighestSpecialAttack()
        {
            this.Sort(Pokemon.CompareBySpecialAttack);

            return this[Count - 1];
        }

        public Pokemon GetHighestSpecialDefense()
        {
            this.Sort(Pokemon.CompareBySpecialDefense);

            return this[Count - 1];
        }

        List<Pokemon> GetAllPokemonOfType(Pokemon.PokemonType type)
        {
            // create list to save pokemon of same type
            PokeDex pokemons = new PokeDex();

            // check each pokemon for the matching type
            foreach (Pokemon pokemon in this)
            {
                if (pokemon.Type1 == type || pokemon.Type2 == type)
                {
                    pokemons.Add(pokemon);
                }
            }

            // return a PokeDex containing only pokemon of matching type
            return pokemons;
        }

        public void SortBy(string columnName)
        {
            // determine the column name to trigger a different sort.
            if (string.IsNullOrEmpty(columnName) || columnName.Equals("index", StringComparison.InvariantCultureIgnoreCase))
            {
                // Sorts the list based off of the Pokemon index.                    
                Sort(Pokemon.CompareByIndex);
            }
            else if (columnName.Equals("name", StringComparison.InvariantCultureIgnoreCase))
            {
                // Sorts the list based off of the Pokemon name.
                Sort(Pokemon.CompareByPokemonName);
            }
            else if (columnName.Equals("hp", StringComparison.InvariantCultureIgnoreCase))
            {
                // Sorts the list based off of the Pokemon HP.
                Sort(Pokemon.CompareByPokemonHP);
            }
            else if (columnName.Equals("attack", StringComparison.InvariantCultureIgnoreCase))
            {
                // Sorts the list based off of the Pokemon Attack.
                Sort(Pokemon.CompareByPokemonAttack);
            }
            else if (columnName.Equals("defense", StringComparison.InvariantCultureIgnoreCase))
            {
                // Sorts the list based off of the Pokemon Defense.
                Sort(Pokemon.CompareByPokemonDefense);
            }
            else if (columnName.Equals("specialattack", StringComparison.InvariantCultureIgnoreCase))
            {
                // Sorts the list based off of the Pokemon SpecialAttack.
                Sort(Pokemon.CompareBySpecialAttack);
            }
            else if (columnName.Equals("specialdefense", StringComparison.InvariantCultureIgnoreCase))
            {
                // Sorts the list based off of the Pokemon SpecialDefense.
                Sort(Pokemon.CompareBySpecialDefense);
            }
            else if (columnName.Equals("speed", StringComparison.InvariantCultureIgnoreCase))
            {
                // Sorts the list based off of the Pokemon Speed.
                Sort(Pokemon.CompareByPokemonSpeed);
            }
            else if (columnName.Equals("total", StringComparison.InvariantCultureIgnoreCase))
            {
                // Sorts the list based off of the Pokemon Total.
                Sort(Pokemon.CompareByPokemonTotal);
            }
            else
            {
                Console.WriteLine("The column name, [{0}] is invalid", columnName);
            }
        }
    }
}