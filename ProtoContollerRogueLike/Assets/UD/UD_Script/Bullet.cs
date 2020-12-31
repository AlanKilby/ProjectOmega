using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerModuleStation PMS;
    PlayerHealth PH;
    Rigidbody2D rb;
    Animator anim;

    GameObject player;

    public LayerMask whatIsSolid;
    public LayerMask whatIsEnnemi;

    public float bulletLifeTime;
    float lifeTimer;
    public float distance;
    public float counterBladeBulletSpeed;
    /*public float poussée;
    public float knockTime;*/

    public int damage;

    bool coroutineRunning = false;
    public bool isEnnemyBullet;
    public bool canStun;

    bool touchObstacle;

    bool hasRandomRotationOnHit;

    [Header("Explosive Ammo Module")]
    public float moduleExplosiveChargeRadius;
    public int moduleExplosiveChargeDamage;
    bool isExplosiveAmmo;


    [Header("Piercing Ammo Module")]
    public int maxEnemyPenetrate;
    int currentEnemyPenetrate;

    [Header("Invicibility Delay For NMI bullet")]
    [SerializeField] float invicibilityDelaySet;
    float invicibilityDelayTimer;
    bool isInvicible;

    private void Start()
    {
        anim = GetComponent<Animator>();
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        PMS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModuleStation>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        lifeTimer = 0.0f;
        touchObstacle = false;
        if (isEnnemyBullet)
        {
            invicibilityDelayTimer = invicibilityDelaySet;
        }
        isInvicible = true;
        currentEnemyPenetrate = maxEnemyPenetrate;
        if (PMS.moduleExplosiveCharge)
        {
            isExplosiveAmmo = true;
        }
        else
        {
            isExplosiveAmmo = false;
        }
    }

    private void Update()
    {
        if (!coroutineRunning)
        {
            //EnnemiDetector();
        }
        lifeTimer += Time.deltaTime;
        if (lifeTimer > bulletLifeTime && !touchObstacle)
        {
            Destroy(gameObject);
        }
        if (touchObstacle)
        {
            if (!hasRandomRotationOnHit)
            {
                float randomZRotation = Random.Range(0, 360);
                gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomZRotation);
                hasRandomRotationOnHit = true;
            }
            rb.velocity = new Vector3(0, 0, 0);
        }
        UpdateAnims();
        if (PH.currentPlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
        InvicibilityTimer();
        if(currentEnemyPenetrate <= 0)
        {
            touchObstacle = true;
        }
    }

    void UpdateAnims()
    {
        anim.SetBool("touchObstacle", touchObstacle);
        anim.SetBool("isExplosiveAmmo", isExplosiveAmmo);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, moduleExplosiveChargeRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
        if (collision.CompareTag("Ennemi") && !isEnnemyBullet)
        {
            collision.GetComponent<EnnemisScript>().TakeDamage(damage);
            /*enemy.isKinematic = false;
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * poussée;
            enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockCo(enemy));*/
            if (canStun)
            {
                collision.GetComponent<EnnemisScript>().Stun();
            }
            ApplyExplosiveModuleEffect();
            if (!PMS.soulScream)
            {
                touchObstacle = true;
            }
            else
            {
                currentEnemyPenetrate--;
            }
        }

        if (collision.CompareTag("Boss") && !isEnnemyBullet)
        {
            collision.GetComponent<UD_BossBase>().TakeDamage(damage);
            ApplyExplosiveModuleEffect();
            if (!PMS.soulScream)
            {
                touchObstacle = true;
            }
            else
            {
                currentEnemyPenetrate--;
            }
        }

        if (collision.CompareTag("Tentacle") && !isEnnemyBullet)
        {
            UD_TentacleBoss tentacleScript = collision.GetComponent<UD_TentacleBoss>();
            if (tentacleScript != null)
            {
                tentacleScript.TakeDamage(damage);
            }
            ApplyExplosiveModuleEffect();
            if (!PMS.soulScream)
            {
                touchObstacle = true;
            }
            else
            {
                currentEnemyPenetrate--;
            }
        }

        if (collision.CompareTag("BossEgg") && !isEnnemyBullet)
        {
            collision.GetComponent<UD_EggBoss>().Spawn();
            ApplyExplosiveModuleEffect();
            if (!PMS.soulScream)
            {
                touchObstacle = true;
            }
            else
            {
                currentEnemyPenetrate--;
            }
        }

        if (collision.CompareTag("Player") && isEnnemyBullet)
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            /*enemy.isKinematic = false;
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * poussée;
            enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockCo(enemy));*/
            touchObstacle = true;
        }

        if (collision.CompareTag("CounterBladeSlashArea") && isEnnemyBullet)
        {
            gameObject.layer = 18; //CounterBladeSlashArea Layer Number
            //Quaternion rot180degrees = Quaternion.Euler(-transform.rotation.eulerAngles); POUR RENVOYER A L'ENVOYEUR
            //rb.velocity = rb.velocity * -1; POUR ALLER VERS L'ENVOYEUR
            /*gameObject.transform.rotation = player.transform.rotation;
            Vector3 playerLook = new Vector3(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z);
            playerLook = playerLook.normalized;*/
            //rb.velocity = playerLook * counterBladeBulletSpeed;
            /*rb.velocity = new Vector2(0, 0);
            rb.AddForce(gameObject.transform.forward * rb.velocity, ForceMode2D.Impulse);*/
            lifeTimer = 0.0f;
            isEnnemyBullet = false;

            GameObject bullet = Instantiate(gameObject, gameObject.transform.position, player.transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Bullet Bu = bullet.GetComponent<Bullet>();
            rb.AddForce(bullet.transform.up * counterBladeBulletSpeed, ForceMode2D.Impulse);
            Bu.isEnnemyBullet = false;
            touchObstacle = true;
        }

        if (collision.CompareTag("Environement") && !isInvicible)
        {
            ApplyExplosiveModuleEffect();
            touchObstacle = true;
        }
    }

    void InvicibilityTimer()
    {
        if (isInvicible)
        {
            invicibilityDelayTimer -= Time.deltaTime;
            if (invicibilityDelayTimer <= 0)
            {
                isInvicible = false;
            }
        }
    }

    void ApplyExplosiveModuleEffect()
    {
        if (PMS.moduleExplosiveCharge)
        {
            Collider2D explosiveHit = Physics2D.OverlapCircle(gameObject.transform.position, moduleExplosiveChargeRadius, whatIsEnnemi);
            if (explosiveHit != null)
            {
                if (explosiveHit.CompareTag("Ennemi"))
                {
                    Debug.Log("DAMAGE");
                    explosiveHit.GetComponent<EnnemisScript>().TakeDamage(moduleExplosiveChargeDamage);
                }
                if (explosiveHit.CompareTag("Boss"))
                {
                    Debug.Log("DAMAGE");
                    explosiveHit.GetComponent<UD_BossBase>().TakeDamage(moduleExplosiveChargeDamage);
                }
                if (explosiveHit.CompareTag("Tentacle"))
                {
                    Debug.Log("DAMAGE");
                    explosiveHit.GetComponent<UD_TentacleBoss>().TakeDamage(moduleExplosiveChargeDamage);
                }
                if (explosiveHit.CompareTag("BossEgg"))
                {
                    Debug.Log("DAMAGE");
                    explosiveHit.GetComponent<UD_EggBoss>().Spawn();
                }
            }
        }
    }

    /*private void EnnemiDetector()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {

            Rigidbody2D enemy = hitInfo.collider.GetComponent<Rigidbody2D>();
            if (hitInfo.collider.CompareTag("Ennemi") && !isEnnemyBullet)
            {
                hitInfo.collider.GetComponent<EnnemisScript>().TakeDamage(damage);
                /*enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * poussée;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));
                if (PMS.moduleExplosiveCharge)
                {
                    Collider2D explosiveHit = Physics2D.OverlapCircle(gameObject.transform.position, moduleExplosiveChargeRadius, whatIsEnnemi);
                    if (explosiveHit != null)
                    {
                        if (explosiveHit.CompareTag("Ennemi"))
                        {
                            Debug.Log("DAMAGE");
                            explosiveHit.GetComponent<EnnemisScript>().TakeDamage(moduleExplosiveChargeDamage);
                        }
                    }
                }
                if (!PMS.soulScream)
                {
                    Destroy(gameObject);
                }
            }
            if (hitInfo.collider.CompareTag("Player") && isEnnemyBullet)
            {
                hitInfo.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
                /*enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * poussée;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));
                Destroy(gameObject);
            }

            if (hitInfo.collider.CompareTag("Environement"))
            {
                Destroy(gameObject);
            }
        }
    }*/

    /*private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if(enemy != null)
        {
            coroutineRunning = true;
            Debug.Log("retour pré-WaitForSecond");
            yield return new WaitForSeconds(knockTime);
            Debug.Log("retour post-WaitForSecond");
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
            coroutineRunning = false;
            Destroy(gameObject);
        }
    }*/
}
