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

namespace Assignment2c
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenPokedexButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "OPEN POKéDEX";
            openFile.Filter = "POKéDEX Files|*.csv;*.xml;*.json";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFile.ShowDialog() == true)
            {
                MessageBox.Show(openFile.FileName);
                string[] properName = openFile.SafeFileName.Split('.');
                PokedexNameLabel.Text = properName[0];
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
                MessageBox.Show(saveFile.FileName);
                string[] properName = saveFile.SafeFileName.Split('.');
                PokedexNameLabel.Text = properName[0];
            }
        }

        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            PokeDexEditor editor = new PokeDexEditor();
            if (editor.ShowDialog() == true)
            {
                MessageBox.Show(editor.tempPokemon.ToString());
            }
        }

        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
