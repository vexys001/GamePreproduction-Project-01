using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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
        _random = new Random();
        if (_autoSpawn)
        {
            _spawnCoroutine = StartCoroutine(SpawnIngredientLoop());
        }
    }

    private void OnSpawnIngredient(Ingredient.Type ingredient)
    {
        Ingredient ingredientPrefab = _ingredientsPrefab.Find(ing => ing.GetIngredientType() == ingredient);
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
