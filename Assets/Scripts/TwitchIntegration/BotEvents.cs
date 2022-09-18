using TwitchLib.Client.Models;

namespace DefaultNamespace
{
    public static class BotEvents
    {
        public delegate void BotCommand(ChatMessage message);
        public static BotCommand OnBotCommand;
        
        public delegate void SpawnIngredient(Ingredient.Type ingredient);
        public static SpawnIngredient OnSpawnIngredient;
    }
}