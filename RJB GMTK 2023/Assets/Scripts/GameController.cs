using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] TimerAndScoreController _timeAndScoreControllerPrefab;
    [SerializeField] Map _mapPrefab;
    
    public static GameController Instance { get; private set; }

    public TimerAndScoreController TimerScoreController { get; private set; }
    public Map MapReference { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        TimerScoreController = Instantiate(_timeAndScoreControllerPrefab);
        MapReference = Instantiate(_mapPrefab);
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void StartGame()
    {
        TimerScoreController.StartTimer();
    }

    public void EndGame(int finalScore)
    {

    }
}
