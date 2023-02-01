using FoodMenu.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FoodMenuController : ControllerBase
    {


        [HttpGet("{name}")]
        public async Task<ActionResult<Meal>> GetMeal(string name)
        {
            try
            {
                

                //change this later to correct return
                return StatusCode(200);
            }
            
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}