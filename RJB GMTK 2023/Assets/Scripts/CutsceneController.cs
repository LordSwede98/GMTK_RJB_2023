using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] GameObject[] _slides;
    [SerializeField] float[] _slideIntervals;

    float _timeRemaining;
    bool _timerRunning = false;

    public Action EndOfCutscene;

    int _index;

    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerRunning)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SkipCutscene();
            }
            else
            {
                if (_timeRemaining > 0)
                {
                    _timeRemaining -= Time.deltaTime;
                }
                else
                {
                    _index += 1;
                    if (_index == _slides.Length)
                    {
                        _timerRunning = false;
                        EndOfCutscene?.Invoke();
                        Destroy(gameObject);
                    }
                    else
                    {
                        ShowCurrentIndexSlide();
                    }
                }
            }
        }
    }

    void ShowCurrentIndexSlide()
    {
        for (int i = 0; i < _slides.Length; i++)
        {
            if (i == _index)
            {
                _slides[i].SetActive(true);
            }
            else
            {
                _slides[i].SetActive(false);
            }
        }

        _timeRemaining = _slideIntervals[_index];
    }

    public void StartCutscene(Action endAction)
    {
        EndOfCutscene = endAction;
        _timerRunning = true;
        ShowCurrentIndexSlide();
    }

    void SkipCutscene()
    {
        _timerRunning = false;
        EndOfCutscene?.Invoke();
        Destroy(gameObject);
    }
}
