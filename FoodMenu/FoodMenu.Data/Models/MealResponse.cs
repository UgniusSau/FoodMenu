﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.Data.Models
{
    public class MealResponse : Meal
    {
        public IList<Meal> MealsByCategory { get; set; }
        
        public IList<Meal> MealsByArea { get; set; }
    }
}
