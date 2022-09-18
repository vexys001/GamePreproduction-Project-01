using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private GameObject _orderPrefab;
    [SerializeField] private OrdersTray _ordersTray;
    [SerializeField] private List<Order> _orders;
    [SerializeField] private List<RecipeData> _recipeDatas;
    [SerializeField] private float _timeBetweenOrders;
    [SerializeField] private int _maxNumberOfOrders = 10;

    private Coroutine _coroutine;
    // Start is called before the first frame update
    void Start()
    {
        _coroutine = StartCoroutine(SpawnOrder());
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
                order.Init(_recipeDatas[0]);
                bool orderAdded = _ordersTray.AddOrder(orderGo);

                if (!orderAdded)
                {
                    Debug.LogError("Order couldn't be added to the tray");
                }
                else
                {
                    _orders.Add(order);
                }
            }
            
            yield return new WaitForSecondsRealtime(_timeBetweenOrders);
        }
    }
}
