using System;
using UnityEngine;
using ComponentHolderProtocol = Unity.VisualScripting.ComponentHolderProtocol;

public class CuttingBoard : MonoBehaviour
{
    [SerializeField] private GameObject _cutIngredientPrefab;
    [SerializeField] private Transform _ingredientPosition;
    private GameObject _ingredientGO;
    private GameObject _cutIngredientGO;
    private Ingredient _ingredient;

    [Header("Materials")] 
    [SerializeField] private bool _isTwitch;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Color _purpleColor;
    
    private MaterialPropertyBlock _propertyBlock;

    private void Start()
    {
        if (_isTwitch)
        {
            _propertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_propertyBlock, 0);
            _propertyBlock.SetColor("_Color", _purpleColor);
            _renderer.SetPropertyBlock(_propertyBlock, 0);
        }
    }

    public void Use()
    {
        if (_ingredientGO != null)
        {
            Color color = _ingredientGO.GetComponent<MeshRenderer>().material.color;
            Destroy(_ingredientGO);
            _ingredientGO = null;
            _cutIngredientGO = Instantiate(_cutIngredientPrefab, _ingredientPosition.transform);
            _cutIngredientGO.GetComponent<MeshRenderer>().material.color = color;
            _cutIngredientGO.transform.localPosition = Vector3.zero;
            _cutIngredientGO.GetComponent<Ingredient>().SetIngredientType(_ingredient.GetIngredientType());
            _ingredient = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            _ingredientGO = other.gameObject;
            _ingredient = _ingredientGO.GetComponent<Ingredient>();
            _ingredient.StopMoving();
            _ingredientGO.transform.parent = _ingredientPosition.transform;
            _ingredientGO.transform.localPosition = Vector3.zero;
        }
    }
}
