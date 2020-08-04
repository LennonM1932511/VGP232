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
        // Update function for refreshing the listbox and the pokedex count
        public void Update()
        {
            pokedexCount.Content = pokedexLoader.Count().ToString();
            PokedexListbox.Items.Refresh();
        }

        PokeDex pokedexLoader;
        //List<Pokemon> filteredPokemon;

        public MainWindow()
        {
            InitializeComponent();
            pokedexLoader = new PokeDex();
            PokedexListbox.ItemsSource = pokedexLoader;
            List<string> poketypes = new List<string> { "All" };
            poketypes.AddRange(Enum.GetNames(typeof(Pokemon.PokemonType)));
            TypeFilter.ItemsSource = poketypes;
        }

        private void OpenPokedexButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "OPEN POKéDEX";
            openFile.Filter = "POKéDEX Files|*.csv;*.xml;*.json";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFile.ShowDialog() == true)
            {
                pokedexLoader.Load(openFile.FileName);
                string[] properName = openFile.SafeFileName.Split('.');
                PokedexNameLabel.Text = properName[0];
                Update();
            }
        }

        private void SavePokedexButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "SAVE POKéDEX";
            saveFile.Filter = "POKéDEX Files|*.csv;*.xml;*.json";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFile.ShowDialog() == true)
            {
                pokedexLoader.Save(saveFile.FileName);
                string[] properName = saveFile.SafeFileName.Split('.');
                PokedexNameLabel.Text = properName[0];
            }
        }

        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            PokeDexEditor editor = new PokeDexEditor();
            if (editor.ShowDialog() == true)
            {
                pokedexLoader.Add(editor.tempPokemon);
                Update();
            }
        }

        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            if (PokedexListbox.SelectedIndex != -1)
            {
                Pokemon selectedPokemon = pokedexLoader[PokedexListbox.SelectedIndex];

                PokeDexEditor editor = new PokeDexEditor();
                editor.Setup(selectedPokemon);

                if (editor.ShowDialog() == true)
                {
                    pokedexLoader[PokedexListbox.SelectedIndex] = editor.tempPokemon;
                    Update();
                }
            }
        }

        private void Remove_Clicked(object sender, RoutedEventArgs e)
        {
            if (PokedexListbox.SelectedIndex != -1)
            {
                pokedexLoader.RemoveAt(PokedexListbox.SelectedIndex);                
                Update();
            }

        }

        private void Sort_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            if (btn != null)
            {
                pokedexLoader.SortBy(btn.Name);
                Update();
            }            
        }

        private void TypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeFilter.SelectedItem.ToString() == "All")
            {
                PokedexListbox.ItemsSource = pokedexLoader;
                Update();
            }
            else
            {
                Pokemon.PokemonType selectedType = (Pokemon.PokemonType)Enum.Parse(typeof(Pokemon.PokemonType), TypeFilter.SelectedItem.ToString(), true);
                List<Pokemon> typePokemon = pokedexLoader.GetAllPokemonOfType(selectedType);
                PokedexListbox.ItemsSource = typePokemon;
                PokedexListbox.Items.Refresh();
            }
        }
    }
}
