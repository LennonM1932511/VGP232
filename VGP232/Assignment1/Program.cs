using System;
using System.Collections.Generic;
using System.IO;

// Assignment 1
// NAME: Lennon Marshall
// STUDENT NUMBER: 1932511
// MARKS: 100/100 Excellent work! Minor issue, there's some trailing spaces in a few of the code blocks, you might want to Format Document i.e. CTRL + K + CTRL + D or F in selection to clean it up.

namespace Assignment1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Variables and flags

            // The path to the input file to load.
            string inputFile = string.Empty;

            // The path of the output file to save.
            string outputFile = string.Empty;

            // The flag to determine if we overwrite the output file or append to it.
            bool appendToFile = false;

            // The flag to determine if we need to display the number of entries
            bool displayCount = false;

            // The flag to determine if we need to sort the results via name.
            bool sortEnabled = false;
            
            // The column name to be used to determine which sort comparison function to use.
            string sortColumnName = string.Empty;

            // The results to be output to a file or to the console
            List<Pokemon> results = new List<Pokemon>();

            for (int i = 0; i < args.Length; i++)
            {
                // h or --help for help to output the instructions on how to use it
                if (args[i] == "-h" || args[i] == "--help")
                {
                    Console.WriteLine("-i <path> or --input <path> : loads the input file path specified (required)");
                    Console.WriteLine("-o <path> or --output <path> : saves result in the output file path specified (optional)");

                    // DONE: help info for count                    
                    Console.WriteLine("-c or --count : displays the number of entries in the input file (optional).");

                    // DONE: help info for append                    
                    Console.WriteLine("-a or --append : enables append mode when writing to an existing output file (optional)");

                    // DONE: help info for sort                    
                    Console.WriteLine("-s or --sort <column name> : outputs the results sorted by column name");                    

                    break;
                }
                else if (args[i] == "-i" || args[i] == "--input")
                {
                    if (args.Length > i + 1)
                    {
                        // validation to make sure we do have an argument after the flag
                        ++i;
                        inputFile = args[i];

                        if (string.IsNullOrEmpty(inputFile))
                        {
                            // DONE: print no input file specified.
                            Console.WriteLine("No input file specified.");
                        }
                        else if (!File.Exists(inputFile))
                        {
                            // DONE: print the file specified does not exist.
                            Console.WriteLine("The file specified does not exist.");
                        }
                        else
                        {
                            // This function returns a List<Pokemon> once the data is parsed.                            
                            Console.WriteLine("Loading data from {0}", inputFile);
                            results = Parse(inputFile);
                        }
                    }
                }
                else if (args[i] == "-s" || args[i] == "--sort")
                {
                    // set the sortEnabled flag and see if the next argument is set for the column name
                    sortEnabled = true;
                    if (args.Length > i + 1)
                    {
                        // validation to make sure we do have an argument after the flag
                        ++i;
                        // LC: there's some trailing spacing, you can use CTRL + K and CTRL + F to remove it.
                        sortColumnName = args[i];                        
                    }                    
                }
                else if (args[i] == "-c" || args[i] == "--count")
                {
                    displayCount = true;
                }
                else if (args[i] == "-a" || args[i] == "--append")
                {
                    // DONE: set the appendToFile flag
                    appendToFile = true;
                }
                else if (args[i] == "-o" || args[i] == "--output")
                {
                    // check for required input file first
                    if (string.IsNullOrEmpty(inputFile))
                    {
                        //no input file specified.
                        Console.WriteLine("No input file specified.");
                    }

                    // validation to make sure we do have an argument after the flag
                    if (args.Length > i + 1)
                    {
                        // increment the index.
                        ++i;
                        string filePath = args[i];
                        if (string.IsNullOrEmpty(filePath))
                        {
                            // DONE: print No output file specified.
                            Console.WriteLine("No output file specified.");
                        }
                        else
                        {
                            // DONE: set the output file to the outputFile
                            outputFile = filePath;
                            Console.WriteLine("Output file set to {0}", filePath);
                        }
                    }
                }                
                else
                {
                    Console.WriteLine("The argument Arg[{0}] = [{1}] is invalid", i, args[i]);
                }
            }

            if (sortEnabled)
            {              
                // LC: good.
                // determine the column name to trigger a different sort.
                if (string.IsNullOrEmpty(sortColumnName) || sortColumnName.Equals("index", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Sorts the list based off of the Pokemon index.                    
                    results.Sort(Pokemon.CompareByIndex);
                }
                else if (sortColumnName.Equals("name", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Sorts the list based off of the Pokemon name.
                    results.Sort(Pokemon.CompareByPokemonName);
                }
                else if (sortColumnName.Equals("hp", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Sorts the list based off of the Pokemon HP.
                    results.Sort(Pokemon.CompareByPokemonHP);
                }
                else if (sortColumnName.Equals("attack", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Sorts the list based off of the Pokemon Attack.
                    results.Sort(Pokemon.CompareByPokemonAttack);
                }
                else if (sortColumnName.Equals("defense", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Sorts the list based off of the Pokemon Defense.
                    results.Sort(Pokemon.CompareByPokemonDefense);
                }
                else if (sortColumnName.Equals("specialattack", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Sorts the list based off of the Pokemon SpecialAttack.
                    results.Sort(Pokemon.CompareBySpecialAttack);
                }
                else if (sortColumnName.Equals("specialdefense", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Sorts the list based off of the Pokemon SpecialDefense.
                    results.Sort(Pokemon.CompareBySpecialDefense);
                }
                else if (sortColumnName.Equals("speed", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Sorts the list based off of the Pokemon Speed.
                    results.Sort(Pokemon.CompareByPokemonSpeed);
                }
                else if (sortColumnName.Equals("total", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Sorts the list based off of the Pokemon Total.
                    results.Sort(Pokemon.CompareByPokemonTotal);
                }
                else
                {
                    Console.WriteLine("The column name, [{0}] is invalid", sortColumnName);
                }
            }

            if (results.Count > 0)
            {                
                if (!string.IsNullOrEmpty(outputFile))
                {                    
                    FileStream fs;

                    // create flag for not writing header in Append mode
                    bool writeHeader = true;

                    if (appendToFile && File.Exists((outputFile)))
                    {                        
                        writeHeader = false;
                        fs = File.Open(outputFile, FileMode.Append);
                        Console.WriteLine("Appending to file");
                    }
                    else
                    {
                        fs = File.Open(outputFile, FileMode.Create);
                        Console.WriteLine("Creating new file");
                    }

                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        // write data header if creating new file
                        if (writeHeader)
                        {
                            // LC: excellent to add the header back in.
                            writer.WriteLine("Nat,Pokemon,HP,Atk,Def,SpA,SpD,Spe,Total");
                        }                        

                        // write data
                        for (int i = 0; i < results.Count; i++)
                        {                            
                            writer.WriteLine(results[i]); // write line
                        }
                        
                    }
                }
                else
                {
                    // print out each line of data
                    for (int i = 0; i < results.Count; i++)
                    {
                        Console.WriteLine(results[i]);
                    }

                    // the count should be shown after the results
                    if (displayCount)
                    {                        
                        Console.WriteLine("There are {0} entries", results.Count);
                    }                    
                }
            }
            Console.WriteLine("Done!");
        }

        /// <summary>
        /// Reads the file and line by line parses the data into a List of Pokemons
        /// </summary>
        /// <param name="fileName">The path to the file</param>
        /// <returns>The list of pokemons</returns>
        public static List<Pokemon> Parse(string fileName)
        {       
            List<Pokemon> output = new List<Pokemon>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                // Skip the first line because header does not need to be parsed.
                // Nat,Pokemon,HP,Atk,Def,SpA,SpD,Spe,Total
                string header = reader.ReadLine();

                // The rest of the lines looks like the following:
                // 1,Bulbasaur,45,49,49,65,65,45,318
                while (reader.Peek() > 0)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    Pokemon pokemon = new Pokemon();

                    // check if there are 9 values
                    // LC: good.
                    if (values.Length != 9)
                    {
                        Console.WriteLine("Invalid data struture.");
                    }
                    else
                    {
                        // test and set pokemon variables
                        pokemon.Index = values[0];
                        pokemon.Name = values[1];
                        // LC: good.
                        int.TryParse(values[2], out int hp);
                        pokemon.HP = hp;
                        int.TryParse(values[3], out int atk);
                        pokemon.Attack = atk;
                        int.TryParse(values[4], out int def);
                        pokemon.Defense = def;
                        int.TryParse(values[5], out int spa);
                        pokemon.SpecialAttack = spa;
                        int.TryParse(values[6], out int spd);
                        pokemon.SpecialDefense = spd;
                        int.TryParse(values[7], out int spe);
                        pokemon.Speed = spe;
                        int.TryParse(values[8], out int total);
                        pokemon.Total = total;
                    }
                    output.Add(pokemon);
                }
            }
            return output;
        }
    }
}