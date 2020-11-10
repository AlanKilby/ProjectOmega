using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //  Gun Inventory
    public GameObject gunHolder;
    public bool[] isFull;
    public string[] gunID;
    public int[] ammoCounter;
    public GameObject[] gunGameObject;
    public GameObject[] slots;
    public int equippedSlot;


    // Module Inventory
    public bool[] isFullModule;
    public GameObject[] moduleSlots;
    public GameObject[] moduleGameObject;




    private void Update()
    {   
        // This changes the weapon slot
        if (Input.GetKeyDown("1"))
        {
            equippedSlot = 0;
        }
        if (Input.GetKeyDown("2"))
        {
            equippedSlot = 1;
        }
        if (Input.GetKeyDown("3"))
        {
            equippedSlot = 2;
        }


        // This enables and disables the GUN's sprite if it's equipped or not
        for (int i = 0; i < isFull.Length; i++)
        {
            if (equippedSlot != gunGameObject[i].GetComponent<Weapon>().gunSlot)
            {
                gunGameObject[i].GetComponent<Weapon>().gunSpriteRenderer.enabled = false;
            }
            if (equippedSlot == gunGameObject[i].GetComponent<Weapon>().gunSlot)
            {
                gunGameObject[i].GetComponent<Weapon>().gunSpriteRenderer.enabled = true;
            }


        }

        



        /*for (int i = 0; i < isFullModule.Length; i++)
        {
            if (equippedSlot != moduleGameObject[i].GetComponent<Module>().moduleSlot)
            {
                gunGameObject[i].GetComponent<Weapon>().gunSpriteRenderer.enabled = false;
            }
            if (equippedSlot == moduleGameObject[i].GetComponent<Module>().moduleSlot)
            {
                gunGameObject[i].GetComponent<Weapon>().gunSpriteRenderer.enabled = true;
            }

        }*/
    }
}
