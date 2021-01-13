using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageOneManagement : MonoBehaviour
{
    UD_BossStageOne BSOn;

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
    void Start()
    {
        BSOn = GetComponent<UD_BossStageOne>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canLaunchPhaseOne = true;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            if (canLaunchPhaseOne)
            {
                StartCoroutine(PhaseOneAttack());
                canLaunchPhaseOne = false;
            }
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
