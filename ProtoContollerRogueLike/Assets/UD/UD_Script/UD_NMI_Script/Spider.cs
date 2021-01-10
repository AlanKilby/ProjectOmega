using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public LayerMask whatIsEnvironement;

    EnnemisScript ES;
    DifficultyPanel DP;

    // Ajout Gus
    private Animator anim;
    private string currentAnimation;
    //

    public float fleeSpeed;
    //public float affraidArea;
    public float spiderFireRate;
    private float spiderFireRateTimer;
    public float spiderImprecision;
    public float spiderBulletForce;
    [SerializeField] private float ownRBvelocityX;
    [SerializeField] private float ownRBvelocityY;
    [SerializeField] [Range(0.0f, 1.0f)] private float lastHopeShootvelocityLimiteX;
    [SerializeField] [Range(0.0f, 1.0f)] private float lastHopeShootvelocityLimiteY;

    //Ajout Gus
    const string SPIDERIDLE = "SpiderIdle";
    const string SPIDERWALK = "SpiderWalk";
    const string SPIDERATTACK = "SpiderAttack";
    //

    private bool canShoot;
    public bool playerInAffraidArea;
    public bool lastHopeShoot;
    public bool playerInSight;

    public Transform firePoint;
    public Transform playerPos;
    public CameraSystem thisRoom;

    public GameObject spiderBulletPrefab;
    public GameObject ownAffraidArea;
    [SerializeField] private Rigidbody2D ownRB;

    private void Start()
    {
        playerInAffraidArea = false;
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ownRB = GetComponent<Rigidbody2D>();
        ES = GetComponent<EnnemisScript>();
        DP = GameObject.Find("DifficultyPanel").GetComponent<DifficultyPanel>();

        //Ajout Gus
        anim = GetComponent<Animator>();
        //
    }
    private void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom == true)
        {
            if (playerInSight && playerPos != null)
            {
                LookToPlayer();
            }
            //CheckPlayerPosition();
            FireRateTimer();
            if(playerInSight && canShoot && (!playerInAffraidArea||lastHopeShoot))
            {
                Shoot();
                spiderFireRateTimer = spiderFireRate;
                canShoot = false;
            }
            Move();
        }
        if(playerPos != null)
        {
            Invoke("PlayerInSight", 0.2f);
        }
    }

    private void FixedUpdate()
    {
        //PlayerInSight();
    }

    private void Move()
    {
        if (!playerInSight)
        {
            gameObject.GetComponent<AIPath>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            ownAffraidArea.SetActive(false);
            playerInAffraidArea = false;
            //Ajout Gus
            ChangeAnimationState(SPIDERWALK);
            //    
        }
        if (playerInSight)
        {
            gameObject.GetComponent<AIPath>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            ownAffraidArea.SetActive(true);

        }

        if (playerInAffraidArea && playerPos != null)
        {
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
            //Ajout Gus
            ChangeAnimationState(SPIDERWALK);
            //     
        }
        if (!playerInAffraidArea)
        {
            ownRB.velocity = new Vector2(0, 0);
            //Ajout Gus
            //ChangeAnimationState(SPIDERIDLE);
            //  
        }

        ownRBvelocityX = Mathf.Abs(ownRB.velocity.x);
        ownRBvelocityY = Mathf.Abs(ownRB.velocity.y);

        Vector2 lastHope = new Vector2(lastHopeShootvelocityLimiteX, lastHopeShootvelocityLimiteY);
        if (ownRBvelocityX <= lastHope.x && ownRBvelocityY <= lastHope.y)
        {
            lastHopeShoot = true;
        }
        else lastHopeShoot = false;
    }

    private void LookToPlayer()
    {
        // Cette partie du script fut trouvee sur le forum Unity https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html?_ga=2.230719519.1043224240.1601999147-1783980511.1597703941
        Vector3 diff = playerPos.position - gameObject.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    /*private void CheckPlayerPosition()
    {
        Collider2D playerPos = Physics2D.OverlapCircle(transform.position, affraidArea);
        if(playerPos != null)
        {
            if (playerPos.CompareTag("Player"))
            {
                playerInAffraidArea = true;
            }
            else
            {
                playerInAffraidArea = false;
            }
        }
        if (playerInAffraidArea)
        {
            ownRB.AddForce(transform.forward * (-speed), ForceMode2D.Force);
        }
    }*/

    private void Shoot()
    {
        GameObject bullet = Instantiate(spiderBulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-spiderImprecision, spiderImprecision)));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Bullet bu = bullet.GetComponent<Bullet>();
        //Augmentation des dégâts en fonction de la difficulté et des stages
        bu.damage = (int)Mathf.Round((bu.damage + DP.currentStageDamageBonusForZombie) * DP.currentModDamageMultiplier); 
        rb.AddForce(bullet.transform.up * spiderBulletForce, ForceMode2D.Impulse);
        //Ajout Gus
        ChangeAnimationState(SPIDERATTACK);
        FindObjectOfType<AudioManager>().Play("Spider Shot");
        //
    }

    private void FireRateTimer()
    {
        if (!canShoot)
        {
            spiderFireRateTimer -= Time.deltaTime;
            if (spiderFireRateTimer < 0.0f)
            {
                canShoot = true;
            }
        }
    }

    private void PlayerInSight()
    {
        RaycastHit2D sight = Physics2D.Linecast(transform.position, playerPos.position, whatIsEnvironement);
        /*if (sight.collider.CompareTag("Player"))
        {
            print("see player");
            playerInSight = true;
        }
        else if (!sight.collider.CompareTag("Player"))
        {
            playerInSight = false; print("no see player");
        }*/
        playerInSight = true;
        if(sight.collider != null)
        {
            if (sight.collider.CompareTag("Environement"))
            {
                playerInSight = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(playerPos != null)
        {
            Gizmos.DrawLine(transform.position, playerPos.position);
            //Gizmos.DrawWireSphere(transform.position, affraidArea);
        }
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
