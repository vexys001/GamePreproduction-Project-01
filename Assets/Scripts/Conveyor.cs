using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Conveyor : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool _autoSpawn;
    [SerializeField] private float _autoSpawnTimer;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _stopPoint;
    [SerializeField] private float _conveyorSpeed;
    [Header("Items")] 
    [SerializeField] private List<Ingredient> _ingredientsPrefab;

    private Random _random;
    private Coroutine _spawnCoroutine;
    private void Start()
    {
        _random = new Random();
        _spawnCoroutine = StartCoroutine(SpawnIngredient());
    }

    IEnumerator SpawnIngredient()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_autoSpawnTimer);
            int index = _random.Next(_ingredientsPrefab.Count);
            Ingredient ingredient = Instantiate(_ingredientsPrefab[index], _startPoint.transform.position, _startPoint.transform.rotation);
            ingredient.StartMoving(_conveyorSpeed);
        }
    }
}
