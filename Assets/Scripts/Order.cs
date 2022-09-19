using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _ingredientDone = 0;

    private float _startTime = 0;
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
    }

    private void Update()
    {
        _startTime += Time.deltaTime;
        if (_startTime > _timeToCompleteTheOrder)
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
        return _recipeData.Ingredients[_ingredientDone];
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
