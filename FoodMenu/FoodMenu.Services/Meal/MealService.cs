using FoodMenu.Data.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Xml.Linq;

namespace FoodMenu.Services
{
    public class MealService : IMealService
    {
        private const string GetMealByNameLink = @"https://www.themealdb.com/api/json/v1/1/search.php?s={0}";
        private const string GetMealByCategoryLink = @"https://www.themealdb.com/api/json/v1/1/filter.php?c={0}";

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
            
            if(mealContainer.Meals == null)
                return null;

            return mealContainer.Meals[0];
        }

        public async Task<IList<string>> GetMealsByCategory(string category, int count)
        {
            var response = await _httpClient.GetAsync(string.Format(GetMealByCategoryLink, category));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var mealContainer = JsonConvert.DeserializeObject<MealResponse>(responseContent);

            return mealContainer?.Meals.Select(x => x?.Name).Take(count).ToList()!;
        }
    }
}


    

