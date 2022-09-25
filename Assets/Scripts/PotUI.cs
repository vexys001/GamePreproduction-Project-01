using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotUI : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _ingredients = new List<TextMeshProUGUI>();

    private int _currentIngredient = 0;
    private void OnEnable()
    {
        GameEvents.OnIngredientAddedToPot += OnIngredientAddedToPot;
    }

    private void OnDisable()
    {
        GameEvents.OnIngredientAddedToPot -= OnIngredientAddedToPot;
    }

    private void OnIngredientAddedToPot(Ingredient ingredient)
    {
        if (_ingredients.Count <= _currentIngredient)
        {
            _ingredients[_currentIngredient].text = ingredient.GetIngredientType().ToString();
        }
    }
}
