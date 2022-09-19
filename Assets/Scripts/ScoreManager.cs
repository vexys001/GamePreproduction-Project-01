using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    public int MaxScorePerOrder = 15;
    [SerializeField] private int _score;
    private TextMeshProUGUI _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(float mult)
    {
        _score += Mathf.FloorToInt(MaxScorePerOrder * mult);
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        _score = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        _scoreText.SetText("Score: " + _score);
    }
}
