using FoodMenu.Data.Models;
using FoodMenu.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FoodMenuController : ControllerBase
    {
        private readonly IMealService _mealService;

        public FoodMenuController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Meal>> GetMeal(string name)
        {
            var result = _mealService.GetMeal();

            if (!result.Any()) return NotFound();
            return Ok(result);
        }
    }
}