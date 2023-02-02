using FoodMenu.Data.Models;
using FoodMenu.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu.Controllers
{
    [ApiController]
    [Route("api/v1/meals")]
    public class FoodMenuController : ControllerBase
    {
        const int CategoryFilteredMealLimit = 5;
        const int AreaFilteredMealLimit = 3;

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

                meal.MealsByCategory = await _mealService.GetMealsByCategory(meal.Category, CategoryFilteredMealLimit);
                meal.MealsByArea = await _mealService.GetMealsByArea(meal.Area, AreaFilteredMealLimit);

                return Ok(meal);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}