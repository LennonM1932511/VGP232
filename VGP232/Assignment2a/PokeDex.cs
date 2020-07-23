﻿using System;
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

        Pokemon GetHighestDefense()
        {
            throw new NotImplementedException();
        }

        Pokemon GetHighestAttack()
        {
            throw new NotImplementedException();
        }

        Pokemon GetHighestHp()
        {
            throw new NotImplementedException();
        }

        Pokemon GetHighestMaxCp()
        {
            throw new NotImplementedException();
        }

        List<Pokemon> GetAllPokemonOfType(Pokemon.PokemonType type)
        {
            throw new NotImplementedException();
        }

        void SortBy(string columnName)
        {

        }
    }
}
