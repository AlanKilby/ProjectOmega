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
                //FindObjectOfType<AudioManager>().Play("Boss Music"); 
                ChangeAnimationState(BOSSIDLE);
                FindObjectOfType<AudioManager>().Play("Boss Idle");
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
        FindObjectOfType<AudioManager>().Play("Boss Spider Shot");
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
        FindObjectOfType<AudioManager>().Play("Boss Acid Charge");
        yield return new WaitForSeconds(delayBeforeSprayLaunch);
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSACIDESPRAY);
            FindObjectOfType<AudioManager>().Play("Boss Acid Spit");
        }
        yield return new WaitForSeconds(delayBetweenSprayAndEgg);
        FindObjectOfType<AudioManager>().Play("Boss Egg Launch");
        BSTh.LaunchEggs();
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSIDLE);
        }
        yield return new WaitForSeconds(delayBetweenEggAndTentacle);
        canLaunchPhaseThree = true;
    }
    public void EscapeSound()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Boss Acid Charge");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Acid Spit");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Spider Shot");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Idle");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Music");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Egg Launch");
        FindObjectOfType<AudioManager>().Play("Boss Escape");
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
