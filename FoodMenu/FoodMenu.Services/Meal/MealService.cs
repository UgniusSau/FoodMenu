using FoodMenu.Data.Models;
using Newtonsoft.Json;

namespace FoodMenu.Services
{
    public class MealService : IMealService
    {
        private const string GetMealByNameLink = @"https://www.themealdb.com/api/json/v1/1/search.php?s={0}";

        private readonly HttpClient _httpClient;

        public MealService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<Meal?> GetMeal(string name)
        {
            var response = await _httpClient.GetAsync(string.Format(GetMealByNameLink, name));
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var mealContainer = JsonConvert.DeserializeObject<MealResponse>(responseContent);
            if(mealContainer.meals == null)
                return null;
            return mealContainer.meals[0];
        }
    }
}


    

