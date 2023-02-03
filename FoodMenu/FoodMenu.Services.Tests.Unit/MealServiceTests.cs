using FluentAssertions;
using FoodMenu.Data.Models;
using Moq;
using System.Text;

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
        [InlineData("Spaghetti", "Tasty", "Italy", "Spaghetti2", "Tasty", "Italy", "spagh")]
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
            var mockJson = @"{
                ""meals"": [
                    {
                        ""strArea"": ""{area}"",
                        ""strMeal"": ""{name}"",
                        ""strCategory"": ""{category}""
                    },
                ]
            }";
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

        [Theory]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Tasty", 2, 2, 2)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Tasty", 0, 0, 2)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Tasty", null, 0, 2)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Tasty", 2, 1, 1)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", null, 2, 1, 1)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "", 2, 1, 1)]

        public async void GetMealsByCategory_Should_Return_FilteredMeals_ExactAsCount_OrEmptyList(
            string name, string category, string area, string name2, string category2, string area2,
            string categoryInput, int countInput, int expectedCount, int jsonSelect)
        {
            //Arrange
            var mockHandler = new TestMessagehandler();
            var meal = Mock.Of<Meal>(m => m.Name == name && m.Category == category && m.Area == area);
            var meal2 = Mock.Of<Meal>(m => m.Name == name2 && m.Category == category2 && m.Area == area2);
            var Meals = Mock.Of<MealIncomingData>(m => m.Meals == Mock.Of<List<Meal>>());
            Meals.Meals.Add(meal);
            Meals.Meals.Add(meal2);
            var mockJson = "";
            if (jsonSelect == 2)
            {
                mockJson = @"{
                ""meals"": [
                    {
                        ""strArea"": ""{area}"",
                        ""strMeal"": ""{name}"",
                        ""strCategory"": ""{category}""
                    },
                    {
                        ""strArea"": ""{area2}"",
                        ""strMeal"": ""{name2}"",
                        ""strCategory"": ""{category2}""
                    }
                ]
                }";
                mockJson = mockJson.Replace("{area}", area).Replace("{name}", name).Replace("{category}", category);
                mockJson = mockJson.Replace("{area2}", area2).Replace("{name2}", name2).Replace("{category2}", category2);
            }
            else
            {
                mockJson = @"{
                ""meals"": [
                    {
                        ""strArea"": ""{area}"",
                        ""strMeal"": ""{name}"",
                        ""strCategory"": ""{category}""
                    }
                ]
                }";
                mockJson = mockJson.Replace("{area}", area).Replace("{name}", name).Replace("{category}", category);

            }
            var mockContent = new StringContent(mockJson, Encoding.UTF8, "application/json");
            var mockResponse = new HttpResponseMessage { Content = mockContent };
            mockHandler.SetResponse(mockResponse);
            var mockClient = new HttpClient(mockHandler);
            var mockMealService = new MealService(mockClient);
            var mockCategoryInput = categoryInput;
            var mockCountInput = countInput;

            //Act
            var actionResult = await mockMealService.GetMealsByCategory(mockCategoryInput, mockCountInput);

            //Assert
            if (categoryInput == null || categoryInput == "" || countInput == null || countInput == 0)
            {
                actionResult.Should().BeOfType<List<Meal>>().And.BeEmpty();
            }
            else
            {
                var resultList = actionResult as List<Meal>;
                Assert.NotNull(resultList);
                Assert.Equal(resultList.Count, expectedCount);
                for (int i = 0; i < resultList.Count; i++)
                {
                    if (i == 0)
                    {
                        Assert.Equal(resultList[i].Name, name);
                        Assert.Equal(resultList[i].Area, area);
                        Assert.Equal(resultList[i].Category, category);
                    }
                    else
                    {
                        Assert.Equal(resultList[i].Name, name2);
                        Assert.Equal(resultList[i].Area, area2);
                        Assert.Equal(resultList[i].Category, category2);
                    }
                }
            }
        }

        [Theory]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Pizza3", "Tasty", "Home", "Tasty", 2, 2)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Pizza3", "Tasty", "Home", "Tasty", 0, 0)]
        public async void GetMealsByCategory_Should_Return_FilteredMeals_OneLessThanCount(
           string name, string category, string area, string name2, string category2, string area2,
           string name3, string category3, string area3, string categoryInput, int countInput,
           int expectedCount)
        {
            //Arrange
            var mockHandler = new TestMessagehandler();
            var meal = Mock.Of<Meal>(m => m.Name == name && m.Category == category && m.Area == area);
            var meal2 = Mock.Of<Meal>(m => m.Name == name2 && m.Category == category2 && m.Area == area2);
            var meal3 = Mock.Of<Meal>(m => m.Name == name3 && m.Category == category3 && m.Area == area3);
            var Meals = Mock.Of<MealIncomingData>(m => m.Meals == Mock.Of<List<Meal>>());
            Meals.Meals.Add(meal);
            Meals.Meals.Add(meal2);
            Meals.Meals.Add(meal3);
            var mockJson = @"{
                ""meals"": [
                    {
                        ""strArea"": ""{area}"",
                        ""strMeal"": ""{name}"",
                        ""strCategory"": ""{category}""
                    },
                    {
                        ""strArea"": ""{area2}"",
                        ""strMeal"": ""{name2}"",
                        ""strCategory"": ""{category2}""
                    },
                    {
                        ""strArea"": ""{area3}"",
                        ""strMeal"": ""{name3}"",
                        ""strCategory"": ""{category3}""
                    }
                
            ]}";
            mockJson = mockJson.Replace("{area}", area).Replace("{name}", name).Replace("{category}", category);
            mockJson = mockJson.Replace("{area2}", area2).Replace("{name2}", name2).Replace("{category2}", category2);
            mockJson = mockJson.Replace("{area3}", area3).Replace("{name3}", name3).Replace("{category3}", category3);
            var mockContent = new StringContent(mockJson, Encoding.UTF8, "application/json");
            var mockResponse = new HttpResponseMessage { Content = mockContent };
            mockHandler.SetResponse(mockResponse);
            var mockClient = new HttpClient(mockHandler);
            var mockMealService = new MealService(mockClient);
            var mockCategoryInput = categoryInput;
            var mockCountInput = countInput;

            //Act
            var actionResult = await mockMealService.GetMealsByCategory(mockCategoryInput, mockCountInput);

            //Assert
            if (categoryInput == null || categoryInput == "" || countInput == null || countInput == 0)
            {
                actionResult.Should().BeOfType<List<Meal>>().And.BeEmpty();
            }
            else
            {
                var resultList = actionResult as List<Meal>;
                Assert.NotNull(resultList);
                Assert.Equal(resultList.Count, expectedCount);
                for (int i = 0; i < resultList.Count; i++)
                {
                    if (i == 0)
                    {
                        Assert.Equal(resultList[i].Name, name);
                        Assert.Equal(resultList[i].Area, area);
                        Assert.Equal(resultList[i].Category, category);
                    }
                    else
                    {
                        Assert.Equal(resultList[i].Name, name2);
                        Assert.Equal(resultList[i].Area, area2);
                        Assert.Equal(resultList[i].Category, category2);
                    }
                }
            }
        }

        [Theory]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Home", 2, 2, 2)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Home", 0, 0, 2)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Home", null, 0, 2)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Home", 2, 1, 1)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", null, 2, 1, 1)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "", 2, 1, 1)]

        public async void GetMealsByArea_Should_Return_FilteredMeals_ExactAsCount_OrEmptyList(
           string name, string category, string area, string name2, string category2, string area2,
           string areaInput, int countInput, int expectedCount, int jsonSelect)
        {
            //Arrange
            var mockHandler = new TestMessagehandler();
            var meal = Mock.Of<Meal>(m => m.Name == name && m.Category == category && m.Area == area);
            var meal2 = Mock.Of<Meal>(m => m.Name == name2 && m.Category == category2 && m.Area == area2);
            var Meals = Mock.Of<MealIncomingData>(m => m.Meals == Mock.Of<List<Meal>>());
            Meals.Meals.Add(meal);
            Meals.Meals.Add(meal2);
            var mockJson = "";
            if (jsonSelect == 2)
            {
                mockJson = @"{
                ""meals"": [
                    {
                        ""strArea"": ""{area}"",
                        ""strMeal"": ""{name}"",
                        ""strCategory"": ""{category}""
                    },
                    {
                        ""strArea"": ""{area2}"",
                        ""strMeal"": ""{name2}"",
                        ""strCategory"": ""{category2}""
                    }
                ]
                }";
                mockJson = mockJson.Replace("{area}", area).Replace("{name}", name).Replace("{category}", category);
                mockJson = mockJson.Replace("{area2}", area2).Replace("{name2}", name2).Replace("{category2}", category2);
            }
            else
            {
                mockJson = @"{
                ""meals"": [
                    {
                        ""strArea"": ""{area}"",
                        ""strMeal"": ""{name}"",
                        ""strCategory"": ""{category}""
                    }
                ]
                }";
                mockJson = mockJson.Replace("{area}", area).Replace("{name}", name).Replace("{category}", category);

            }
            var mockContent = new StringContent(mockJson, Encoding.UTF8, "application/json");
            var mockResponse = new HttpResponseMessage { Content = mockContent };
            mockHandler.SetResponse(mockResponse);
            var mockClient = new HttpClient(mockHandler);
            var mockMealService = new MealService(mockClient);
            var mockAreaInput = areaInput;
            var mockCountInput = countInput;

            //Act
            var actionResult = await mockMealService.GetMealsByArea(mockAreaInput, mockCountInput);

            //Assert
            if (areaInput == null || areaInput == "" || countInput == null || countInput == 0)
            {
                actionResult.Should().BeOfType<List<Meal>>().And.BeEmpty();
            }
            else
            {
                var resultList = actionResult as List<Meal>;
                Assert.NotNull(resultList);
                Assert.Equal(resultList.Count, expectedCount);
                for (int i = 0; i < resultList.Count; i++)
                {
                    if (i == 0)
                    {
                        Assert.Equal(resultList[i].Name, name);
                        Assert.Equal(resultList[i].Area, area);
                        Assert.Equal(resultList[i].Category, category);
                    }
                    else
                    {
                        Assert.Equal(resultList[i].Name, name2);
                        Assert.Equal(resultList[i].Area, area2);
                        Assert.Equal(resultList[i].Category, category2);
                    }
                }
            }
        }

        [Theory]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Pizza3", "Tasty", "Home", "Home", 2, 2)]
        [InlineData("Pizza1", "Tasty", "Home", "Pizza2", "Tasty", "Home", "Pizza3", "Tasty", "Home", "Home", 0, 0)]

        public async void GetMealsByArea_Should_Return_FilteredMeals_OneLessThanCount(
           string name, string category, string area, string name2, string category2, string area2,
           string name3, string category3, string area3, string areaInput, int countInput,
           int expectedCount)
        {
            //Arrange
            var mockHandler = new TestMessagehandler();
            var meal = Mock.Of<Meal>(m => m.Name == name && m.Category == category && m.Area == area);
            var meal2 = Mock.Of<Meal>(m => m.Name == name2 && m.Category == category2 && m.Area == area2);
            var meal3 = Mock.Of<Meal>(m => m.Name == name3 && m.Category == category3 && m.Area == area3);
            var Meals = Mock.Of<MealIncomingData>(m => m.Meals == Mock.Of<List<Meal>>());
            Meals.Meals.Add(meal);
            Meals.Meals.Add(meal2);
            Meals.Meals.Add(meal3);
            var mockJson = @"{
                ""meals"": [
                    {
                        ""strArea"": ""{area}"",
                        ""strMeal"": ""{name}"",
                        ""strCategory"": ""{category}""
                    },
                    {
                        ""strArea"": ""{area2}"",
                        ""strMeal"": ""{name2}"",
                        ""strCategory"": ""{category2}""
                    },
                    {
                        ""strArea"": ""{area3}"",
                        ""strMeal"": ""{name3}"",
                        ""strCategory"": ""{category3}""
                    }
                
            ]}";
            mockJson = mockJson.Replace("{area}", area).Replace("{name}", name).Replace("{category}", category);
            mockJson = mockJson.Replace("{area2}", area2).Replace("{name2}", name2).Replace("{category2}", category2);
            mockJson = mockJson.Replace("{area3}", area3).Replace("{name3}", name3).Replace("{category3}", category3);
            var mockContent = new StringContent(mockJson, Encoding.UTF8, "application/json");
            var mockResponse = new HttpResponseMessage { Content = mockContent };
            mockHandler.SetResponse(mockResponse);
            var mockClient = new HttpClient(mockHandler);
            var mockMealService = new MealService(mockClient);
            var mockAreaInput = areaInput;
            var mockCountInput = countInput;

            //Act
            var actionResult = await mockMealService.GetMealsByArea(mockAreaInput, mockCountInput);

            //Assert
            if (areaInput == null || areaInput == "" || countInput == null || countInput == 0)
            {
                actionResult.Should().BeOfType<List<Meal>>().And.BeEmpty();
            }
            else
            {
                var resultList = actionResult as List<Meal>;
                Assert.NotNull(resultList);
                Assert.Equal(resultList.Count, expectedCount);
                for (int i = 0; i < resultList.Count; i++)
                {
                    if (i == 0)
                    {
                        Assert.Equal(resultList[i].Name, name);
                        Assert.Equal(resultList[i].Area, area);
                        Assert.Equal(resultList[i].Category, category);
                    }
                    else
                    {
                        Assert.Equal(resultList[i].Name, name2);
                        Assert.Equal(resultList[i].Area, area2);
                        Assert.Equal(resultList[i].Category, category2);
                    }
                }
            }
        }
    }
}