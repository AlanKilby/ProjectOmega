using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageTwoManagement : MonoBehaviour
{
    UD_BossStageOne BSOn;
    UD_BossStageTwo BSTw;

    public CameraSystem thisRoom;

    bool canLaunchPhaseTwo;

    private Animator anim;
    private string currentAnimation;
    const string BOSSIDLE = "BossIdle";
    const string BOSSFLEE = "BossFlee";
    const string BOSSACIDESPRAY = "BossAcideSpray";
    const string BOSSSPIDERSHOW = "BossSpiderShow";

    [SerializeField] float delayBetweenTentacleAndShoot;
    [SerializeField] float delayBetweenShootAndSpray;
    [SerializeField] float delayBetweenSprayAndTentacle;

    void Start()
    {
        BSOn = GetComponent<UD_BossStageOne>();
        BSTw = GetComponent<UD_BossStageTwo>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseTwo = true;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            if (canLaunchPhaseTwo)
            {
                StartCoroutine(PhaseTwoAttack());
                canLaunchPhaseTwo = false;
            }
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
        StartCoroutine(BSOn.AcideSprayShoot());
        ChangeAnimationState(BOSSACIDESPRAY);
        yield return new WaitForSeconds(delayBetweenSprayAndTentacle);
        ChangeAnimationState(BOSSIDLE);
        canLaunchPhaseTwo = true;
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
