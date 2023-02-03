using FoodMenu.Data.Models;
using Newtonsoft.Json;

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
            var mealContainer = JsonConvert.DeserializeObject<MealIncomingData>(responseContent);

            if (mealContainer.Meals == null)
                return null;

            if (mealContainer.Meals[0].Name == null || mealContainer.Meals[0].Name == "")
                return null;

            return mealContainer.Meals[0];
        }

        public async Task<IList<Meal>> GetMealsByCategory(string category, int count)
        {
            // new List<Meal>() can also be created to not allocate memory Enumerable.Empty<Meal>().ToList();
            if (category == null || category == "" || count == null || count == 0)
            {
                return new List<Meal>();
            }

            var response = await _httpClient.GetAsync(string.Format(GetMealByCategoryLink, category));

            if (!response.IsSuccessStatusCode)
            {
                return new List<Meal>();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var mealContainer = JsonConvert.DeserializeObject<MealIncomingData>(responseContent);

            if (mealContainer == null)
            {
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
            // new List<Meal>() can also be created to not allocate memory Enumerable.Empty<Meal>().ToList();
            if (area == null || area == "" || count == null || count == 0)
            {
                return new List<Meal>();
            }
            var response = await _httpClient.GetAsync(string.Format(GetMealByAreaLink, area));

            if (!response.IsSuccessStatusCode)
            {
                return new List<Meal>();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var mealContainer = JsonConvert.DeserializeObject<MealIncomingData>(responseContent);

            if (mealContainer == null)
            {
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