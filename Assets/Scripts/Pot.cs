using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] private List<Ingredient> _ingredients = new List<Ingredient>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            Ingredient ingredient = other.GetComponent<Ingredient>();
            _ingredients.Add(ingredient);
            OrderManager.Instance.IngredientCollected(ingredient);
        }
    }
}
