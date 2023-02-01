using FoodMenu.Data.Models;

namespace FoodMenu.Services
{
    public interface IMealService
    {
        public IQueryable<Meal> GetMeal();
    }
}
