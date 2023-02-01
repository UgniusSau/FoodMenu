using FoodMenu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.Repositories
{
    public class MealRepository : IMealRepository
    {
        public IQueryable<Meal> GetMeal()
        {
            throw new NotImplementedException();
        }
    }
}
