﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleVampire : MonoBehaviour
{
    SwordAttack SA;
    Inventory In;
    [SerializeField] Module Mo;

    private void Start()
    {
        SA = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordAttack>();
        In = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            for (int i = 0; i < In.moduleSlots.Length; i++)
            {
                if (In.isFullModule[i] == false)
                {
                    FindObjectOfType<AudioManager>().Play("Vampire Sword Announcer"); 
                    
                    Mo.LaunchPopUp();

                    In.moduleGameObject[i] = gameObject; //Met le module dans la liste

                    Mo.moduleSlot = i; //Attribue un slot au Module

                    In.isFullModule[i] = true; //Fait que le Slot ne puisse plus être utilisé

                    Instantiate(Mo.moduleIcon, In.moduleSlots[i].transform, false);

                    transform.SetParent(In.moduleSlots[i].transform);

                    SA.isVampire = true;

                    gameObject.SetActive(false);

                    break;
                }

            }
        }
    }
}
