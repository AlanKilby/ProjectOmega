using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerModuleStation PMS;
    Rigidbody2D rb;

    GameObject player;

    public LayerMask whatIsSolid;
    public LayerMask whatIsEnnemi;

    public float bulletLifeTime;
    float lifeTimer;
    public float distance;
    public float moduleExplosiveChargeRadius;
    public float counterBladeBulletSpeed;
    /*public float poussée;
    public float knockTime;*/

    public int damage;
    public int moduleExplosiveChargeDamage;

    bool coroutineRunning = false;
    public bool isEnnemyBullet;

    private void Start()
    {
        PMS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModuleStation>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        lifeTimer = 0.0f;
    }

    private void Update()
    {
        if (!coroutineRunning)
        {
            //EnnemiDetector();
        }
        lifeTimer += Time.deltaTime;
        if (lifeTimer > bulletLifeTime)
        {
            Destroy(gameObject);
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
        if (collision.CompareTag("Player") && isEnnemyBullet)
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            /*enemy.isKinematic = false;
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * poussée;
            enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockCo(enemy));*/
            Destroy(gameObject);
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
            Destroy(gameObject);
        }

        if (collision.CompareTag("Environement"))
        {
            Destroy(gameObject);
        }
    }

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
