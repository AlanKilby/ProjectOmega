using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleGiveCheat : MonoBehaviour
{
    Button bu;

    SwordAttack SA;
    Inventory In;
    PlayerModuleStation PMS;
    PlayerHealth PH;
    PlayerMouvement PM;
    PhaseShiftHit PSH;

    [SerializeField] GameObject vampireIcon, soulScreamIcon, reviveIcon, phaseShiftIcon, explosiveChargeIcon, counterBladeIcon;

    [SerializeField] float healthRegenWithQuickRevive;
    [SerializeField] private int damageDealedByHit;

    private void Start()
    {
        SA = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordAttack>();
        In = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        PMS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModuleStation>();
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        PSH = GameObject.FindGameObjectWithTag("PhaseShiftDetector").GetComponent<PhaseShiftHit>();

        bu = GetComponent<Button>();
    }

    private void Update()
    {
        if (UI_PointInfoCheat.moduleCheatEnable)
        {
            bu.interactable = true;
        }
        else
        {
            bu.interactable = false;
        }
    }

    public void UnlockVampireModule()
    {
        if (UI_PointInfoCheat.moduleCheatEnable)
        {
            for (int i = 0; i < In.moduleSlots.Length; i++)
            {
                if (In.isFullModule[i] == false)
                {
                    FindObjectOfType<AudioManager>().Play("Vampire Sword Announcer");

                    In.moduleGameObject[i] = gameObject; //Met le module dans la liste

                    In.isFullModule[i] = true; //Fait que le Slot ne puisse plus être utilisé

                    GameObject mod = Instantiate(vampireIcon, In.moduleSlots[i].transform, false);

                    mod.transform.SetParent(In.moduleSlots[i].transform);

                    SA.isVampire = true;

                    break;
                }

            }
        }
    }

    public void UnlockSoulScreamModule()
    {
        if (UI_PointInfoCheat.moduleCheatEnable)
        {
            for (int i = 0; i < In.moduleSlots.Length; i++)
            {
                if (In.isFullModule[i] == false)
                {
                    FindObjectOfType<AudioManager>().Play("Piercing Ammo Announcer");

                    In.moduleGameObject[i] = gameObject; //Met le module dans la liste

                    In.isFullModule[i] = true; //Fait que le Slot ne puisse plus être utilisé

                    GameObject mod = Instantiate(soulScreamIcon, In.moduleSlots[i].transform, false);

                    mod.transform.SetParent(In.moduleSlots[i].transform);

                    PMS.soulScream = true;

                    break;
                }

            }
        }
    }

    public void UnlockReviveModule()
    {
        if (UI_PointInfoCheat.moduleCheatEnable)
        {
            for (int i = 0; i < In.moduleSlots.Length; i++)
            {
                if (In.isFullModule[i] == false)
                {
                    FindObjectOfType<AudioManager>().Play("Revive Device Announcer");

                    In.moduleGameObject[i] = gameObject; //Met le module dans la liste

                    In.isFullModule[i] = true; //Fait que le Slot ne puisse plus être utilisé

                    GameObject mod = Instantiate(reviveIcon, In.moduleSlots[i].transform, false);

                    mod.transform.SetParent(In.moduleSlots[i].transform);

                    PH.healthRegenWithQuickRevive = healthRegenWithQuickRevive;

                    PH.hasQuickRevive = true;

                    i = PH.quickReviveIconSlot;

                    break;
                }

            }
        }
    }
    
    public void UnlockPhaseShiftModule()
    {
        if (UI_PointInfoCheat.moduleCheatEnable)
        {
            for (int i = 0; i < In.moduleSlots.Length; i++)
            {
                if (In.isFullModule[i] == false)
                {
                    FindObjectOfType<AudioManager>().Play("Offensive Dash Announcer");

                    In.moduleGameObject[i] = gameObject; //Met le module dans la liste

                    In.isFullModule[i] = true; //Fait que le Slot ne puisse plus être utilisé

                    GameObject mod = Instantiate(phaseShiftIcon, In.moduleSlots[i].transform, false);

                    mod.transform.SetParent(In.moduleSlots[i].transform);

                    PM.hasModulePhaseShift = true;

                    PSH.damageDealed = damageDealedByHit;

                    break;
                }

            }
        }
    }

    public void UnlockExplosiveChargeModule()
    {
        if (UI_PointInfoCheat.moduleCheatEnable)
        {
            for (int i = 0; i < In.moduleSlots.Length; i++)
            {
                if (In.isFullModule[i] == false)
                {
                    FindObjectOfType<AudioManager>().Play("Explosive Ammo Announcer");

                    In.moduleGameObject[i] = gameObject; //Met le module dans la liste

                    In.isFullModule[i] = true; //Fait que le Slot ne puisse plus être utilisé

                    GameObject mod = Instantiate(explosiveChargeIcon, In.moduleSlots[i].transform, false);

                    mod.transform.SetParent(In.moduleSlots[i].transform);

                    PMS.moduleExplosiveCharge = true;

                    break;
                }

            }
        }
    }

    public void UnlockCounterBladeModule()
    {
        if (UI_PointInfoCheat.moduleCheatEnable)
        {
            for (int i = 0; i < In.moduleSlots.Length; i++)
            {
                if (In.isFullModule[i] == false)
                {
                    FindObjectOfType<AudioManager>().Play("Parry Bullets Announcer");

                    In.moduleGameObject[i] = gameObject; //Met le module dans la liste

                    In.isFullModule[i] = true; //Fait que le Slot ne puisse plus être utilisé

                    GameObject mod = Instantiate(counterBladeIcon, In.moduleSlots[i].transform, false);

                    mod.transform.SetParent(In.moduleSlots[i].transform);

                    SA.hasCounterBlade = true;

                    break;
                }

            }
        }
    }
}
