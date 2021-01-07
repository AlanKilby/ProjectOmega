using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_TentacleBoss : MonoBehaviour
{
    public CameraSystem thisRoom;

    [SerializeField] LayerMask whatIsPlayer;

    Animator anim;
    UD_BossBase boss;
    Transform playerPos;

    /*[HideInInspector]*/ public float lifeTimer;
    [SerializeField] GameObject hitToPlayerDetector;
    [SerializeField] float hitToPlayerDetectorDelay;
    [SerializeField] float hitToPlayerDetectorDelayBeforeAttack;
    [SerializeField] float ownAttackLaunchDetectionRadius;

    [SerializeField] float attackRate;
    float attackRateTimer;

    [SerializeField] float health;
    [SerializeField] int damageToBossOnDeath;

    bool playerIsInAttackArea;
    bool canAttack;
    bool tentacleUp;
    bool canTurn;

    //Ajout Gus
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    //

    void Start()
    {
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<UD_BossBase>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        tentacleUp = true;
        canTurn = true;
        hitToPlayerDetector.SetActive(false);
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            if (!boss.isAlive || boss == null)
            {
                Destroy(gameObject);
            }
            if (canAttack && playerIsInAttackArea)
            {
                StartCoroutine(TentacleAttackShoot());
            }
            AttackReloadTimer();
            LifeTimer();
            if (canTurn)
            {
                PlayerDirection();
            }
            UpdateAnims();
        }
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }
    IEnumerator TentacleAttackShoot()
    {
        canTurn = false;
        yield return new WaitForSeconds(hitToPlayerDetectorDelayBeforeAttack);
        tentacleUp = false;
        attackRateTimer = attackRate;
        canAttack = false;
        hitToPlayerDetector.SetActive(true);
        //Moment ou la tentacule claque à terre
        yield return new WaitForSeconds(hitToPlayerDetectorDelay);
        canTurn = true;
        tentacleUp = true;
        hitToPlayerDetector.SetActive(false);
    }

    private void CheckSurroundings()
    {
        playerIsInAttackArea = Physics2D.OverlapCircle(gameObject.transform.position, ownAttackLaunchDetectionRadius, whatIsPlayer);
    }

    void AttackReloadTimer()
    {
        if (!canAttack)
        {
            attackRateTimer -= Time.deltaTime;
            if (attackRateTimer <= 0)
            {
                canAttack = true;
            }
        }
    }

    void LifeTimer()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    void PlayerDirection()
    {
        Vector3 diff = playerPos.position - gameObject.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public void TakeDamage(int damage)
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            health -= damage;

            if (health <= 0)
            {
                Death();
            }
            /*//Ajout Gus
            sr.material = matWhite;
            if (health <= 0)
            {
                Death();
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
            //*/ //GUS DOIT Y VOIR
        }
    }

    public void Death()
    {
        boss.TakeDamage(damageToBossOnDeath);
        Destroy(gameObject);
    }

    void UpdateAnims()
    {
        anim.SetBool("tentacleUp", tentacleUp);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, ownAttackLaunchDetectionRadius);
    }

    //Ajout Gus
    void ResetMaterial()
    {
        sr.material = matDefault;
    }
    //
}
