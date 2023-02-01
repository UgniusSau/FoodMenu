using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.Data.Models
{
    public class Meal
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Area { get; set; }
        public IQueryable<Meal> SameCategoryMeals { get; set; }
        public IQueryable<Meal> SameAreaMeals { get; set; }
    }
}
