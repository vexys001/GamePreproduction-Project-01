using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : Singleton<OrderManager>
{
    [SerializeField] private GameObject _orderPrefab;
    [SerializeField] private Dictionary<Order, GameObject> _ordersGo = new Dictionary<Order, GameObject>();
    [SerializeField] private List<Order> _orders = new List<Order>();
    [SerializeField] private List<RecipeData> _recipeDatas;
    [SerializeField] private float _timeBetweenOrders;
    [SerializeField] private int _maxNumberOfOrders = 10;
    [SerializeField] private int _timePerOrder = 10;

    private Coroutine _coroutine;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameEvents.OnOrderExpired += OnOrderExpired;
    }

    private void OnDisable()
    {
        GameEvents.OnOrderExpired -= OnOrderExpired;
    }

    void Start()
    {
        _coroutine = StartCoroutine(SpawnOrder());
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
                    }
                    else if (_orders[0].StartTime <= _timePerOrder * 0.66f)
                    {
                        ScoreManager.Instance.AddScore(0.66f);
                    }
                    else
                    {
                        ScoreManager.Instance.AddScore(0.33f);
                    }

                    RemoveOrder(_orders[0]);
                }
            }
        }
        
        Destroy(ingredient.gameObject);
    }

    private void OnOrderExpired(Order order)
    {
        if (_ordersGo.Remove(order, out GameObject orderGo))
        {
            Destroy(orderGo);
            OrdersTray.Instance.UpdateTray(_ordersGo.Values.ToList());
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
            OrdersTray.Instance.UpdateTray(_ordersGo.Values.ToList());
        }
    }

    IEnumerator SpawnOrder()
    {
        while (true)
        {
            if (_maxNumberOfOrders > _orders.Count)
            {
                Transform orderSpawnPosition = OrdersTray.Instance.GetSpawnPosition();
                GameObject orderGo = Instantiate(_orderPrefab, orderSpawnPosition.transform.position, orderSpawnPosition.transform.rotation);
                Order order = orderGo.GetComponent<Order>();
                order.Init(_recipeDatas[0], _timePerOrder);
                bool orderAdded = OrdersTray.Instance.AddOrder(orderGo);

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
}
