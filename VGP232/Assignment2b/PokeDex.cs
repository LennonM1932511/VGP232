using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Assignment2b
{
    public class PokeDex : List<Pokemon>, IPeristence, IXmlSerializable, IJsonSerializable, ICsvSerializable
    {
        XmlSerializer serializer;

        public PokeDex()
        {
            serializer = new XmlSerializer(typeof(List<Pokemon>));
        }

        public bool Load(string fileName)
        {
            Clear();
            bool success = false;

            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("No input file specified.");
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine("The file specified does not exist.");
            }
            else
            {
                Console.WriteLine("Loading PokeDex from {0}", fileName);

                string fileExt = Path.GetExtension(fileName);

                // check for compatible file type and load file
                if (fileExt.Equals(".csv", StringComparison.InvariantCultureIgnoreCase))
                {
                    success = LoadCSV(fileName);
                }
                else if (fileExt.Equals(".xml", StringComparison.InvariantCultureIgnoreCase))
                {
                    success = LoadXML(fileName);
                }
                else if (fileExt.Equals(".json", StringComparison.InvariantCultureIgnoreCase))
                {
                    success = LoadJSON(fileName);
                }
                else
                {
                    Console.WriteLine("Invalid file type: {0}", fileExt);
                }
            }
            return success;
        }

        public bool Save(string fileName)
        {
            bool success = false;

            Console.WriteLine("Saving PokeDex to {0}", fileName);

            string fileExt = Path.GetExtension(fileName);

            // check for compatible file type and save file
            if (fileExt.Equals(".csv", StringComparison.InvariantCultureIgnoreCase))
            {
                success = SaveAsCSV(fileName);
            }
            else if (fileExt.Equals(".xml", StringComparison.InvariantCultureIgnoreCase))
            {
                success = SaveAsXML(fileName);
            }
            else if (fileExt.Equals(".json", StringComparison.InvariantCultureIgnoreCase))
            {
                success = SaveAsJSON(fileName);
            }
            else
            {
                Console.WriteLine("Invalid file type: {0}", fileExt);
            }

            return success;
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

        public bool LoadXML(string path)
        {
            Clear();
            bool success = false;
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    List<Pokemon> temp = serializer.Deserialize(reader) as List<Pokemon>;
                    if (temp != null)
                    {
                        this.Clear();
                        this.AddRange(temp);
                    }
                    success = true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An exception occurred attempting to deserialize from XML");
            }
            return success;
        }

        public bool SaveAsXML(string path)
        {
            bool success = false;
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    serializer.Serialize(writer, this);
                    success = true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An exception occurred attempting to serialize to XML");
            }
            return success;
        }

        public bool LoadJSON(string path)
        {
            Clear();
            bool success = false;
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string jsonData = reader.ReadToEnd();
                    PokeDex temp = JsonConvert.DeserializeObject<PokeDex>(jsonData);
                    if (temp != null)
                    {
                        this.Clear();
                        this.AddRange(temp);
                    }
                    success = true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An exception occurred attempting to access {0}", path.ToString());
            }
            return success;
        }

        public bool SaveAsJSON(string path)
        {
            bool success = false;
            try
            {
                string jsonData = JsonConvert.SerializeObject(this);

                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(jsonData);
                    success = true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An exception occurred attempting to access {0}", path.ToString());
            }
            return success;
        }

        public bool LoadCSV(string path)
        {
            Clear();
            using (StreamReader reader = new StreamReader(path))
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

        public bool SaveAsCSV(string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine("Nat,Pokemon,HP,Atk,Def,SpA,SpD,Spe,Total,TypeI,TypeII");

                    // write data
                    for (int i = 0; i < Count; i++)
                    {
                        writer.WriteLine(this[i]); // write line
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An exception occurred attempting to access {0}", path.ToString());
                return false;
            }
        }
    }
}