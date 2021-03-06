﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_NukeConsumable : MonoBehaviour
{
    public CameraSystem thisRoom;

    public int nukeDamage;

    UI_NukeFeedbackEffect nukeFeedback;

    private void Start()
    {
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        nukeFeedback = GameObject.Find("NukeFeedback").GetComponent<UI_NukeFeedbackEffect>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            nukeFeedback.explode = true;
            FindObjectOfType<AudioManager>().Play("Nuke");
            foreach(Transform child1 in thisRoom.transform)
            {
                if (child1.CompareTag("Ennemi"))
                {
                    /*foreach (Transform child2 in thisRoom.transform)
                    {
                        if (child2.CompareTag("Ennemi"))
                        {
                            foreach (Transform child3 in thisRoom.transform)
                            {
                                if (child3.CompareTag("Ennemi"))
                                {
                                    child3.GetComponent<EnnemisScript>().TakeDamage(nukeDamage);
                                }
                            }
                            child2.GetComponent<EnnemisScript>().TakeDamage(nukeDamage);
                        }
                    }*/ // ECLATE
                    child1.GetComponent<EnnemisScript>().TakeDamage(nukeDamage);
                }
            }
            Destroy(gameObject);
        }
    }
}
