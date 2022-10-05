using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _ingredientDone = 0;
    [SerializeField] private TextMeshPro _orderTimerText;

    public float StartTime = 0;
    private float _timeToCompleteTheOrder;
    private MaterialPropertyBlock _propBlock;
    private RecipeData _recipeData;
    
    public void Init(RecipeData recipeData, float time)
    {
        _recipeData = recipeData;
        _timeToCompleteTheOrder = time;
        _propBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetTexture("_MainTex", recipeData.Icon);
        _renderer.SetPropertyBlock(_propBlock);
        _orderTimerText.text = ((int)_timeToCompleteTheOrder).ToString();
    }

    private void Update()
    {
        StartTime += Time.deltaTime;
        _orderTimerText.text = ((int)_timeToCompleteTheOrder - (int)StartTime).ToString();
        if (StartTime > _timeToCompleteTheOrder)
        {
            GameEvents.OnOrderExpired?.Invoke(this);
        }
    }

    public void StopMovingAnimation()
    {
        _animator.SetBool("Stop", true);
    }

    public Ingredient.Type CurrentIngredient()
    {
        return _recipeData.Ingredients[_ingredientDone].Type;
    }

    public void NextIngredient()
    {
        _ingredientDone++;
    }

    public bool IsDone()
    {
        return _recipeData.Ingredients.Count == _ingredientDone;
    }
}
