using FoodMenu.Data.Models;

namespace FoodMenu.Services
{
    public interface IMealService
    {
        Task<Meal?> GetMeal(string name);
    }
}
