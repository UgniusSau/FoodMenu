using FoodMenu.Data.Models;

namespace FoodMenu.Services
{
    public interface IMealService
    {
        Task<Meal?> GetMealDetails(string name);
        Task<IList<Meal>> GetMealsByCategory(string category, int count);
        Task<IList<Meal>> GetMealsByArea(string area, int count);
    }
}