using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownManager : Singleton<CountdownManager>
{
    [SerializeField] private float _maxTimer;
    [SerializeField] private float _timer;

    public bool Active = false;
    public TextMeshProUGUI TimeLeftText;
    
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                //Call Other Function
                _timer = 0;
                StopCountdown();
                GameEvents.OnTimerEnded();
            }
            TimeLeftText.text = "Time Left: " + Mathf.Floor(_timer);
        }
    }

    public float GetTime()
    {
        return (_maxTimer - _timer);
    }

    public void AddTime(float time)
    {
        _timer += time;
        TimeLeftText.text = "Time Left: " + Mathf.Floor(_timer);
    }

    public void StartCountdown()
    {
        Active = true;
    }

    public void StopCountdown()
    {
        Active = false;
        //GameEvents.OnTimerEnded();
    }

    public void ResetCountdown()
    {
        _timer = _maxTimer;
    }
}
