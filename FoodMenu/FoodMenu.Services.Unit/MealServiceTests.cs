using FluentAssertions;
using FoodMenu.Data.Models;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FoodMenu.Services.Unit
{
    public class MealServiceTests
    {
        [Theory]
        [InlineData("Pizza1", "Very Tasty", "Home", "Pizza1")]
        [InlineData("Spaghetti", "Tasty", "Italy", "spaghetti")]
        [InlineData("Spaghetti", "Tasty", "Italy", "spagh")]
        [InlineData("", "", "", "findempty")]
        [InlineData(null, "", "", "findempty")]
        [InlineData(null, null, null, "findempty")]

        public async void GetMealDetails_Should_Return_Meal(string name, string category, string area, string input)
        {
            //Arrange
            var mockHandler = new TestMessagehandler();
            var meal = Mock.Of<Meal>(m => m.Name == name && m.Category == category && m.Area == area);
            var Meals = Mock.Of<MealIncomingData>(m => m.Meals == Mock.Of<List<Meal>>());
            Meals.Meals.Add(meal);
            var mockJson = "{\"meals\":[{\"strArea\":\"{area}\",\"strMeal\":\"{name}\",\"strCategory\":\"{category}\"}]}";
            mockJson = mockJson.Replace("{area}", area).Replace("{name}", name).Replace("{category}", category);
            var mockContent = new StringContent(mockJson, Encoding.UTF8, "application/json");
            var mockResponse = new HttpResponseMessage { Content = mockContent };
            mockHandler.SetResponse(mockResponse);
            var mockClient = new HttpClient(mockHandler);
            var mockMealService = new MealService(mockClient);
            var mockInput = input;

            //Act

            var actionResult = await mockMealService.GetMealDetails(mockInput);

            //Assert
            if (Meals.Meals[0].Name == null || Meals.Meals[0].Name == "")
            {
                actionResult.Should().BeNull();
            }
            else
            {
                actionResult.Name.Should().Be(Meals.Meals[0].Name);
                actionResult.Category.Should().Be(Meals.Meals[0].Category);
                actionResult.Area.Should().Be(Meals.Meals[0].Area);
            }
        }

        [Theory]
        [InlineData("Pizza1", "Very Tasty", "Home", "Pizza2", "veryTasty", "Home", "Pizza1")]
        [InlineData("Spaghetti", "Tasty", "Italy", "Spaghetti2", "Tasty", "Italy", "spaghetti")]
        [InlineData("Spaghetti", "Tasty", "Italy", "Spaghetti2", "Tasty", "Italy","spagh")]
        public async void GetMealDetails_Should_Return_FirstFoundMeal(string name, string category, string area,
            string name2, string category2, string area2, string input)
        {
            //Arrange
            var mockHandler = new TestMessagehandler();
            var meal = Mock.Of<Meal>(m => m.Name == name && m.Category == category && m.Area == area);
            var meal2 = Mock.Of<Meal>(m => m.Name == name2 && m.Category == category2 && m.Area == area2);
            var Meals = Mock.Of<MealIncomingData>(m => m.Meals == Mock.Of<List<Meal>>());
            Meals.Meals.Add(meal);
            Meals.Meals.Add(meal2);
            var mockJson = "{\"meals\":[{\"strArea\":\"{area}\",\"strMeal\":\"{name}\",\"strCategory\":\"{category}\"}]}";
            mockJson = mockJson.Replace("{area}", area).Replace("{name}", name).Replace("{category}", category);
            var mockContent = new StringContent(mockJson, Encoding.UTF8, "application/json");
            var mockResponse = new HttpResponseMessage { Content = mockContent };
            mockHandler.SetResponse(mockResponse);
            var mockClient = new HttpClient(mockHandler);
            var mockMealService = new MealService(mockClient);
            var mockInput = input;

            //Act

            var actionResult = await mockMealService.GetMealDetails(mockInput);

            //Assert
                actionResult.Name.Should().Be(Meals.Meals[0].Name);
                actionResult.Category.Should().Be(Meals.Meals[0].Category);
                actionResult.Area.Should().Be(Meals.Meals[0].Area);
        }
    }
}