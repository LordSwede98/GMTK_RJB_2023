using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] TimerAndScoreController _timeAndScoreControllerPrefab = null;
    [SerializeField] Map _mapPrefab = null;
    [SerializeField] Transform _playerController = null;
    [SerializeField] CutsceneController _openingPrefab;
    [SerializeField] CutsceneController _middlePrefab;
    [SerializeField] CutsceneController _endingPrefab;
    public AudioSource fire;
    public AudioSource water;
    
    public enum Phase { FirePhase, WaterPhase, CutscenePhase };

    public static GameController Instance { get; private set; }

    public TimerAndScoreController TimerScoreController { get; private set; }
    public Map MapReference { get; private set; }

    public Phase _phase;

    // Start is called before the first frame update
    void Start()
    {
        _phase = Phase.CutscenePhase;
        Instance = this;
        MapReference = Instantiate(_mapPrefab);
        TimerScoreController = Instantiate(_timeAndScoreControllerPrefab);
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
        _phase = Phase.FirePhase;
        TimerScoreController.StartTimer(30, true, EndFirstPhase);
        _playerController.position = MapReference.CenterPosition();
    }

    public void StartSecondPhase()
    {
        _phase = Phase.WaterPhase;
        TimerScoreController.StartTimer(0, false, EndSecondPhase);
        //_playerController.position = new Vector3(MapReference.GridWidth() / 2, MapReference.GridHeight() / 2, _playerController.position.z);
    }
    
    public void EndFirstPhase(int finalScore)
    {
        //StartSecondPhase();
        _phase = Phase.CutscenePhase;
        CutsceneController cutscene = Instantiate(_middlePrefab);
        cutscene.StartCutscene(StartSecondPhase);
        fire.mute = true;
        water.mute = false;
        water.Play();
    }

    public void EndSecondPhase(int finalScore)
    {
        _phase = Phase.CutscenePhase;
        CutsceneController cutscene = Instantiate(_endingPrefab);
        cutscene.StartCutscene(Ending);
    }

    public void Ending()
    {

    }
}
