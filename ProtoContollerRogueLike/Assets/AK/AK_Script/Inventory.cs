using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public bool[] isFullModule;
    public string[] gunID;
    public GameObject[] gunGameObject;
    public GameObject[] moduleGameObject;
    public GameObject[] slots;
    public GameObject[] moduleSlots;

    public int ammoCounter;
    public int ammoCounterAA12;

    public int equippedSlot;

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            equippedSlot = 0;
        }
        if (Input.GetKeyDown("2"))
        {
            equippedSlot = 1;
        }

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
