using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteIngredient : MonoBehaviour
{
    [SerializeField] private Claw _claw;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") && _claw.GetHeldObject() != other.gameObject)
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.gameObject.CompareTag("Ingredient") && _claw.GetHeldObject() != collision.rigidbody.gameObject)
        {
            Destroy(collision.rigidbody.gameObject);
        }
    }
}
