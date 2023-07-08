using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] TimerAndScoreController _timeAndScoreControllerPrefab;
    [SerializeField] Map _mapPrefab;
    [SerializeField] Transform _playerController;
    
    public enum Phase { FirePhase, WaterPhase };

    public static GameController Instance { get; private set; }

    public TimerAndScoreController TimerScoreController { get; private set; }
    public Map MapReference { get; private set; }

    public Phase _phase;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        TimerScoreController = Instantiate(_timeAndScoreControllerPrefab);
        MapReference = Instantiate(_mapPrefab);
        StartFirstPhase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void StartFirstPhase()
    {
        _phase = Phase.FirePhase;
        TimerScoreController.StartTimer(30, true, EndFirstPhase);
        _playerController.position = new Vector3(MapReference.GridWidth() / 2, MapReference.GridHeight() / 2, _playerController.position.z);
    }

    public void StartSecondPhase()
    {
        _phase = Phase.WaterPhase;
        TimerScoreController.StartTimer(0, false, EndSecondPhase);
        _playerController.position = new Vector3(MapReference.GridWidth() / 2, MapReference.GridHeight() / 2, _playerController.position.z);
    }

    public void EndFirstPhase(int finalScore)
    {
        StartSecondPhase();
    }

    public void EndSecondPhase(int finalScore)
    {
        
    }
}
