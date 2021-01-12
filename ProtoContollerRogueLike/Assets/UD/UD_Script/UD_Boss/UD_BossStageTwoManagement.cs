using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageTwoManagement : MonoBehaviour
{
    UD_BossStageOne BSOn;
    UD_BossStageTwo BSTw;

    public CameraSystem thisRoom;

    bool canLaunchPhaseTwo;

    [SerializeField] float delayBetweenTentacleAndShoot;
    [SerializeField] float delayBetweenShootAndSpray;
    [SerializeField] float delayBetweenSprayAndTentacle;

    void Start()
    {
        BSOn = GetComponent<UD_BossStageOne>();
        BSTw = GetComponent<UD_BossStageTwo>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseTwo = true;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            if (canLaunchPhaseTwo)
            {
                StartCoroutine(PhaseTwoAttack());
                canLaunchPhaseTwo = false;
            }
        }
    }

    IEnumerator PhaseTwoAttack()
    {
        BSTw.LaunchTentacle();
        yield return new WaitForSeconds(delayBetweenTentacleAndShoot);
        BSOn.StartAcideShoot();
        yield return new WaitForSeconds(delayBetweenShootAndSpray);
        StartCoroutine(BSOn.AcideSprayShoot());
        yield return new WaitForSeconds(delayBetweenSprayAndTentacle);
        canLaunchPhaseTwo = true;
    }
}
