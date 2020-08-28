using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponLib
{
    [Serializable]
    public class Weapon
    {
        public enum Category
        {
            Simple_Melee, Simple_Ranged, Martial_Melee, Martial_Ranged
        }

        public enum DamageType
        {
            Bludgeoning, Piercing, Slashing
        }
                
        public string Name { get; set; }
        public Category category { get; set; }
        public string Price { get; set; } // format: gold, decimal
        public string Weight { get; set; } // format: pounds, decimal
        public string Damage { get; set; } // format: XdY
        public DamageType damageType { get; set; }
        public string Range { get; set; } // format: X, or X/Y
        public string Boosts { get; set; } // format: comma separated
        public string Properties { get; set; } // format: comma separated
        public string Description { get; set; }
        public string Image { get; set; } // path to optional image
        

        public Weapon()
        {
        }        

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2} gp, {3} lbs, {4} {5}, {6} ft\n{7}, {8}\n{9}\n{10}\n", 
                category.ToString(), Name, Price, Weight, Damage, damageType.ToString(), Range, 
                Boosts, Properties, 
                Description, 
                Image);
        }
    }
}