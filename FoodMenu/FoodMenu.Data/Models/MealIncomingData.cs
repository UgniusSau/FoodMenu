using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.Data.Models
{
    public class MealIncomingData
    {
        [JsonProperty("meals")]
        public List<Meal> Meals { get; set; }
    }
}
