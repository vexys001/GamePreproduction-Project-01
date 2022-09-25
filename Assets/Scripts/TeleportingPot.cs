using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingPot : MonoBehaviour
{
    public Transform[] TeleportingPoints;
    [SerializeField] private int _pos = 0;

    public float MaxTimer;
    [SerializeField] private float currentTimer; 
    // Start is called before the first frame update
    void Start()
    {
        transform.position = TeleportingPoints[_pos++].position;
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;

        if(currentTimer >= MaxTimer)
        {
            transform.position = TeleportingPoints[_pos++].position;
            if(_pos >= TeleportingPoints.Length)
            {
                _pos = 0;
            }
            currentTimer = 0;
        }
    }
}
