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
    /// Логика взаимодействия для ListParts.xaml
    /// </summary>
    public partial class ListParts : Page
    {
        public ListParts()
        {
            InitializeComponent();
            if (maxPage * 5 < App.DB.Ingredient.Where(x => x.IsDelete != true).Count())
                maxPage += 1;
            TbCounter.Text = App.DB.Ingredient.Where(x => x.IsDelete != true).Count().ToString();
            Update();
            LblPages.Content = $"{fakePage}/{maxPage}";
            CalculateSummarydData();
            GeneratePageNumbers();
            
        }
        int numberPage = 0;
        int count = 5;
        int maxPage = App.DB.Ingredient.Where(x => x.IsDelete != true).Count() / 5;
        int fakePage = 1;
        int ButtonPage;
        
        private void LinkEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (sender as Hyperlink).DataContext as Ingredient;
            NavigationService.Navigate(new AddEditPart(selectedItem));

        }

        private void LinkDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (sender as Hyperlink).DataContext as Ingredient;
            selectedItem.IsDelete = true;
            App.DB.SaveChanges();
            
        }

        private void BtnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage = 0;
            Update();
            fakePage = 1;
            LblPages.Content = $"{fakePage}/{maxPage}";
        }

        private void BtnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage--;
            fakePage--;
            if (numberPage < 0)
                numberPage = 0;
            if (fakePage < 1)
                fakePage = 1;
            Update();
            
            LblPages.Content = $"{fakePage}/{maxPage}";
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage++;
            fakePage++;
            if(numberPage == maxPage)
            {
                numberPage = maxPage - 1;
                fakePage--;
            }
                
            if (fakePage < maxPage + 1)
            {
                Update();

                LblPages.Content = $"{fakePage}/{maxPage}";
            }
        }

        private void BtnLastPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage = maxPage - 1;
            fakePage = maxPage;
            Update();
            LblPages.Content = $"{fakePage}/{maxPage}";
        }
        private void Update()
        {
            if (fakePage > maxPage)
            {
                fakePage = maxPage;
            }
                
            IEnumerable<Ingredient> partsList = App.DB.Ingredient.Where(x => x.IsDelete != true);
            partsList = partsList.Skip(count * numberPage).Take(count);
            DtGreedient.ItemsSource = partsList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPart(new Ingredient()));
        }
        public void CalculateSummarydData()
        {
            IEnumerable<Ingredient> listIngredients = App.DB.Ingredient.Where(x => x.IsDelete != true);
            double sum = listIngredients.Sum(x => x.Price * x.AvailableCount);
            TbFoodSumm.Text = $"Запасов в холодильнике на сумму: {sum:N2} Руб.";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
            GeneratePageNumbers();
        }

        private void GeneratePageNumbers()
        {
            SPanelPages.Children.Clear();
            for(int i = 1; i <= maxPage; i ++)
            {
                ButtonPage = i;
                Button btn = new Button();
                btn.Content = i;
                btn.Width = 28;
                btn.Margin = new Thickness(10, 0, 0, 0);
                btn.Click += PageButton_Click;
                SPanelPages.Children.Add(btn);
            }
        }
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            string c = b.Content.ToString();
            int a = int.Parse(c) -1;
            numberPage = a;

            Update();
            LblPages.Content = $"{fakePage}/{maxPage}";

        }
    }
}
