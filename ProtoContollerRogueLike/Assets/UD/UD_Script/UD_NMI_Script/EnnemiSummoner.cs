using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnnemiSummoner : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public LayerMask whatIsEnvironement;

    [SerializeField] private Animator anim;
    EnnemisScript ES;
    DifficultyPanel DP;

    public float fleeSpeed;
    public float summonRate;
    private float summonRateTimer;
    public float summoningLenght;
    private float summoningTimer;
    [SerializeField] private float ownRBvelocityX;
    [SerializeField] private float ownRBvelocityY;
    [SerializeField] [Range(0.0f, 1.0f)] private float lastHopeShootvelocityLimiteX;
    [SerializeField] [Range(0.0f, 1.0f)] private float lastHopeShootvelocityLimiteY;

    private bool canSummon;
    private bool isSummoning;
    public bool playerInAffraidArea;
    public bool playerInDontMoveArea;
    public bool lastHopeSummon;
    public bool playerInSight;

    public Transform playerPos;
    public CameraSystem thisRoom;

    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject minionsPrefab;
    public GameObject ownAffraidArea;

    //Ajout Gus
    private string currentAnimation;

    const string SUMMONER_IDLE = "SummonerIdle";
    const string SUMMONER_CAST = "SummonerCast";
    const string SUMMONER_WALK = "SummonerWalk";
    //

    [SerializeField] private Rigidbody2D ownRB;



    void Start()
    {
        playerInAffraidArea = false;
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ownRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ES = GetComponent<EnnemisScript>();
        DP = GameObject.Find("DifficultyPanel").GetComponent<DifficultyPanel>();
        summonRate = (int)Mathf.Round((summonRate - DP.currentStageSpawnRateBonusForSummoner));
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom == true)
        {
            if (playerInSight)
            {
                LookToPlayer();
            }
            FireRateTimer();
            SummoningTimer();
            if (playerInSight && canSummon && (!playerInAffraidArea || lastHopeSummon))
            {
                summonRateTimer = summonRate;
                summoningTimer = summoningLenght;
                canSummon = false;
                isSummoning = true;
            }
            Move();
        }
        UpdateAnims();
    }

    private void FixedUpdate()
    {
        PlayerInSight();
    }

    void UpdateAnims()
    {
        //anim.SetBool("isSummoning", isSummoning);
        //ChangeAnimationState(SUMMONER_CAST);


    }

    public void StopSummoningAnim()
    {
        isSummoning = false;
        Summon();
        
    }

    private void Move()
    {
        /*if (!playerInSight)
        {
            gameObject.GetComponent<AIPath>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            ownAffraidArea.SetActive(false);
            playerInAffraidArea = false;
        }
        if (playerInSight && !isSummoning)
        {
            gameObject.GetComponent<AIPath>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            ownAffraidArea.SetActive(true);
        }
        if (playerInSight && isSummoning)
        {
            gameObject.GetComponent<AIPath>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            ownAffraidArea.SetActive(false);
            playerInAffraidArea = false;
        }*/
        
        if (!isSummoning && !playerInDontMoveArea)
        {
            gameObject.GetComponent<AIPath>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            ownAffraidArea.SetActive(true);
            ChangeAnimationState(SUMMONER_WALK);
        }
        if (isSummoning)
        {
            gameObject.GetComponent<AIPath>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            ownAffraidArea.SetActive(false);
            playerInAffraidArea = false;
            ChangeAnimationState(SUMMONER_CAST);
        }

        if (playerInAffraidArea)
        {
            gameObject.GetComponent<AIPath>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Vector3 fleeDirection = playerPos.position - gameObject.transform.position;
            fleeDirection.Normalize();
            if (ES.isStunned)
            {
                ownRB.AddForce(fleeDirection * (-fleeSpeed * (1 - ES.stunSlowPercentageEffect / 100)), ForceMode2D.Force);
            }
            else
            {
                ownRB.AddForce(fleeDirection * (-fleeSpeed), ForceMode2D.Force);
            }
            ChangeAnimationState(SUMMONER_WALK);

        }
        if (!playerInAffraidArea)
        {
            ownRB.velocity = new Vector2(0, 0);
            //ChangeAnimationState(SUMMONER_IDLE);

        }
        if (playerInDontMoveArea)
        {
            gameObject.GetComponent<AIPath>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            ChangeAnimationState(SUMMONER_WALK);

        }

        ownRBvelocityX = Mathf.Abs(ownRB.velocity.x);
        ownRBvelocityY = Mathf.Abs(ownRB.velocity.y);

        Vector2 lastHope = new Vector2(lastHopeShootvelocityLimiteX, lastHopeShootvelocityLimiteY);
        if (ownRBvelocityX <= lastHope.x && ownRBvelocityY <= lastHope.y)
        {
            lastHopeSummon = true;
        }
        else lastHopeSummon = false;
    }

    private void LookToPlayer()
    {
        // Cette partie du script fut trouvee sur le forum Unity https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html?_ga=2.230719519.1043224240.1601999147-1783980511.1597703941
        Vector3 diff = playerPos.position - gameObject.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void Summon()
    {
        spawnPoint1.GetComponent<EnnemiSummonerSpawner>().SpawnEnemy();
        spawnPoint2.GetComponent<EnnemiSummonerSpawner>().SpawnEnemy();
        spawnPoint3.GetComponent<EnnemiSummonerSpawner>().SpawnEnemy();
    }

    private void FireRateTimer()
    {
        if (!canSummon)
        {
            summonRateTimer -= Time.deltaTime;
            if (summonRateTimer < 0.0f)
            {
                canSummon = true;
            }
        }
    }
    private void SummoningTimer()
    {
        if (isSummoning)
        {
            summoningTimer -= Time.deltaTime;
            if (summoningTimer < 0.0f)
            {
                StopSummoningAnim();

            }
        }
    }

    private void PlayerInSight()
    {
        RaycastHit2D sight = Physics2D.Linecast(transform.position, playerPos.position, whatIsEnvironement);
        playerInSight = true;
        if(sight != false)
        {
            if (sight.collider.CompareTag("Environement"))
            {
                playerInSight = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, playerPos.position);
    }

    //Ajout Gus
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);

        currentAnimation = newAnimation;
    }
    //
}
