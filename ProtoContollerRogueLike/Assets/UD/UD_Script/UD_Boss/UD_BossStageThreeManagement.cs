﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageThreeManagement : MonoBehaviour
{
    UD_BossStageOne BSOn;
    UD_BossStageTwo BSTw;
    UD_BossStageThree BSTh;
    UD_BossBase BB;

    public CameraSystem thisRoom;

    bool canLaunchPhaseThree;

    private Animator anim;
    private string currentAnimation;
    const string BOSSIDLE = "BossIdle";
    const string BOSSFLEE = "BossFlee";
    const string BOSSACIDESPRAY = "BossAcideSpray";
    const string BOSSSPIDERSHOW = "BossSpiderShow";
    const string BOSSSPIDERHIDE = "BossSpiderHide";

    [SerializeField] float delayBetweenTentacleAndShoot;
    [SerializeField] float delayBetweenShootAndSpray;
    [SerializeField] float delayAfterSpiderShoot;
    [SerializeField] float delayBeforeSprayLaunch;
    [SerializeField] float delayBetweenSprayAndEgg;
    [SerializeField] float delayBetweenEggAndTentacle;

    IEnumerator coAtt;

    void Start()
    {
        anim = GetComponent<Animator>();
        BB = GetComponent<UD_BossBase>();
        BSOn = GetComponent<UD_BossStageOne>();
        BSTw = GetComponent<UD_BossStageTwo>();
        BSTh = GetComponent<UD_BossStageThree>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseThree = true;
        coAtt = PhaseThreeAttack();
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerCamera)
        {
            if (canLaunchPhaseThree && BB.isAlive)
            {
                ChangeAnimationState(BOSSIDLE);
                //StartCoroutine(coAtt);
                StartCoroutine(PhaseThreeAttack());
                canLaunchPhaseThree = false;
            }
        }
        if (!BB.isAlive)
        {
            canLaunchPhaseThree = false;
            StopCoroutine(coAtt);
            Flee();
        }
    }

    IEnumerator PhaseThreeAttack()
    {
        BSTw.LaunchTentacle();
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSIDLE);
        }
        yield return new WaitForSeconds(delayBetweenTentacleAndShoot);
        BSOn.StartAcideShoot();
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSSPIDERSHOW);
        }
        yield return new WaitForSeconds(delayBetweenShootAndSpray);
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSSPIDERHIDE);
        }
        yield return new WaitForSeconds(delayAfterSpiderShoot);
        StartCoroutine(BSOn.AcideSprayShoot());
        yield return new WaitForSeconds(delayBeforeSprayLaunch);
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSACIDESPRAY);
        }
        yield return new WaitForSeconds(delayBetweenSprayAndEgg);
        BSTh.LaunchEggs();
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSIDLE);
        }
        yield return new WaitForSeconds(delayBetweenEggAndTentacle);
        canLaunchPhaseThree = true;
    }

    public void Flee()
    {
        ChangeAnimationState(BOSSFLEE);
    }

    public void DestroyHimself()
    {
        BB.Death();
    }

    public void ReturnToIdleAnim()
    {
        ChangeAnimationState(BOSSIDLE);
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);
        currentAnimation = newAnimation;
    }
}
