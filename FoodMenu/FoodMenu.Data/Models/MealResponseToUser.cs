namespace FoodMenu.Data.Models
{
    //Can be done with inheretance, but it messes the json order and puts main meal details at back of json
    public class MealResponseToUser
    {
        public Meal Meal { get; set; }
        public IList<Meal> MealsByCategory { get; set; }

        public IList<Meal> MealsByArea { get; set; }
    }
}
