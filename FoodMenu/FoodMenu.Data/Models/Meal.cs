using Newtonsoft.Json;
using System.Text.Json;

namespace FoodMenu.Data.Models
{
    public class Meal
    {
        [JsonProperty("strMeal")]
        public string Name { get; set; }

        [JsonProperty("strCategory")]
        public string Category { get; set; }

        [JsonProperty("strArea")]
        public string Area { get; set; }
    }
}