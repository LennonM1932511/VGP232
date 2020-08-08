using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PokemonLib;

namespace Assignment2c
{
    /// <summary>
    /// Interaction logic for PokeDexEditor.xaml
    /// </summary>
    public partial class PokeDexEditor : Window
    {
        public Pokemon TempPokemon { get; set; }

        // LC: excellent.
        // Recalculate and return Total
        public int UpdateTotal()
        {
            return TempPokemon.HP
                + TempPokemon.Attack
                + TempPokemon.Defense
                + TempPokemon.Speed
                + TempPokemon.SpecialAttack
                + TempPokemon.SpecialDefense;
        }

        public PokeDexEditor()
        {
            InitializeComponent();

            // Populate comboboxes with Pokemon Types
            type1Combo.ItemsSource = Enum.GetNames(typeof(Pokemon.PokemonType));
            type2Combo.ItemsSource = Enum.GetNames(typeof(Pokemon.PokemonType));

            TempPokemon = new Pokemon();
            DataContext = TempPokemon;
            Title = "ADD POKéMON";
            SubmitButton.Content = "ADD";
        }

        public void Setup(Pokemon pokemon)
        {
            TempPokemon = pokemon;
            DataContext = TempPokemon;
            Title = "EDIT POKéMON";
            SubmitButton.Content = "SAVE";
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Save_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Update the total whenever data in textboxes change
            TotalBox.Text = UpdateTotal().ToString();
            TempPokemon.Total = UpdateTotal();
        }
    }
}
