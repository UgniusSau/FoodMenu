using FoodMenu.Data.Models;
using FoodMenu.Repositories;

namespace FoodMenu.Services
{
    public class MealService : IMealService
    {

        private readonly IMealRepository _mealRepository;

        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }
        public IQueryable<Meal> GetMeal()
        {
            return _mealRepository.GetMeal();
        }

    }
}
