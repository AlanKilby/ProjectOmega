using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePhaseShift : MonoBehaviour
{
    PlayerMouvement PM;
    Inventory In;
    PhaseShiftHit PSH;
    [SerializeField] Module Mo;

    [SerializeField] private int damageDealedByHit;

    private void Start()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        In = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        PSH = GameObject.FindGameObjectWithTag("PhaseShiftDetector").GetComponent<PhaseShiftHit>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            for (int i = 0; i < In.moduleSlots.Length; i++)
            {
                if (In.isFullModule[i] == false)
                {
                    Mo.LaunchPopUp();

                    In.moduleGameObject[i] = gameObject; //Met le module dans la liste

                    Mo.moduleSlot = i; //Attribue un slot au Module

                    In.isFullModule[i] = true; //Fait que le Slot ne puisse plus être utilisé

                    Instantiate(Mo.moduleIcon, In.moduleSlots[i].transform, false);

                    transform.SetParent(In.moduleSlots[i].transform);

                    PM.hasModulePhaseShift = true;

                    PSH.damageDealed = damageDealedByHit;

                    gameObject.SetActive(false);

                    break;
                }

            }
        }
    }
}
