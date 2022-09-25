public class GameEvents
{
    public delegate void OrderExpired(Order order);
    public static OrderExpired OnOrderExpired;
    public delegate void IngredientAddedToPot(Ingredient ingredient);
    public static IngredientAddedToPot OnIngredientAddedToPot;
}