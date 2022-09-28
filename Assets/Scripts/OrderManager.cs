using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private GameObject _orderPrefab;
    [SerializeField] private Dictionary<Order, GameObject> _ordersGo = new Dictionary<Order, GameObject>();
    [SerializeField] private List<Order> _orders = new List<Order>();
    [SerializeField] private List<RecipeData> _recipeDatas;
    [SerializeField] private float _timeBetweenOrders;
    [SerializeField] private int _maxNumberOfOrders = 10;
    [SerializeField] private int _timePerOrder = 10;
    [SerializeField] private int _timeGainedPerOrder = 20;
    [SerializeField] private int _maxScore = 15;
    [SerializeField] private OrdersTray _ordersTray;

    private Coroutine _coroutine;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameEvents.OnOrderExpired += OnOrderExpired;
        GameEvents.OnIngredientAddedToPot += IngredientCollected;
        GameEvents.OnTimerEnded += EndGame;
    }

    private void OnDisable()
    {
        GameEvents.OnOrderExpired -= OnOrderExpired;
        GameEvents.OnIngredientAddedToPot -= IngredientCollected;
        GameEvents.OnTimerEnded -= EndGame;
    }

    void Start()
    {
        _coroutine = StartCoroutine(SpawnOrder());
        CountdownManager.Instance.StartCountdown();
    }

    public void IngredientCollected(Ingredient ingredient)
    {
        //TODO check order[0] is updated
        if (_orders.Count > 0)
        {
            if (_orders[0].CurrentIngredient() == ingredient.GetIngredientType())
            {
                _orders[0].NextIngredient();
                if (_orders[0].IsDone())
                {
                    Debug.Log("Order Done");

                    if (_orders[0].StartTime <= _timePerOrder * 0.33f)
                    {
                        ScoreManager.Instance.AddScore(1f);
                        CountdownManager.Instance.AddTime(_timeGainedPerOrder);
                    }
                    else if (_orders[0].StartTime <= _timePerOrder * 0.66f)
                    {
                        ScoreManager.Instance.AddScore(0.66f);
                        CountdownManager.Instance.AddTime(_timeGainedPerOrder);
                    }
                    else
                    {
                        ScoreManager.Instance.AddScore(0.33f);
                        CountdownManager.Instance.AddTime(_timeGainedPerOrder);
                    }
                    GameEvents.OnOrderDone?.Invoke(_orders[0]);
                    RemoveOrder(_orders[0]);
                }
            }
        }
        
        Destroy(ingredient.gameObject);

        if (ScoreManager.Instance.GetScore() >= _maxScore)
        {
            CountdownManager.Instance.StopCountdown();
            EndGame();
        }
    }

    private void OnOrderExpired(Order order)
    {
        if (_ordersGo.Remove(order, out GameObject orderGo))
        {
            Destroy(orderGo);
            _ordersTray.UpdateTray(_ordersGo.Values.ToList());
        }
    }

    private void AddOrder(Order order, GameObject go)
    {
        _orders.Add(order);
        _ordersGo.Add(order, go);
    }

    private void RemoveOrder(Order order)
    {
        if (_ordersGo.Remove(order, out GameObject orderGo))
        {
            Destroy(orderGo);
            _orders.Remove(order);
            _ordersTray.UpdateTray(_ordersGo.Values.ToList());
        }
    }

    IEnumerator SpawnOrder()
    {
        while (true)
        {
            if (_maxNumberOfOrders > _orders.Count)
            {
                Transform orderSpawnPosition = _ordersTray.GetSpawnPosition();
                GameObject orderGo = Instantiate(_orderPrefab, orderSpawnPosition.transform.position, orderSpawnPosition.transform.rotation);
                Order order = orderGo.GetComponent<Order>();
                int index = Random.Range (0, _recipeDatas.Count);
                order.Init(_recipeDatas[index], _timePerOrder);
                bool orderAdded = _ordersTray.AddOrder(orderGo);

                if (!orderAdded)
                {
                    Debug.LogError("Order couldn't be added to the tray");
                }
                else
                {
                    AddOrder(order, orderGo);
                }
            }

            yield return new WaitForSecondsRealtime(_timeBetweenOrders);
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndScreen");
    }
}
