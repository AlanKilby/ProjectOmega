using System.Collections;
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

    [SerializeField] float delayBetweenTentacleAndShoot;
    [SerializeField] float delayBetweenShootAndSpray;
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
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            if (canLaunchPhaseThree)
            {
                StartCoroutine(coAtt);
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
        ChangeAnimationState(BOSSIDLE);
        yield return new WaitForSeconds(delayBetweenTentacleAndShoot);
        BSOn.StartAcideShoot();
        ChangeAnimationState(BOSSSPIDERSHOW);
        yield return new WaitForSeconds(delayBetweenShootAndSpray);
        StartCoroutine(BSOn.AcideSprayShoot());
        ChangeAnimationState(BOSSACIDESPRAY);
        yield return new WaitForSeconds(delayBetweenSprayAndEgg);
        BSTh.LaunchEggs();
        ChangeAnimationState(BOSSIDLE);
        yield return new WaitForSeconds(delayBetweenEggAndTentacle);
        ChangeAnimationState(BOSSIDLE);
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
