﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] TimerAndScoreController _timeAndScoreControllerPrefab = null;
    [SerializeField] Map _mapPrefab = null;
    [SerializeField] Transform _playerController = null;
    [SerializeField] CutsceneController _openingPrefab = null;
    [SerializeField] CutsceneController _middlePrefab = null;
    [SerializeField] CutsceneController _endingPrefab = null;
    [SerializeField] CutsceneController _badEndingPrefab = null;
    [SerializeField] GameObject _endingCanvas = null;
    [SerializeField] GameObject _goodEnding = null;
    [SerializeField] GameObject _badEnding = null;
    [SerializeField] TextMeshProUGUI _scoreText = null;
    [SerializeField] GameObject _minimap = null;
    public AudioSource fire;
    public AudioSource water;
    public AudioSource endCutsceneAudio;
    public AudioSource midCutsceneAudio;
    
    public enum Phase { FirePhase, WaterPhase, CutscenePhase };

    public static GameController Instance { get; private set; }

    public TimerAndScoreController TimerScoreController { get; private set; }
    public Map MapReference { get; private set; }

    public Phase _phase;

    int finalScore;

    // Start is called before the first frame update
    void Start()
    {
        _phase = Phase.CutscenePhase;
        Instance = this;
        MapReference = Instantiate(_mapPrefab);
        TimerScoreController = Instantiate(_timeAndScoreControllerPrefab);
        HideShowUI(false);
        //StartFirstPhase();
        CutsceneController cutscene = Instantiate(_openingPrefab);
        cutscene.StartCutscene(StartFirstPhase);
    }

    // Update is called once per frame
    void Update()
    {
        //TESTING CODE
        if(Input.GetKeyDown(KeyCode.Z) && _phase == Phase.WaterPhase)
        {
            TimerScoreController.TimerEnded();
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void StartFirstPhase()
    {
        HideShowUI(true);
        _phase = Phase.FirePhase;
        TimerScoreController.StartTimer(30, true, EndFirstPhase);
        _playerController.position = MapReference.CenterPosition();
    }

    public void StartSecondPhase()
    {
        HideShowUI(true);
        _phase = Phase.WaterPhase;
        TimerScoreController.StartTimer(0, false, EndSecondPhase);
        midCutsceneAudio.mute = true;
        water.mute = false;
        water.Play();
    }
    
    public void EndFirstPhase(int score)
    {
        HideShowUI(false);
        _phase = Phase.CutscenePhase;
        CutsceneController cutscene = Instantiate(_middlePrefab);
        cutscene.StartCutscene(StartSecondPhase);
        fire.mute = true;
        midCutsceneAudio.Play();
        midCutsceneAudio.mute = false;
    }

    public void EndSecondPhase(int score)
    {
        finalScore = score;
        HideShowUI(false);
        _phase = Phase.CutscenePhase;
        if (score > 0)
        {
            CutsceneController cutscene = Instantiate(_endingPrefab);
            cutscene.StartCutscene(Ending);
        }
        else
        {
            CutsceneController cutscene = Instantiate(_badEndingPrefab);
            cutscene.StartCutscene(Ending);
        }
        water.mute = true;
        endCutsceneAudio.mute = false;
        endCutsceneAudio.Play();
    }

    public void Ending()
    {
        _scoreText.text = "Final Score: \n" + finalScore; 
        _endingCanvas.SetActive(true);
        if (finalScore > 0)
        {
            _goodEnding.SetActive(true);
            _badEnding.SetActive(false);
        }
        else
        {
            _goodEnding.SetActive(false);
            _badEnding.SetActive(true);
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Replay()
    {
        Destroy(MapReference.gameObject);
        MapReference = null;
        MapReference = Instantiate(_mapPrefab);

        _endingCanvas.SetActive(false);

        TimerScoreController.ResetScore();

        CutsceneController cutscene = Instantiate(_openingPrefab);
        cutscene.StartCutscene(StartFirstPhase);
        endCutsceneAudio.mute = true;
        fire.Stop();
        fire.Play();
        fire.mute = false;
    }

    void HideShowUI(bool show)
    {
        TimerScoreController.gameObject.SetActive(show);
        _minimap.SetActive(show);
    }
}
