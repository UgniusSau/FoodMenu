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
        public async Task<ActionResult<MealResponse?>> GetMeal(string name)
        {
            try
            {
                var meal = await _mealService.GetMealDetails(name);
                if (meal == null)
                {
                    return BadRequest("Meal not found.");
                }

                var mealsByCategory = await _mealService.GetMealsByCategory(meal.Category, CategoryFilteredMealLimit);
                var mealsByArea = await _mealService.GetMealsByArea(meal.Area, AreaFilteredMealLimit);

                var mealResponse = new MealResponse
                {
                    Meal = meal,
                    MealsByCategory = mealsByCategory,
                    MealsByArea = mealsByArea
                };

                return Ok(mealResponse);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}