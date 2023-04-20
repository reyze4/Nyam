using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyamRecipe.Components.Model
{
    public partial class Dish
    {
        public double ServicePrice
        {
            get
            {
                var allIngredients = CookingStage.SelectMany(x => x.IngredientOfStage).ToList();
                double totalSum = allIngredients.Sum(x => x.Quantity * x.Ingredient.CostForCount);
                double price = totalSum / ServingQuantity;
                price = price % 0.1;
                return price;
            }
        }
    }
}
