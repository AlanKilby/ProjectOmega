using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleExplosiveCharge : MonoBehaviour
{
    Inventory In;
    PlayerModuleStation PMS;
    [SerializeField] Module Mo;

    private void Start()
    {
        PMS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModuleStation>();
        In = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            for (int i = 0; i < In.slots.Length; i++)
            {
                if (In.isFullModule[i] == false)
                {
                    In.moduleGameObject[i] = gameObject; //Met le module dans la liste

                    Mo.moduleSlot = i; //Attribue un slot au Module

                    In.isFullModule[i] = true; //Fait que le Slot ne puisse plus être utilisé

                    Instantiate(Mo.moduleIcon, In.moduleSlots[i].transform, false);

                    transform.SetParent(In.moduleSlots[i].transform);

                    PMS.moduleExplosiveCharge = true;

                    gameObject.SetActive(false);

                    break;
                }

            }
        }
    }
}
