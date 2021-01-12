using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageOneManagement : MonoBehaviour
{
    UD_BossStageOne BSOn;

    public CameraSystem thisRoom;

    bool canLaunchPhaseOne;

    [SerializeField] float delayBetweenSprayAndShoot;
    [SerializeField] float delayBetweenShootAndSpray;
    void Start()
    {
        BSOn = GetComponent<UD_BossStageOne>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseOne = true;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            if (canLaunchPhaseOne)
            {
                StartCoroutine(PhaseOneAttack());
                canLaunchPhaseOne = false;
            }
        }
    }

    IEnumerator PhaseOneAttack()
    {
        StartCoroutine(BSOn.AcideSprayShoot());
        yield return new WaitForSeconds(delayBetweenSprayAndShoot);
        BSOn.StartAcideShoot();
        yield return new WaitForSeconds(delayBetweenShootAndSpray);
        canLaunchPhaseOne = true;
    }
}
