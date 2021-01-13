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

    private Animator anim;
    private string currentAnimation;
    const string BOSSIDLE = "BossIdle";
    const string BOSSDIE = "BossDie";
    const string BOSSACIDESPRAY = "BossAcideSpray";
    const string BOSSSPIDERSHOW = "BossSpiderShow";
    const string BOSSTOXSHOOT = "BossToxShoot";

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
        ChangeAnimationState(BOSSIDLE);
        yield return new WaitForSeconds(delayBetweenTentacleAndShoot);
        BSOn.StartAcideShoot();
        ChangeAnimationState(BOSSSPIDERSHOW);
        yield return new WaitForSeconds(delayBetweenShootAndSpray);
        StartCoroutine(BSOn.AcideSprayShoot());
        ChangeAnimationState(BOSSACIDESPRAY);
        yield return new WaitForSeconds(delayBetweenSprayAndTox);
        StartCoroutine(BSFo.StageFourAttack());
        ChangeAnimationState(BOSSTOXSHOOT);
        yield return new WaitForSeconds(delayBetweenToxAndEgg);
        BSTh.LaunchEggs();
        ChangeAnimationState(BOSSIDLE);
        yield return new WaitForSeconds(delayBetweenEggAndTentacle);
        ChangeAnimationState(BOSSIDLE);
        canLaunchPhaseFour = true;
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
