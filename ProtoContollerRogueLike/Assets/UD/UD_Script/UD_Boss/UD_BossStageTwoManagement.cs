using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageTwoManagement : MonoBehaviour
{
    UD_BossStageOne BSOn;
    UD_BossStageTwo BSTw;
    UD_BossBase BB;

    public CameraSystem thisRoom;

    bool canLaunchPhaseTwo;

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
    [SerializeField] float delayBetweenSprayAndTentacle;

    IEnumerator coAtt;

    void Start()
    {
        anim = GetComponent<Animator>();
        BSOn = GetComponent<UD_BossStageOne>();
        BSTw = GetComponent<UD_BossStageTwo>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseTwo = true;
        coAtt = PhaseTwoAttack();
        BB = GetComponent<UD_BossBase>();
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerCamera)
        {
            if (canLaunchPhaseTwo)
            {
                ChangeAnimationState(BOSSIDLE);
                //StartCoroutine(coAtt);
                StartCoroutine(PhaseTwoAttack());
                canLaunchPhaseTwo = false;
            }
        }
        if (!BB.isAlive)
        {
            canLaunchPhaseTwo = false;
            StopCoroutine(coAtt);
            Flee();
        }
    }

    IEnumerator PhaseTwoAttack()
    {
        BSTw.LaunchTentacle();
        ChangeAnimationState(BOSSIDLE);
        yield return new WaitForSeconds(delayBetweenTentacleAndShoot);
        BSOn.StartAcideShoot();
        ChangeAnimationState(BOSSSPIDERSHOW);
        yield return new WaitForSeconds(delayBetweenShootAndSpray);
        ChangeAnimationState(BOSSSPIDERHIDE);
        yield return new WaitForSeconds(delayAfterSpiderShoot);
        StartCoroutine(BSOn.AcideSprayShoot());
        yield return new WaitForSeconds(delayBeforeSprayLaunch);
        ChangeAnimationState(BOSSACIDESPRAY);
        yield return new WaitForSeconds(delayBetweenSprayAndTentacle);
        canLaunchPhaseTwo = true;
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
