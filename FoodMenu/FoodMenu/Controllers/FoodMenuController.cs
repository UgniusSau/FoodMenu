using FoodMenu.Data.Models;
using FoodMenu.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu.Controllers
{
    [ApiController]
    [Route("api/v1/meals")]
    public class FoodMenuController : ControllerBase
    {
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
                    return NotFound();
                }

                return meal;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}