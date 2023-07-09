using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public bool onFire = false;
    public bool path = false;
    public float burnTime, score;
    public int posX, posY;
    public GameObject model;
    public BoxCollider _collider;
    float burning;

    public GameObject fireSprite;

    private void Start()
    {
        burning = burnTime;
    }

    private void Update()
    {
        if (onFire && GameController.Instance._phase == GameController.Phase.WaterPhase)
        {
            burning -= Time.deltaTime;
            if(burning < 0)
            {
                DestroyHouse();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("registering collision");
        if (other.tag == "Projectile")
        {
            if (GameController.Instance._phase == GameController.Phase.FirePhase)
            {
                SetAblaze(false);
            }
            else
            {
                Douse();
            }
        }
    }

    public void SetAblaze(bool startBurning)
    {
        if (GameController.Instance._phase == GameController.Phase.FirePhase && !onFire)
        {
            GameController.Instance.TimerScoreController.IncreaseScore(100);
        }
        onFire = true;
        fireSprite.SetActive(true);
        if (!GetComponentInParent<Map>().TilesOnFire.Contains(this))
        {
            Debug.Log("Added to Fire List");
            GetComponentInParent<Map>().TilesOnFire.Add(this);
        }
    }

    public void Douse()
    {
        if(onFire)
        {
            GameController.Instance.TimerScoreController.IncreaseScore(Mathf.FloorToInt(score * (burnTime - burning)));
        }
        onFire = false;
        fireSprite.SetActive(false);
        if (GetComponentInParent<Map>().TilesOnFire.Contains(this))
        {
            GetComponentInParent<Map>().TilesOnFire.Remove(this);
        }
    }

    void DestroyHouse()
    {
        onFire = false;
        Douse();
        GameController.Instance.TimerScoreController.IncreaseScore(-100);
        model.SetActive(false);
        _collider.enabled = false;
    }
}
