using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] TimerAndScoreController _timeAndScoreControllerPrefab;
    
    public static GameController Instance { get; private set; }

    public TimerAndScoreController TimerAndScoreController { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        TimerAndScoreController = Instantiate(_timeAndScoreControllerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        
    }

    public void EndGame(int finalScore)
    {

    }
}
