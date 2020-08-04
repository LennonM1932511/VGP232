using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokemonLib;

namespace Assignment2c
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PokeDex pokedexLoader;
        private const string pokedexFiles = "POKéDEX Files|*.csv;*.xml;*.json";

        public MainWindow()
        {
            InitializeComponent();

            // Setup Listbox Default Source
            pokedexLoader = new PokeDex();
            PokedexListbox.ItemsSource = pokedexLoader;

            // Setup ComboBox Types, add 'All', and remove 'none'
            List<string> poketypes = new List<string> { "All" };
            poketypes.AddRange(Enum.GetNames(typeof(Pokemon.PokemonType)));
            poketypes.Remove(poketypes[1]);
            TypeFilter.ItemsSource = poketypes;
        }

        // Update listbox and the pokedex count
        public void Update()
        {
            pokedexCount.Content = pokedexLoader.Count().ToString();
            PokedexListbox.Items.Refresh();
        }

        private void OpenPokedexButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "OPEN POKéDEX",
                Filter = pokedexFiles,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFile.ShowDialog() == true)
            {
                pokedexLoader.Load(openFile.FileName);

                // Get Filename without extention and set as Label text
                string[] properName = openFile.SafeFileName.Split('.');
                PokedexNameLabel.Text = properName[0];

                Update();
            }
        }

        private void SavePokedexButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Title = "SAVE POKéDEX",
                Filter = pokedexFiles,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFile.ShowDialog() == true)
            {
                pokedexLoader.Save(saveFile.FileName);

                // Get Filename without extention and set as Label text
                string[] properName = saveFile.SafeFileName.Split('.');
                PokedexNameLabel.Text = properName[0];
            }
        }

        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            PokeDexEditor editor = new PokeDexEditor();
            if (editor.ShowDialog() == true)
            {
                pokedexLoader.Add(editor.TempPokemon);
                Update();
            }
        }

        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            if (PokedexListbox.SelectedIndex != -1)
            {
                PokeDexEditor editor = new PokeDexEditor();
                editor.Setup((Pokemon)PokedexListbox.SelectedItem);
                if (editor.ShowDialog() == true)
                {
                    Update();
                }
            }
        }

        private void Remove_Clicked(object sender, RoutedEventArgs e)
        {
            if (PokedexListbox.SelectedIndex != -1)
            {
                PokeDex pokedexTemp = new PokeDex();
                pokedexTemp = (PokeDex)PokedexListbox.ItemsSource;

                // Remove selected pokemon from all lists
                pokedexTemp.Remove((Pokemon)PokedexListbox.SelectedItem);
                pokedexLoader.Remove((Pokemon)PokedexListbox.SelectedItem);
                Update();
            }

        }

        private void Sort_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton btn)
            {
                // Set pokedexTemp to listbox source
                PokeDex pokedexTemp = new PokeDex();
                pokedexTemp = (PokeDex)PokedexListbox.ItemsSource;

                // Sort both lists
                pokedexTemp.SortBy(btn.Name);
                pokedexLoader.SortBy(btn.Name);
                Update();
            }
        }

        private void TypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // check if filter is selected
            if (TypeFilter.SelectedIndex != 0)
            {
                // Set the selectedType to the correct Enum and filter list by type
                Pokemon.PokemonType selectedType =
                    (Pokemon.PokemonType)Enum.Parse(typeof(Pokemon.PokemonType), TypeFilter.SelectedItem.ToString(), true);
                PokedexListbox.ItemsSource = pokedexLoader.GetAllPokemonOfType(selectedType);
                PokedexListbox.Items.Refresh();
            }
            else
            {
                PokedexListbox.ItemsSource = pokedexLoader;
                Update();
            }
        }
    }
}
