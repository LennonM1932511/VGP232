using System;
using System.Collections.Generic;
using System.IO;

// Assignment 2b
// NAME: Lennon Marshall
// STUDENT NUMBER: 1932511

namespace Assignment2b
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
                        results.Load(inputFile);
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
                results.SortBy(sortColumnName);
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