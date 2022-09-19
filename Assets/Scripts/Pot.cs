using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            OrderManager.Instance.IngredientCollected(other.GetComponent<Ingredient>());
        }
    }
}
