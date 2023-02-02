using FoodMenu.Data.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace FoodMenu.Services
{
    public class MealService : IMealService
    {
        private const string GetMealByNameLink = @"https://www.themealdb.com/api/json/v1/1/search.php?s={0}";
        private const string GetMealByCategoryLink = @"https://www.themealdb.com/api/json/v1/1/filter.php?c={0}";
        private const string GetMealByAreaLink = @"https://www.themealdb.com/api/json/v1/1/filter.php?a={0}";

        private readonly HttpClient _httpClient;

        public MealService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Meal> GetMealDetails(string name)
        {
            var response = await _httpClient.GetAsync(string.Format(GetMealByNameLink, name));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var mealContainer = JsonConvert.DeserializeObject<FilteredMeals>(responseContent);

            if (mealContainer.Meals == null)
                return null;

            return mealContainer.Meals[0];
        }

        public async Task<IList<Meal>> GetMealsByCategory(string category, int count)
        {
            var response = await _httpClient.GetAsync(string.Format(GetMealByCategoryLink, category));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var mealContainer = JsonConvert.DeserializeObject<FilteredMeals>(responseContent);
            if (mealContainer == null)
            {
                // can also be created to not allocate memory Enumerable.Empty<Meal>().ToList();
                mealContainer.Meals = new List<Meal>();
            }
            else
            {
                mealContainer.Meals = mealContainer.Meals.Take(count).ToList();
            }

            mealContainer.Meals.ForEach(meal =>
            {
                var mealDetails = GetMealDetails(meal.Name).Result;
                meal.Category = mealDetails.Category;
                meal.Area = mealDetails.Area;
            });

            return mealContainer.Meals;
        }

        public async Task<IList<Meal>> GetMealsByArea(string area, int count)
        {
            var response = await _httpClient.GetAsync(string.Format(GetMealByAreaLink, area));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var mealContainer = JsonConvert.DeserializeObject<FilteredMeals>(responseContent);

            if (mealContainer == null)
            {
                // can also be created to not allocate memory Enumerable.Empty<Meal>().ToList();
                mealContainer.Meals = new List<Meal>();
            }
            else
            {
                mealContainer.Meals = mealContainer.Meals.Take(count).ToList();
            }

            mealContainer.Meals.ForEach(meal =>
            {
                var mealDetails = GetMealDetails(meal.Name).Result;
                meal.Category = mealDetails.Category;
                meal.Area = mealDetails.Area;
            });

            return mealContainer.Meals;
        }

    }
}


    

