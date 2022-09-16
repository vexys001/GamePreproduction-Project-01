
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ingredient : MonoBehaviour
{
    public enum Type
    {
        Ham,
        Bread,
        Cheese
    }

    private Rigidbody _rb;
    private Coroutine _moveCoroutine;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

    public void StartMoving(float speed)
    {
        _moveCoroutine = StartCoroutine(MoveOnConveyor(speed));
    }

    private IEnumerator MoveOnConveyor(float speed)
    {
        while (true)
        {
            Transform _transform = gameObject.transform;
            _transform.position += _transform.forward * (speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void Fall()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
        StopCoroutine(_moveCoroutine);
        Destroy(gameObject, 2f);
    }
}