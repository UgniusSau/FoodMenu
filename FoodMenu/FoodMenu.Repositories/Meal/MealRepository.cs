using FoodMenu.Data.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FoodMenu.Repositories
{
    public class MealRepository : IMealRepository
    {
        private const string GetMealByNameLink = @"https://www.themealdb.com/api/json/v1/1/search.php?s={0}";

        private readonly HttpClient _httpClient;

        public MealRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Meal?> GetMealDetails(string name)
        {
            var response = await _httpClient.GetAsync(string.Format(GetMealByNameLink, name));
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            var mealContainer = JsonConvert.DeserializeObject<MealResponse>(responseContent);



            return mealContainer.meals[0];


        }
    }
}
