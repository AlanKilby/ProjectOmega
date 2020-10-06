using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiDrop : MonoBehaviour
{
    [SerializeField] EnnemisScript ES;

    [SerializeField] private int weaponDropRate;
    [SerializeField] private int moduleDropRate;
    [SerializeField] private int consummableDropRate;

    public bool willDrop;

    void Start()
    {
        moduleDropRate = moduleDropRate + weaponDropRate;
        consummableDropRate = moduleDropRate + consummableDropRate;
    }

    //private void FixedUpdate()
    //{
    //    if(willDrop == true)
    //    {
    //        ChooseDropType();
    //        willDrop = false;
    //    }
    //}

    public void LanchDrop()
    {
        willDrop = true;
    }

    public void ChooseDropType()
    {
        int randomnumber = Random.Range(1, 100);
        if (randomnumber >= 1 && randomnumber <=weaponDropRate)
        {
            print("drop gun");
            Destroy(gameObject);
        }
        //else if(randomnumber!=1) 
        //    randomnumber = Random.Range(1, moduleDropRate);

        if (randomnumber > weaponDropRate && randomnumber <= moduleDropRate)
        {
            print("drop module");
            Destroy(gameObject);
        }
        //else if (randomnumber != 1) 
        //    randomnumber = Random.Range(1, consummableDropRate);

        if (randomnumber > moduleDropRate && randomnumber <= consummableDropRate)
        {
            print("drop consummable");
            Destroy(gameObject);
        }

        if (randomnumber > consummableDropRate)
        {
            print("No Drop!");
            Destroy(gameObject);
        }
        //else if (randomnumber != 1)
        //{
        //    print("no loot");
        //    Destroy(gameObject);
        //}

    }
}
