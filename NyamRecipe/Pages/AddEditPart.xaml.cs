using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddEditPart.xaml
    /// </summary>
    public partial class AddEditPart : Page
    {
        Ingredient contextIngridient;
        public AddEditPart(Ingredient ingredient)
        {
            InitializeComponent();
            contextIngridient = ingredient;
            DataContext = contextIngridient;
            CbUnit.ItemsSource = App.DB.Unit.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextIngridient.Name))
            {
                errorMessage += "Введите название\n";
            }
            if (contextIngridient.Cost <= 0)
            {
                errorMessage += "Введите корректную цену\n";
            }
            if (contextIngridient.CostForCount <= 0 )
            {
                errorMessage += "Выберите количество\n";
            }
            if (contextIngridient.Unit == null)
            {
                errorMessage += "Выберите единицу измерения\n";
            }
            if (contextIngridient.CostForCount <= 0)
            {
                errorMessage += "Выберите корректное количество в холодильнике\n";
            }
            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }
            if (contextIngridient.Id == 0)
            {
                App.DB.Ingredient.Add(contextIngridient);
            }
            App.DB.SaveChanges();
            NavigationService.GoBack();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void TextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInputABC(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[A-zА-я,]") == false)
            {
                e.Handled = true;
            }
        }
    }
}
