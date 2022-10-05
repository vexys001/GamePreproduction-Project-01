using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Recipe", fileName = "Recipe")]
public class RecipeData : ScriptableObject
{
    public string Name;
    public Texture Icon;
    public List<RecipeIngredient> Ingredients;
}

[Serializable]
public struct RecipeIngredient
{
    public Ingredient.Type Type;
    public bool IsCut;
}