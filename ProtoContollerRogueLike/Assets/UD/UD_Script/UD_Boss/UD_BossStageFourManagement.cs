using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageFourManagement : MonoBehaviour
{
    UD_BossBase BB;
    UD_BossStageOne BSOn;
    UD_BossStageTwo BSTw;
    UD_BossStageThree BSTh;
    UD_BossStageFour BSFo;

    public CameraSystem thisRoom;

    bool canLaunchPhaseFour;

    private Animator anim;
    private string currentAnimation;
    const string BOSSIDLE = "BossIdle";
    const string BOSSDIE = "BossDie";
    const string BOSSACIDESPRAY = "BossAcideSpray";
    const string BOSSSPIDERSHOW = "BossSpiderShow";
    const string BOSSSPIDERHIDE = "BossSpiderHide";
    const string BOSSTOXSHOOT = "BossToxShoot";

    [SerializeField] float delayBetweenTentacleAndShoot;
    [SerializeField] float delayBetweenShootAndSpray;
    [SerializeField] float delayAfterSpiderShoot;
    [SerializeField] float delayBeforeSprayLaunch;
    [SerializeField] float delayBetweenSprayAndTox;
    [SerializeField] float delayBetweenToxAndEgg;
    [SerializeField] float delayBetweenEggAndTentacle;

    IEnumerator coAtt;

    void Start()
    {
        anim = GetComponent<Animator>();
        BB = GetComponent<UD_BossBase>();
        BSOn = GetComponent<UD_BossStageOne>();
        BSTw = GetComponent<UD_BossStageTwo>();
        BSTh = GetComponent<UD_BossStageThree>();
        BSFo = GetComponent<UD_BossStageFour>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseFour = true;
        coAtt = PhaseFourAttack();
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerCamera)
        {
            if (canLaunchPhaseFour && BB.isAlive)
            {
                ChangeAnimationState(BOSSIDLE);
                //StartCoroutine(coAtt);
                StartCoroutine(PhaseFourAttack());
                canLaunchPhaseFour = false;
            }
        }
        if (!BB.isAlive)
        {
            canLaunchPhaseFour = false;
            StopCoroutine(coAtt);
            Die();
        }
    }

    IEnumerator PhaseFourAttack()
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
        yield return new WaitForSeconds(delayBetweenSprayAndTox);
        StartCoroutine(BSFo.StageFourAttack());
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSTOXSHOOT);
        }
        yield return new WaitForSeconds(delayBetweenToxAndEgg);
        BSTh.LaunchEggs();
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSIDLE);
        }
        yield return new WaitForSeconds(delayBetweenEggAndTentacle);
        canLaunchPhaseFour = true;
    }

    public void Die()
    {
        ChangeAnimationState(BOSSDIE);
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
