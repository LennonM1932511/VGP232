using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2a
{
    public class Pokemon
    {
        // LM: Flying, Dark, and Steel were missing from the list in the assignment
        public enum PokemonType
        {
            None, Grass, Fire, Water, Bug, Normal, Poison, Electric, Ground, Fairy,
            Fighting, Psychic, Rock, Ghost, Ice, Dragon, Flying, Dark, Steel
        }

        // Nat,Pokemon,HP,Atk,Def,SpA,SpD,Spe,Total,Type1,Type2
        public string Index { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int Total { get; set; }
        public PokemonType Type1 { get; set; }
        public PokemonType Type2 { get; set; }


        public static bool TryParse(string line, out Pokemon pokemon)
        {
            pokemon = new Pokemon();

            string[] values = line.Split(',');

            if (values.Length != 11)
            {
                Console.WriteLine("Invalid data struture.");
                pokemon = null;
                return false;
            }
            else
            {
                // test and set pokemon variables
                pokemon.Index = values[0];
                pokemon.Name = values[1];
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

                // check if TypeI string matches a PokemonType
                if (Enum.TryParse(values[9], out PokemonType type1))
                {
                    pokemon.Type1 = type1;
                }
                else
                {
                    Console.WriteLine("Invalid Pokemon TypeI: {0}", values[9]);
                    pokemon = null;
                    return false;
                }

                // check if TypeII string matches a PokemonType
                if (Enum.TryParse(values[10], out PokemonType type2))
                {
                    pokemon.Type2 = type2;
                }
                else if (string.IsNullOrEmpty(values[10]))
                {
                    pokemon.Type2 = 0;
                }
                else
                {
                    Console.WriteLine("Invalid Pokemon TypeII: {0}", values[10]);
                    pokemon = null;
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// The Comparator function to check for name
        /// </summary>
        /// <param name="left">Left side Pokemon</param>
        /// <param name="right">Right side Pokemon</param>
        /// <returns> -1 (or any other negative value) for "less than", 0 for "equals", or 1 (or any other positive value) for "greater than"</returns>        
        public static int CompareByPokemonName(Pokemon left, Pokemon right)
        {
            return left.Name.CompareTo(right.Name);
        }

        public static int CompareByIndex(Pokemon left, Pokemon right)
        {
            return left.Index.CompareTo(right.Index);
        }

        public static int CompareByPokemonHP(Pokemon left, Pokemon right)
        {
            return left.HP.CompareTo(right.HP);
        }

        public static int CompareByPokemonAttack(Pokemon left, Pokemon right)
        {
            return left.Attack.CompareTo(right.Attack);
        }

        public static int CompareByPokemonDefense(Pokemon left, Pokemon right)
        {
            return left.Defense.CompareTo(right.Defense);
        }

        public static int CompareBySpecialAttack(Pokemon left, Pokemon right)
        {
            return left.SpecialAttack.CompareTo(right.SpecialAttack);
        }

        public static int CompareBySpecialDefense(Pokemon left, Pokemon right)
        {
            return left.SpecialDefense.CompareTo(right.SpecialDefense);
        }

        public static int CompareByPokemonSpeed(Pokemon left, Pokemon right)
        {
            return left.Speed.CompareTo(right.Speed);
        }

        public static int CompareByPokemonTotal(Pokemon left, Pokemon right)
        {
            return left.Total.CompareTo(right.Total);
        }

        public override string ToString()
        {
            // construct a string to return with the following format
            // Nat,Pokemon,HP,Atk,Def,SpA,SpD,Spe,Total            
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", Index, Name, HP, Attack, Defense,
                SpecialAttack, SpecialDefense, Speed, Total, Type1.ToString(), Type2 == PokemonType.None ? "" : Type2.ToString());
        }
    }
}