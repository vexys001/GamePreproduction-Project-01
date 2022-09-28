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
        GameEvents.OnOrderDone += OnOrderDone;
        GameEvents.OnOrderExpired += OnOrderDone;
    }

    private void OnDisable()
    {
        GameEvents.OnIngredientAddedToPot -= OnIngredientAddedToPot;
        GameEvents.OnOrderDone -= OnOrderDone;
        GameEvents.OnOrderExpired -= OnOrderDone;
    }
    
    private void OnOrderDone(Order order)
    {
        foreach (TextMeshProUGUI ingredient in _ingredients)
        {
            ingredient.text = "";
        }

        _currentIngredient = 0;
    }

    private void OnIngredientAddedToPot(Ingredient ingredient)
    {
        if (_ingredients.Count > _currentIngredient)
        {
            _ingredients[_currentIngredient].text = ingredient.GetIngredientType().ToString();
            _currentIngredient++;
        }
    }
}
