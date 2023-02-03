using Newtonsoft.Json;

namespace FoodMenu.Data.Models
{
    public class MealIncomingData
    {
        [JsonProperty("meals")]
        public List<Meal> Meals { get; set; }
    }
}
