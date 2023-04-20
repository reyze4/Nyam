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
    /// Логика взаимодействия для RecipePage.xaml
    /// </summary>
    public partial class RecipePage : Page
    {
        int Quan = 1;

        Dish contextDish;
        int result;
        int fullprice;
        public RecipePage(Dish dish)
        {
            InitializeComponent();
            contextDish = dish;
            DataContext = contextDish;
            TBQuantity.Text = $"{Quan}";

            LVDescription.ItemsSource = App.DB.CookingStage.Where(x => x.DishId == dish.Id).ToList();

            List<Ingredient> ingredientList = new List<Ingredient>();
            List<CookingStage> cooking = new List<CookingStage>();
            var cookingStages = App.DB.CookingStage.Where(x => x.DishId == dish.Id).ToList();

            foreach (var item in cookingStages)
            {
                var ingredientOfStages = App.DB.IngredientOfStage.Where(x => x.CookingStageId == item.Id).ToList();
                cooking.Add(item);
                foreach (var item2 in ingredientOfStages)
                {
                    var ingredient = App.DB.Ingredient.FirstOrDefault(x => x.Id == item2.IngredientId);
                    ingredientList.Add(ingredient);
                }
            }
            CsvGrid.ItemsSource = ingredientList;

            result = (int)cooking.Sum(x => x.TimeInMinutes);
            fullprice = (int)ingredientList.Sum(x => x.Cost);
            TBCookingTime.Text = $"{result} мин.";

            TBGeneralSum.Text = $"Общая стоимость: {fullprice * int.Parse(TBQuantity.Text)} руб.";

        }

        private void LVDescription_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == BPlus)
            {
                Quan++;
                TBQuantity.Text = $"{Quan}";
            }
            else
            {
                if (Quan != 1)
                {
                    Quan--;
                    TBQuantity.Text = $"{Quan}";
                }

            }
            TBCookingTime.Text = $"{result * Quan} мин.";
            TBGeneralSum.Text = $"Общая стоимость: {fullprice * int.Parse(TBQuantity.Text)} руб.";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Блюдо {contextDish.Name} готовиться.\nПримерная готовка {result * Quan} минут");
            NavigationService.Navigate(new ListDishesPage());

        }
    }
}
