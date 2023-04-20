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
using NyamRecipe.Model;

namespace NyamRecipe.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListDishesPage.xaml
    /// </summary>
    public partial class ListDishesPage : Page
    {
        public ListDishesPage()
        {
            InitializeComponent();

            CbCategory.ItemsSource = App.DB.Category.ToList();
            LvDishes.ItemsSource = App.DB.Dish.ToList();
        }

        private void CookBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = LvDishes.SelectedItem as Dish;
            if (selectedItem == null)
            {
                MessageBox.Show("Выберите блюдо");
                return;
            }
            NavigationService.Navigate(new RecipePage(selectedItem));
        }

        private void Refresh()
        {

            IEnumerable<Dish> filterService = App.DB.Dish;
            if(CbCategory.SelectedIndex != -1)
            {
                filterService = filterService.Where(x => x.Category == CbCategory.SelectedItem);
            }

            if (TbSearh.Text.Length > 0)
            {
                filterService = filterService.Where(x => x.Name.ToLower().StartsWith(TbSearh.Text.ToLower()));
            }
            LvDishes.ItemsSource = filterService.ToList();

        }

        private void CbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void TbSearh_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
