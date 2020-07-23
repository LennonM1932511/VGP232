using System;
using System.Collections.Generic;
using System.IO;

// Assignment 1
// NAME: Lennon Marshall
// STUDENT NUMBER: 1932511
// MARKS: 100/100 Excellent work! Minor issue, there's some trailing spaces in a few of the code blocks, you might want to Format Document i.e. CTRL + K + CTRL + D or F in selection to clean it up.

namespace Assignment2a
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

            // The flag to determine if we need to display the number of entries
            bool displayCount = false;

            // The flag to determine if we need to sort the results via name.
            bool sortEnabled = false;

            // The column name to be used to determine which sort comparison function to use.
            string sortColumnName = string.Empty;

            // The results to be output to a file or to the console
            PokeDex results = new PokeDex();

            for (int i = 0; i < args.Length; i++)
            {
                // h or --help for help to output the instructions on how to use it
                if (args[i] == "-h" || args[i] == "--help")
                {
                    Console.WriteLine("-i <path> or --input <path> : loads the input file path specified (required)");
                    Console.WriteLine("-o <path> or --output <path> : saves result in the output file path specified (optional)");
                    Console.WriteLine("-c or --count : displays the number of entries in the input file (optional).");
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
                            results.Load(inputFile);
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
                            // set the output file to filePath
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
                    results.Save(outputFile);
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
    }
}