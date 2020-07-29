using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2b
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
            bool success = false;
            pokemon = null;
            string[] values = line.Split(',');

            try
            {
                Pokemon temp = new Pokemon();

                // test and set pokemon variables
                temp.Index = values[0];
                temp.Name = values[1];
                int.TryParse(values[2], out int hp);
                temp.HP = hp;
                int.TryParse(values[3], out int atk);
                temp.Attack = atk;
                int.TryParse(values[4], out int def);
                temp.Defense = def;
                int.TryParse(values[5], out int spa);
                temp.SpecialAttack = spa;
                int.TryParse(values[6], out int spd);
                temp.SpecialDefense = spd;
                int.TryParse(values[7], out int spe);
                temp.Speed = spe;
                int.TryParse(values[8], out int total);
                temp.Total = total;

                // check if TypeI string matches a PokemonType
                Enum.TryParse(values[9], out PokemonType type1);
                temp.Type1 = type1;

                // check if TypeII string matches a PokemonType
                Enum.TryParse(values[10], out PokemonType type2);
                temp.Type2 = type2;

                pokemon = temp;
                success = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Parsing Data Failed");
            }

            return success;
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
            // LC: good.
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", Index, Name, HP, Attack, Defense,
                SpecialAttack, SpecialDefense, Speed, Total, Type1.ToString(), Type2 == PokemonType.None ? "" : Type2.ToString());
        }
    }
}