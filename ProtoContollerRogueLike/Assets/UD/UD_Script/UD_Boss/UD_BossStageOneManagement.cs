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
    const string BOSSACIDESPRAY = "BossAcideSpray";
    const string BOSSFLEE = "BossFlee";
    const string BOSSSPIDERSHOW = "BossSpiderShow";

    [SerializeField] float delayBetweenSprayAndShoot;
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
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            if (canLaunchPhaseOne)
            {
                StartCoroutine(coAtt);
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
        ChangeAnimationState(BOSSACIDESPRAY);
        yield return new WaitForSeconds(delayBetweenSprayAndShoot);
        BSOn.StartAcideShoot();
        ChangeAnimationState(BOSSSPIDERSHOW);
        yield return new WaitForSeconds(delayBetweenShootAndSpray);
        ChangeAnimationState(BOSSIDLE);
        canLaunchPhaseOne = true;
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
