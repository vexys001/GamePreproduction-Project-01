using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Conveyor : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool _autoSpawn;
    [SerializeField] private float _autoSpawnTimer;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _conveyorSpeed;
    [Header("Items")] 
    [SerializeField] private List<Ingredient> _ingredientsPrefab;

    [Header("Materials")] 
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Color _purpleColor;

    private MaterialPropertyBlock _propertyBlock;
    private Random _random;
    private Coroutine _spawnCoroutine;

    private void OnEnable()
    {
        if (!_autoSpawn)
        {
            BotEvents.OnSpawnIngredient += OnSpawnIngredient;
        }
    }

    private void OnDisable()
    {
        if (!_autoSpawn)
        {
            BotEvents.OnSpawnIngredient -= OnSpawnIngredient;
        }
    }

    private void Start()
    {
        _propertyBlock = new MaterialPropertyBlock();
        
        _random = new Random();
        if (_autoSpawn)
        {
            _spawnCoroutine = StartCoroutine(SpawnIngredientLoop());
        }
        else
        {
            _renderer.GetPropertyBlock(_propertyBlock, 0);
            _propertyBlock.SetColor("_Color", _purpleColor);
            _renderer.SetPropertyBlock(_propertyBlock, 0);
        }
    }

    private void OnSpawnIngredient(Ingredient.Type ingredient)
    {
        Ingredient ingredientPrefab = _ingredientsPrefab.Find(ing => ing.GetIngredientType() == ingredient);
        if (!_autoSpawn)
        {
            ingredientPrefab.GetComponent<Interactable>().RotateCanvas();
        }

        SpawnIngredient(ingredientPrefab);
    }

    private void SpawnIngredient(Ingredient prefab)
    {
        Ingredient ingredient = Instantiate(prefab, _startPoint.transform.position, _startPoint.transform.rotation);
        ingredient.StartMoving(_conveyorSpeed);
    }

    IEnumerator SpawnIngredientLoop()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_autoSpawnTimer);
            int index = _random.Next(_ingredientsPrefab.Count);
            SpawnIngredient(_ingredientsPrefab[index]);
        }
    }
}
