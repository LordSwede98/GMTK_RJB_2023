using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerAndScoreController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText = null;

    [SerializeField] TextMeshProUGUI _timerText = null;
    
    int _score;

    float _timeRemaining;

    bool _timerRunning = false;

    bool _countdown = true;

    GameController _controller;

    public Action<int> EndOfTimer;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        //_timeRemaining = 300;
        _controller = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerRunning)
        {
            if (_countdown)
            {
                if (_timeRemaining > 0)
                {
                    _timeRemaining -= Time.deltaTime;
                    DisplayTime(_timeRemaining);
                }
                else
                {
                    _timeRemaining = 0;
                    TimerEnded();
                }
            }
            else
            {
                _timeRemaining += Time.deltaTime;
                DisplayTime(_timeRemaining);
            }
        }
    }

    public void TimerEnded()
    {
        _timerRunning = false;
        Debug.Log("TimerFinished");
        EndOfTimer?.Invoke(_score);
    }

    public void StartTimer(float startTime, bool countdown, Action<int> endTimerAction)
    {
        _timerRunning = true;
        _countdown = countdown;
        _timeRemaining = startTime;
        EndOfTimer = endTimerAction;
    }

    void DisplayTime(float currentTimeValue)
    {
        if (currentTimeValue < 0)
            currentTimeValue = 0;

        float minutes = Mathf.FloorToInt(currentTimeValue / 60);
        float seconds = Mathf.FloorToInt(currentTimeValue % 60);

        _timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void IncreaseScore(int scoreValue)
    {
        _score += scoreValue;
        _scoreText.text = _score.ToString();
    }
}
