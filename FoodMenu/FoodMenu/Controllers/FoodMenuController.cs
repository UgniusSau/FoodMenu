using FoodMenu.Data.Models;
using FoodMenu.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu.Controllers
{
    [ApiController]
    [Route("api/v1/meals")]
    public class FoodMenuController : ControllerBase
    {
        const int categoryFilteredMealLimit = 5;
        const int areaFilteredMealLimit = 3;

        private readonly IMealService _mealService;

        public FoodMenuController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Meal?>> GetMeal(string name)
        {
            try
            {
                var meal = await _mealService.GetMeal(name);
                if (meal == null)
                {
                    return BadRequest("Meal not found.");
                }

                meal.MealsByCategory = await _mealService.GetMealsByCategory(meal.Category, categoryFilteredMealLimit);

                return meal;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}