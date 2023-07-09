using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Transform firePoint;
    public GameObject FireProjectile;
    public GameObject WaterProjectile;
    public GameObject player;
    public int FlamethrowerFireRate = 5;
    public int WaterCannonFireRate = 5;
    public float firingPenaltyModifier = 0.5f;
    private int Delay;

    void FixedUpdate()
    {
        if (Input.GetKey("space") || Input.GetAxis("Right Trigger") > 0)
        {
            player.GetComponent<PlayerController>().moveSpeedModifier = firingPenaltyModifier; //Apply a movement penalty for firing,
            if (GameController.Instance._phase == GameController.Phase.FirePhase) //Flamethrower firing code starts here
            {
                Firing("Fire", FlamethrowerFireRate);
                //fireParticles.SetActive(true);
            }
            else if (GameController.Instance._phase == GameController.Phase.WaterPhase) //Water cannon firing code starts here
            {
                Firing("Water", WaterCannonFireRate);
                //waterParticles.SetActive(true);
            }
            else //Only errors out if it can't find the global game phase
            {
                Debug.Log("Error in finding GameController Instance in Weapons.cs - Lines 17 to 29");
                Delay = 0;
            }
        }
        else
        {
            Delay = 0;
            player.GetComponent<PlayerController>().moveSpeedModifier = 1; //resets the firing penalty, so movement should return to 1.
            //fireParticles.SetActive(false);
            //waterParticles.SetActive(false);
        }
    }

    void Firing(string projectileType, int fireRate)
    {
        if (Delay > fireRate)
        {
            if (projectileType == "Fire")
            {
                GameObject flame = Instantiate(FireProjectile, firePoint.position, firePoint.rotation);
                flame.GetComponent<Rigidbody>().velocity = transform.right * 10;         //No idea but if you make this a variable, it completely breaks. Honestly, it's weird asf - but I cba to fix it. This is for fire projectile speed.
                Delay = 0;
            }
            else if (projectileType == "Water")
            {
                GameObject water = Instantiate(WaterProjectile, firePoint.position, firePoint.rotation);
                water.GetComponent<Rigidbody>().velocity = transform.right * 10;         //Same again. This is for water projectile speed.
                Delay = 0;
            }
            else
            {
                Debug.Log("Error is parsing projectile type to Firing function - Weapon.cs starting line 38");
                Delay = 0;
            }
        }
        else
        {
            Delay++;
        }
    }
}