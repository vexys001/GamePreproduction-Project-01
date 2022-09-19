using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Recipe", fileName = "Recipe")]
public class RecipeData : ScriptableObject
{
    public string Name;
    public Texture Icon;
    public List<Ingredient.Type> Ingredients;
}
