public class GameEvents
{
    public delegate void OrderExpired(Order order);
    public static OrderExpired OnOrderExpired;
}