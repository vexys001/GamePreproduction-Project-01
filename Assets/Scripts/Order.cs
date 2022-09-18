using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private RecipeData _recipeData;
    public void Init(RecipeData recipeData)
    {
        _recipeData = recipeData;
        //Setup Recipe icon
    }

    public void StopMovingAnimation()
    {
        _animator.SetBool("Stop", true);
    }
}
