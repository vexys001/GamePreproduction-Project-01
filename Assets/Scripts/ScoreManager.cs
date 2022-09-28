using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    public int MaxScorePerOrder = 15;
    [SerializeField] private int _score;
    public TextMeshProUGUI ScoreText;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        //_scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateScoreUI();
    }

    public void AddScore(float mult)
    {
        _score += Mathf.FloorToInt(MaxScorePerOrder * mult);
        if(_score >= 100)
        {
            CountdownManager.Instance.StopCountdown();
        }
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        _score = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        ScoreText.SetText("Score: " + _score);
    }

    public int GetScore()
    {
        return _score;
    }
}
