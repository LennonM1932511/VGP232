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
                
        public string sName { get; set; }
        public Category eCategory { get; set; } // enumerated list
        public string sPrice { get; set; } // format: gold, decimal
        public string sWeight { get; set; } // format: pounds, decimal
        public string sDamage { get; set; } // format: XdY
        public DamageType eDamageType { get; set; } // enumerated list
        public string sRange { get; set; } // format: X, or X/Y
        public string sBoosts { get; set; } // format: comma separated
        public string sProperties { get; set; } // format: comma separated
        public string sDescription { get; set; } // text description
        public string sImage { get; set; } // path to optional image
        

        public Weapon()
        {
        }        

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2} gp, {3} lbs, {4} {5}, {6} ft\n{7}, {8}\n{9}\n{10}\n", 
                eCategory.ToString(), sName, sPrice, sWeight, sDamage, eDamageType.ToString(), sRange, 
                sBoosts, sProperties, 
                sDescription, 
                sImage);
        }
    }
}