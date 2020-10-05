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
        
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if(willDrop == true)
        {
            ChooseDropType();
            willDrop = false;
        }
    }

    public void LanchDrop()
    {
        willDrop = true;
    }

    private void ChooseDropType()
    {
        int randomnumber = Random.Range(1, weaponDropRate);
        if (randomnumber == 1)
        {
            print("drop gun");
            Destroy(gameObject);
        }
        else if(randomnumber!=1) randomnumber = Random.Range(1, moduleDropRate);
        if (randomnumber == 1)
        {
            print("drop module");
            Destroy(gameObject);
        }
        else if (randomnumber != 1) randomnumber = Random.Range(1, consummableDropRate);
        if (randomnumber == 1)
        {
            print("drop consummable");
            Destroy(gameObject);
        }
        else if (randomnumber != 1) print("no loot"); Destroy(gameObject);
    }
}
