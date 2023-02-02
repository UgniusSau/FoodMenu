using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.Data.Models
{
    //Can be done with inheretance, but it messes the json order and puts main meal details at back of json
    public class MealResponse
    {
        public Meal Meal { get; set; }
        public IList<Meal> MealsByCategory { get; set; }
        
        public IList<Meal> MealsByArea { get; set; }
    }
}
