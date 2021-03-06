﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Assignment2a
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
            pokedex.Load(inputPath);

            actual = pokedex.GetHighestDefense();
            Assert.AreEqual("213", actual.Index);
            Assert.AreEqual("Shuckle", actual.Name);
            Assert.AreEqual(230, actual.Defense);
        }

        [Test]
        public void PokeDex_GetHighestHP_Pokemon()
        {
            // 242 - Blissey, HP: 255
            Pokemon actual = null;
            pokedex.Load(inputPath);
            actual = pokedex.GetHighestHP();
            Assert.AreEqual("242", actual.Index);
            Assert.AreEqual("Blissey", actual.Name);
            Assert.AreEqual(255, actual.HP);
        }

        [Test]
        public void PokeDex_GetHighestAttack_Pokemon()
        {
            // 386.1 - Deoxys (A), Atk: 180
            Pokemon actual = null;
            pokedex.Load(inputPath);
            actual = pokedex.GetHighestAttack();
            Assert.AreEqual("386.1", actual.Index);
            Assert.AreEqual("Deoxys (A)", actual.Name);
            Assert.AreEqual(180, actual.Attack);
        }

        [Test]
        public void PokeDex_GetHighestSpeed_Pokemon()
        {
            // 386.3 - Deoxys (S), Spe: 180
            Pokemon actual = null;
            pokedex.Load(inputPath);
            actual = pokedex.GetHighestSpeed();
            Assert.AreEqual("386.3", actual.Index);
            Assert.AreEqual("Deoxys (S)", actual.Name);
            Assert.AreEqual(180, actual.Speed);
        }

        [Test]
        public void PokeDex_GetHighestSpecialDefense_Pokemon()
        {
            // 213 - Shuckle, SpD: 230
            Pokemon actual = null;
            pokedex.Load(inputPath);
            actual = pokedex.GetHighestSpecialDefense();
            Assert.AreEqual("213", actual.Index);
            Assert.AreEqual("Shuckle", actual.Name);
            Assert.AreEqual(230, actual.SpecialDefense);
        }

        [Test]
        public void PokeDex_FindHighestSpecialAttack_Pokemon()
        {
            // 386.1 - Deoxys (A), SpA: 180
            Pokemon actual = null;
            pokedex.Load(inputPath);
            actual = pokedex.GetHighestSpecialAttack();
            Assert.AreEqual("386.1", actual.Index);
            Assert.AreEqual("Deoxys (A)", actual.Name);
            Assert.AreEqual(180, actual.SpecialAttack);
        }

        [Test]
        public void PokeDex_LoadThatExistAndValid_True()
        {
            Assert.IsTrue(pokedex.Load(inputPath));            
            Assert.AreEqual(663, pokedex.Count);
        }

        [Test]
        public void PokeDex_LoadThatDoesNotExist_FalseAndEmpty()
        {
            // TODO: load returns false, expect an empty pokeDex
            Assert.IsFalse(pokedex.Load("fail.csv"));
            Assert.IsEmpty(pokedex);
        }

        [Test]
        public void PokeDex_SaveWithValuesCanLoad_TrueAndNotEmpty()
        {
            // TODO: save returns true, load returns true, and pokedex is not empty.
            Assert.IsTrue(pokedex.Save(outputPath));
            Assert.IsTrue(pokedex.Load(inputPath));
            Assert.IsNotEmpty(pokedex);
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
            Pokemon expected = null;

            expected = new Pokemon()
            {
                Index = "1",
                Name = "Bulbasaur",
                HP = 45,
                Attack = 49,
                Defense = 49,
                SpecialAttack = 65,
                SpecialDefense = 65,
                Speed = 45,
                Total = 318,
                Type1 = Pokemon.PokemonType.Grass,
                Type2 = Pokemon.PokemonType.Poison
            };

            string line = "1,Bulbasaur,45,49,49,65,65,45,318,Grass,Poison";
            Pokemon actual = null;

            Assert.IsTrue(Pokemon.TryParse(line, out actual));
            Assert.AreEqual(expected.Index, actual.Index);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.HP, actual.HP);
            Assert.AreEqual(expected.Attack, actual.Attack);
            Assert.AreEqual(expected.Defense, actual.Defense);
            Assert.AreEqual(expected.SpecialAttack, actual.SpecialAttack);
            Assert.AreEqual(expected.SpecialDefense, actual.SpecialDefense);
            Assert.AreEqual(expected.Speed, actual.Speed);
            Assert.AreEqual(expected.Total, actual.Total);
            Assert.AreEqual(expected.Type1, actual.Type1);
            Assert.AreEqual(expected.Type2, actual.Type2);
        }

        [Test]
        public void Pokemon_TryParseInvalidLine_FalseNull()
        {
            // TODO: use "1,Bulbasaur,A,B,C,65,65", TryParse returns false, and pokemon is null.
            string line = "1,Bulbasaur,A,B,C,65,65";
            Assert.IsFalse(Pokemon.TryParse(line, out Pokemon pokemon));
            Assert.IsNull(pokemon);
        }
    }
}