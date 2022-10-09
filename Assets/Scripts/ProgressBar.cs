using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image _fill;
    private float _timeTotal;
    private float _timeleft;
    private float _currentTime;

    public void SetTotalTime(float time)
    {
        _timeTotal = time;
    }

    public void UpdateUi(float _timeleft)
    {
        _fill.fillAmount = _timeleft / _timeTotal;
    }
}
