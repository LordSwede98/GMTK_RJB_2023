using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject FireProjectile;
    public GameObject WaterProjectile;
    public int FlamethrowerFireRate = 20;
    public int WaterCannonFireRate = 20;

    private int Delay;

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            if (GameController.Instance._phase == GameController.Phase.FirePhase) //Flamethrower firing code starts here
            {
                Firing("Fire", FlamethrowerFireRate);
            }
            else if (GameController.Instance._phase == GameController.Phase.WaterPhase) //Water cannon firing code starts here
            {
                Firing("Water", WaterCannonFireRate);
            }
            else
            {
                Debug.Log("Error in finding GameController Instance in Weapons.cs - Lines 17 to 29");
            }
        }
        else
        {
            Delay = 0;
        }
    }

    void Firing(string debug, int fireRate)
    {
        if (Delay > fireRate)
        {
            Debug.Log(debug);
            Delay = 0;
        }
        else
        {
            Delay++;
        }
    }
}