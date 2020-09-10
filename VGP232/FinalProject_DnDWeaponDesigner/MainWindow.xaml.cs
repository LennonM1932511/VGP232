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
using WeaponLib;

namespace FinalProject_DnDWeaponDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WeaponList weaponlistLoader;
        private const string weaponDesignerFiles = "Weapon Designer Files|*.xml;*.json";

        public MainWindow()
        {
            InitializeComponent();

            // Setup Listbox Default Source
            weaponlistLoader = new WeaponList();
            dgWeaponList.ItemsSource = weaponlistLoader;

            // Setup ComboBox with 'All' added
            List<string> weaponCategories = new List<string> { "All" };
            weaponCategories.AddRange(Enum.GetNames(typeof(Weapon.Category)));            
            cbCategory.ItemsSource = weaponCategories;

            Weapon testSword = new Weapon();
            testSword.sName = "Shortsword";
            testSword.eCategory = Weapon.Category.Martial_Melee;
            testSword.sPrice = "10";
            testSword.sDamage = "1d6";
            testSword.eDamageType = Weapon.DamageType.Piercing;
            testSword.sRange = "5";
            testSword.sWeight = "2";
            testSword.sProperties = "Finesse, Light";            
            testSword.sDescription = "A sword of a class generally shorter than one meter, but longer than a dagger.";
            testSword.sImage = "images/shortsword.png";

            Weapon testSword2 = new Weapon();
            testSword2.sName = "Longsword";
            testSword2.eCategory = Weapon.Category.Martial_Melee;
            testSword2.sPrice = "15";
            testSword2.sDamage = "1d8";
            testSword2.eDamageType = Weapon.DamageType.Slashing;
            testSword2.sRange = "5";
            testSword2.sWeight = "3";
            testSword2.sProperties = "Versatile (1d10)";            
            testSword2.sDescription = "A sword of a class generally shorter than one meter, but longer than a dagger.";
            testSword2.sImage = "/images/longsword.png";

            weaponlistLoader.Add(testSword);            
            weaponlistLoader.Add(testSword2);
            weaponlistLoader.Add(testSword);
            weaponlistLoader.Add(testSword2);
            weaponlistLoader.Add(testSword);
            weaponlistLoader.Add(testSword2);

        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
