using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageOneManagement : MonoBehaviour
{
    UD_BossStageOne BSOn;
    UD_BossBase BB;

    public CameraSystem thisRoom;

    bool canLaunchPhaseOne;

    private Animator anim;
    private string currentAnimation;
    const string BOSSIDLE = "BossIdle";
    const string BOSSFLEE = "BossFlee";
    const string BOSSACIDESPRAY = "BossAcideSpray";
    const string BOSSSPIDERSHOW = "BossSpiderShow";
    const string BOSSSPIDERHIDE = "BossSpiderHide";

    [SerializeField] float delayBeforeSprayLaunch;
    [SerializeField] float delayBetweenSprayAndShoot;
    [SerializeField] float delayAfterSpiderShoot;
    [SerializeField] float delayBetweenShootAndSpray;

    IEnumerator coAtt;

    void Start()
    {
        anim = GetComponent<Animator>();
        BB = GetComponent<UD_BossBase>();
        BSOn = GetComponent<UD_BossStageOne>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        coAtt = PhaseOneAttack();
        canLaunchPhaseOne = true;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerCamera)
        { 
            if (canLaunchPhaseOne && BB.isAlive)
            {
                //FindObjectOfType<AudioManager>().Play("Boss Music"); 
                ChangeAnimationState(BOSSIDLE);
                FindObjectOfType<AudioManager>().Play("Boss Idle");
                //StartCoroutine(coAtt);
                StartCoroutine(PhaseOneAttack());
                canLaunchPhaseOne = false;
            }
        }
        if (!BB.isAlive)
        {
            canLaunchPhaseOne = false;
            StopCoroutine(coAtt);
            Flee();
        }
    }

    IEnumerator PhaseOneAttack()
    {
        StartCoroutine(BSOn.AcideSprayShoot());
        FindObjectOfType<AudioManager>().Play("Boss Acid Charge");
        yield return new WaitForSeconds(delayBeforeSprayLaunch);
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSACIDESPRAY);
            FindObjectOfType<AudioManager>().Play("Boss Acid Spit");
        }
        yield return new WaitForSeconds(delayBetweenSprayAndShoot);
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
        canLaunchPhaseOne = true;
    }

    public void EscapeSound()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Boss Acid Charge");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Acid Spit");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Spider Shot");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Idle");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Music");
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
