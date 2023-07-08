using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public bool onFire = false;
    public bool path = false;
    public float burnTime, score;
    public int posX, posY;
    float burnProgress;

    public GameObject fireSprite;

    private void Update()
    {
        //if this tile is on fire and hasn't been added to the list of tiles on fire, add it to the list
        //if (onFire && !GetComponentInParent<Map>().TilesOnFire.Contains(this))
        //{
        //    GetComponentInParent<Map>().TilesOnFire.Add(this);
        //    fireSprite.SetActive(true);
        //}
        ////otherwise if the tile isn't on fire and is in the list of tiles on fire, remove it from the list
        //else if (!onFire && GetComponentInParent<Map>().TilesOnFire.Contains(this))
        //{
        //    GetComponentInParent<Map>().TilesOnFire.Remove(this);
        //    fireSprite.SetActive(false);
        //}
        if (onFire && GameController.Instance._phase == GameController.Phase.WaterPhase)
        {

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
        onFire = false;
        fireSprite.SetActive(false);
        if (GetComponentInParent<Map>().TilesOnFire.Contains(this))
        {
            GetComponentInParent<Map>().TilesOnFire.Remove(this);
        }
    }
}
