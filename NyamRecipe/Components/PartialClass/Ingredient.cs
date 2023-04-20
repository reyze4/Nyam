using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyamRecipe.Components.Model
{
    public partial class Ingredient
    {
        public string PriceColor
        {
            get
            {
                if(CostForCount < 60)
                {
                    return "Green";
                }
                if(CostForCount < 200 & CostForCount > 60)
                {
                    return "Pink";
                }
                else
                {
                    return "Cyan";
                }
            }
        }
        public double Price
        {
            get
            {
                return (double)Cost / CostForCount;
            }
        }

    }
}
