public class GameEvents
{
    public delegate void OrderExpired(Order order);
    public static OrderExpired OnOrderExpired;
    
    public delegate void OrderDone(Order order);
    public static OrderDone OnOrderDone;
    public delegate void IngredientAddedToPot(Ingredient ingredient);
    public static IngredientAddedToPot OnIngredientAddedToPot;
    public delegate void ValidIngredientAddedToPot(Ingredient ingredient);
    public static ValidIngredientAddedToPot OnValidIngredientAddedToPot;
    
    public delegate void TimerEnded();
    public static TimerEnded OnTimerEnded;
}