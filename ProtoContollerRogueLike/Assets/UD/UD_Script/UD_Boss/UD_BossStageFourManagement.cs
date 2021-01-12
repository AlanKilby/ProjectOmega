using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageFourManagement : MonoBehaviour
{
    UD_BossStageOne BSOn;
    UD_BossStageTwo BSTw;
    UD_BossStageThree BSTh;
    UD_BossStageFour BSFo;

    public CameraSystem thisRoom;

    bool canLaunchPhaseFour;

    [SerializeField] float delayBetweenTentacleAndShoot;
    [SerializeField] float delayBetweenShootAndSpray;
    [SerializeField] float delayBetweenSprayAndTox;
    [SerializeField] float delayBetweenToxAndEgg;
    [SerializeField] float delayBetweenEggAndTentacle;

    void Start()
    {
        BSOn = GetComponent<UD_BossStageOne>();
        BSTw = GetComponent<UD_BossStageTwo>();
        BSTh = GetComponent<UD_BossStageThree>();
        BSFo = GetComponent<UD_BossStageFour>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseFour = true;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            if (canLaunchPhaseFour)
            {
                StartCoroutine(PhaseFourAttack());
                canLaunchPhaseFour = false;
            }
        }
    }

    IEnumerator PhaseFourAttack()
    {
        BSTw.LaunchTentacle();
        yield return new WaitForSeconds(delayBetweenTentacleAndShoot);
        BSOn.StartAcideShoot();
        yield return new WaitForSeconds(delayBetweenShootAndSpray);
        StartCoroutine(BSOn.AcideSprayShoot());
        yield return new WaitForSeconds(delayBetweenSprayAndTox);
        StartCoroutine(BSFo.StageFourAttack());
        yield return new WaitForSeconds(delayBetweenToxAndEgg);
        BSTh.LaunchEggs();
        yield return new WaitForSeconds(delayBetweenEggAndTentacle);
        canLaunchPhaseFour = true;
    }
}
