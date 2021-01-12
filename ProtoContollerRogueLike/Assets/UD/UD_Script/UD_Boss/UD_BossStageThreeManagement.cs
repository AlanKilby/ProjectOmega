using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageThreeManagement : MonoBehaviour
{
    UD_BossStageOne BSOn;
    UD_BossStageTwo BSTw;
    UD_BossStageThree BSTh;

    public CameraSystem thisRoom;

    bool canLaunchPhaseThree;

    [SerializeField] float delayBetweenTentacleAndShoot;
    [SerializeField] float delayBetweenShootAndSpray;
    [SerializeField] float delayBetweenSprayAndEgg;
    [SerializeField] float delayBetweenEggAndTentacle;

    void Start()
    {
        BSOn = GetComponent<UD_BossStageOne>();
        BSTw = GetComponent<UD_BossStageTwo>();
        BSTh = GetComponent<UD_BossStageThree>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseThree = true;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            if (canLaunchPhaseThree)
            {
                StartCoroutine(PhaseThreeAttack());
                canLaunchPhaseThree = false;
            }
        }
    }

    IEnumerator PhaseThreeAttack()
    {
        BSTw.LaunchTentacle();
        yield return new WaitForSeconds(delayBetweenTentacleAndShoot);
        BSOn.StartAcideShoot();
        yield return new WaitForSeconds(delayBetweenShootAndSpray);
        StartCoroutine(BSOn.AcideSprayShoot());
        yield return new WaitForSeconds(delayBetweenSprayAndEgg);
        BSTh.LaunchEggs();
        yield return new WaitForSeconds(delayBetweenEggAndTentacle);
        canLaunchPhaseThree = true;
    }
}
