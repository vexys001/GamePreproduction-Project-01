using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EndScreenManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimeText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "" + ScoreManager.Instance.GetScore();
        TimeText.text = "" + TimeSpan.FromSeconds(CountdownManager.Instance.GetTime()).ToString(@"mm\:ss");
    }
}
