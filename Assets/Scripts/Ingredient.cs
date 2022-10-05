
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Ingredient : MonoBehaviour
{
    public enum Type
    {
        NONE,
        LETTUCE,
        BREAD,
        TOMATO,
        CHEESE,
        HAM
    }

    [SerializeField] private Type _type;
    [SerializeField] private bool _isCut;
    private Rigidbody _rb;
    private MeshFilter _mesh;
    private Coroutine _moveCoroutine;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

    public Ingredient.Type GetIngredientType()
    {
        return _type;
    }

    public bool GetIsCut()
    {
        return _isCut;
    }

    public void SetIngredientType(Ingredient.Type type)
    {
        _type = type;
    }

    public void StartMoving(float speed)
    {
        _moveCoroutine = StartCoroutine(MoveOnConveyor(speed));
    }

    public void StopMoving()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
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
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
    }
}