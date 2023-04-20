using NyamRecipe.Pages;
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

namespace NyamRecipe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComponentsBtn_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new ListParts());
        }

        private void DishesBtn_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new ListDishesPage());
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
