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
        public Pokemon tempPokemon { get; set; }

        public PokeDexEditor()
        {
            InitializeComponent();
            type1Combo.ItemsSource = Enum.GetNames(typeof(Pokemon.PokemonType));
            type2Combo.ItemsSource = Enum.GetNames(typeof(Pokemon.PokemonType));

            tempPokemon = new Pokemon();
            this.DataContext = tempPokemon;
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
    }
}
