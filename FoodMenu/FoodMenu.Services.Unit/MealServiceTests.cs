using FluentAssertions;
using FoodMenu.Data.Models;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace FoodMenu.Services.Unit
{
    public class MealServiceTests
    {

        [Fact]
        public async void GetMealDetails_Should_Return_FirstFoundMeal()
        {
            //Arrange
            var mockHandler = new TestMessagehandler();
            var meal = new Meal
            {
                Name = "Pizza1",
                Category = "Very Tasty",
                Area = "Home"
            };
            var mockMeal = new MealApiResponse
            {
                Meals = new List<Meal>()
            };
            mockMeal.Meals.Add(meal);
            var mockJson = JsonConvert.SerializeObject(mockMeal);
            var mockContent = new StringContent(mockJson, Encoding.UTF8, "application/json");
            var mockResponse = new HttpResponseMessage { Content = mockContent };
            mockHandler.SetResponse(mockResponse);
            var mockClient = new HttpClient(mockHandler);
            var mockMealService = new MealService(mockClient);
            var mockInput = "pizza";

            //Act

            var actionResult = mockMealService.GetMealDetails(mockInput).Result;

            //Assert

            actionResult.Name.Should().Be(mockMeal.Meals[0].Name);
            actionResult.Category.Should().Be(mockMeal.Meals[0].Category);
            actionResult.Area.Should().Be(mockMeal.Meals[0].Area);
        }
    }
}