using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerModuleStation PMS;

    public LayerMask whatIsSolid;
    public LayerMask whatIsEnnemi;

    public float bulletLifeTime;
    float lifeTimer;
    public float distance;
    public float moduleExplosiveChargeRadius;
    /*public float poussée;
    public float knockTime;*/

    public int damage;
    public int moduleExplosiveChargeDamage;

    bool coroutineRunning = false;
    public bool isEnnemyBullet;

    private void Start()
    {
        PMS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModuleStation>();
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
