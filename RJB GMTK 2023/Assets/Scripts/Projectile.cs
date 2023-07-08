using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int survivalRate;
    public int expansionDelay;
    private int DestroyDelay;
    private int ExpansionDelayTimer;
    public GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        survivalRate = 40;
        expansionDelay = 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DestroyDelay > survivalRate)
        {
            Destroy(self);
        }
        else
        {
            if (expansionDelay < ExpansionDelayTimer)
            {
                this.transform.localScale += new Vector3(1, 1, 1);
                ExpansionDelayTimer = 0;
            }
            else
            {
                ExpansionDelayTimer++;
            }
            DestroyDelay++;
        }
    }
}
