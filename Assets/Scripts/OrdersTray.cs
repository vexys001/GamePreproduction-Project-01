using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersTray : Singleton<OrdersTray>
{
    [SerializeField] private Transform _orderSpawnPosition;
    [SerializeField] private List<Transform> _ordersSocket;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private float _speed;
    
    private int _currentNumberOfOrder = 0;

    public bool AddOrder(GameObject order)
    {
        Transform orderTargetTransform = GetNextAvailablePosition();
        
        if (orderTargetTransform == null)
        {
            return false;
        }

        StartCoroutine(MoveIntoPosition(orderTargetTransform, order));
        _currentNumberOfOrder++;
        return true;
    }

    public Transform GetSpawnPosition()
    {
        return _orderSpawnPosition;
    }

    private Transform GetNextAvailablePosition()
    {
        if (_currentNumberOfOrder <= _ordersSocket.Count)
        {
            return _ordersSocket[_currentNumberOfOrder];
        }

        return null;
    }
    
    private IEnumerator MoveIntoPosition(Transform targetPosition, GameObject order)
    {
        Vector3 distance = Vector3.zero;
        distance = targetPosition.position - order.transform.position;
        while (distance.magnitude >= _stoppingDistance)
        {
            order.transform.position += distance.normalized * _speed * Time.deltaTime;
            distance = targetPosition.position - order.transform.position;
            if (distance.magnitude < _stoppingDistance)
            {
                order.GetComponent<Order>().StopMovingAnimation();
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void UpdateTray()
    {
        
    }
}
