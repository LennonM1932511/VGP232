using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WeaponLib
{
    public class WeaponList : List<Weapon>, IPeristence, IXmlSerializable, IJsonSerializable
    {
        XmlSerializer serializer;

        public WeaponList()
        {
            serializer = new XmlSerializer(typeof(List<Weapon>));
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
                Console.WriteLine("Loading WeaponList from {0}", fileName);

                string fileExt = Path.GetExtension(fileName);

                // check for compatible file type and load file                
                if (fileExt.Equals(".xml", StringComparison.InvariantCultureIgnoreCase))
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

            Console.WriteLine("Saving WeaponList to {0}", fileName);

            string fileExt = Path.GetExtension(fileName);

            // check for compatible file type and save file            
            if (fileExt.Equals(".xml", StringComparison.InvariantCultureIgnoreCase))
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

        public List<Weapon> GetWeaponCategory(Weapon.Category category)
        {
            // create list to save weapons of same category
            WeaponList weapons = new WeaponList();

            // check each weapon for the matching category
            foreach (Weapon weapon in this)
            {
                if (weapon.eCategory == category)
                {
                    weapons.Add(weapon);
                }
            }

            // return a WeaponList containing only weapons of matching category
            return weapons;
        }        

        public bool LoadXML(string path)
        {
            Clear();
            bool success = false;
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    List<Weapon> temp = serializer.Deserialize(reader) as List<Weapon>;
                    if (temp != null)
                    {
                        Clear();
                        AddRange(temp);
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
                    WeaponList temp = JsonConvert.DeserializeObject<WeaponList>(jsonData);
                    if (temp != null)
                    {
                        Clear();
                        AddRange(temp);
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
    }
}