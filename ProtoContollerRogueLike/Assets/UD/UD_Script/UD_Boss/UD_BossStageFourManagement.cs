using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageFourManagement : MonoBehaviour
{
    FadeSceneManagerScript FSMS;

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
    const string BOSSDIE = "BossDeathAnim";
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
        FSMS = GameObject.Find("FadeManager").GetComponent<FadeSceneManagerScript>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseFour = true;
        coAtt = PhaseFourAttack();
        AudioManager.volumeSlider = 1f;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerCamera)
        {
            if (canLaunchPhaseFour && BB.isAlive)
            {
                //FindObjectOfType<AudioManager>().Play("Boss Music"); 
                ChangeAnimationState(BOSSIDLE);
                FindObjectOfType<AudioManager>().Play("Boss Idle");
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
        yield return new WaitForSeconds(delayBetweenSprayAndTox);
        StartCoroutine(BSFo.StageFourAttack());
        if (BB.isAlive)
        {
            ChangeAnimationState(BOSSTOXSHOOT);
            FindObjectOfType<AudioManager>().Play("Boss Toxic Shot");
        }
        yield return new WaitForSeconds(delayBetweenToxAndEgg);
        FindObjectOfType<AudioManager>().Play("Boss Egg Launch");
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
        FindObjectOfType<AudioManager>().StopPlaying("Boss Acid Charge");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Acid Spit");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Spider Shot");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Idle");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Music");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Egg Launch");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Toxic Shot");
    }

    public void DeathSound()
    {
        FindObjectOfType<AudioManager>().Play("Boss Death");
    }

    public void DestroyHimself()
    {
        FadeSceneManagerScript.whatTransition = SceneTransition.Outro;
        FindObjectOfType<AudioManager>().Play("Fight Music");
        FSMS.FadeOut();
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
