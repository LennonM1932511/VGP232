using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Assignment1
{
    [TestFixture]
    public class UnitTests
    {
        private PokeDex pokedex;
        private string inputPath;
        private string outputPath;

        const string INPUT_FILE = "data2.csv";
        const string OUTPUT_FILE = "output.csv";

        // A helper function to get the directory of where the actual path is.
        private string CombineToAppPath(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
        }

        [SetUp]
        public void SetUp()
        {
            inputPath = CombineToAppPath(INPUT_FILE);
            outputPath = CombineToAppPath(OUTPUT_FILE);
            pokedex = new PokeDex();
        }

        [TearDown]
        public void CleanUp()
        {
            // We remove the output file after we are done.
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }

        // PokeDex Unit Tests
        [Test]
        public void PokeDex_GetHighestDefense_Pokemon()
        {
            // 213 - Shuckle, Def: 230
            Pokemon actual = null;
            // TODO: uncomment this once you have it implemented.
            // actual = pokedex.GetHighestDefense();
            Assert.AreEqual("213", actual.Index);
            Assert.AreEqual("Shuckle", actual.Name);
            Assert.AreEqual("230", actual.Defense);
        }

        [Test]
        public void PokeDex_GetHighestHP_Pokemon()
        {
            // 242 - Blissey, HP: 255
            // TODO: call pokedex.GetHighestHP and confirm the index, name, and hp matches using asserts.
        }

        [Test]
        public void PokeDex_GetHighestAttack_Pokemon()
        {
            // 386.1 - Deoxys (A), Atk: 180
            // TODO: call pokedex.GetHighestAttack and confirm the index, name, and atk matches using asserts.
        }

        [Test]
        public void PokeDex_GetHighestSpeed_Pokemon()
        {
            // 386.3 - Deoxys (S), Spe: 180
            // TODO: add test
        }

        [Test]
        public void PokeDex_GetHighestSpecialDefense_Pokemon()
        {
            // 213 - Shuckle, SpD: 230
            // TODO: add test
        }

        [Test]
        public void PokeDex_FindHighestSpecialAttack_Pokemon()
        {
            // 386.3 - Deoxys (A), SpA: 180
            // TODO: add test
        }


        [Test]
        public void PokeDex_LoadThatExistAndValid_True()
        {
            // TODO: expect pokedex with count of 663.
        }

        [Test]
        public void PokeDex_LoadThatDoesNotExist_FalseAndEmpty()
        {
            // TODO: load returns false, expect an empty pokeDex
        }

        [Test]
        public void PokeDex_SaveWithValuesCanLoad_TrueAndNotEmpty()
        {
            // TODO: save returns true, load returns true, and pokedex is not empty.
        }

        [Test]
        public void PokeDex_SaveEmpty_TrueAndEmpty()
        {
            // After saving an empty pokedex, load the file and expect pokedex to be empty.
            pokedex.Clear();
            Assert.IsTrue(pokedex.Save(outputPath));
            Assert.IsTrue(pokedex.Load(outputPath));
            Assert.IsTrue(pokedex.Count == 0);
        }

        // Pokemon Unit Tests
        [Test]
        public void Pokemon_TryParseValidLine_TruePropertiesSet()
        {
            // TODO: create a pokemon with the stats above set properly
            Pokemon expected = null;
            // TODO: uncomment this once you added the Type1 and Type2
            //expected = new Pokemon()
            //{
            //    Index = "1",
            //    Name = "Bulbasaur",
            //    HP = 45,
            //    Attack = 49,
            //    Defense = 49,
            //    SpecialAttack = 65,
            //    SpecialDefense = 65,
            //    Speed = 45,
            //    Total = 318,
            //    Type1 = PokemonType.Grass,
            //    Type2 = PokemonType.Poison
            //};

            string line = "1,Bulbasaur,45,49,49,65,65,45,318,Grass,Poison";
            Pokemon actual = null;

            // TODO: uncomment this once you have TryParse implemented.
            //Assert.IsTrue(Pokemon.TryParse(line, actual));
            Assert.Equals(expected.Index, actual.Index);
            Assert.Equals(expected.Name, actual.Name);
            Assert.Equals(expected.HP, actual.HP);
            // TODO: check for the rest of the properties, Atk, Def, SpA, SpD, Spe, Total, and Type1 and Type2
        }

        [Test]
        public void Pokemon_TryParseInvalidLine_FalseNull()
        {
            // TODO: use "1,Bulbasaur,A,B,C,65,65", TryParse returns false, and pokemon is null.
        }
    }
}
