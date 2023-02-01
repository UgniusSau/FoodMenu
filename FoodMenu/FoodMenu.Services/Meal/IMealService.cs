using FoodMenu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.Services
{
    public interface IMealService
    {
        Task<Meal?> GetMeal(string name);
    }
}
