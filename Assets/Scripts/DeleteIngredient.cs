using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteIngredient : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.gameObject.CompareTag("Ingredient"))
        {
            Destroy(collision.rigidbody.gameObject);
        }
    }
}
