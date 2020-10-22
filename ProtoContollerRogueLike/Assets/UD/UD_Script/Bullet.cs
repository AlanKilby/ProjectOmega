using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask whatIsSolid;

    public float bulletLifeTime;
    float lifeTimer;
    public float distance;
    /*public float poussée;
    public float knockTime;*/

    public int damage;

    bool coroutineRunning = false;

    private void Start()
    {
        lifeTimer = 0.0f;
    }

    private void Update()
    {
        if (!coroutineRunning)
        {
            EnnemiDetector();
        }
        lifeTimer += Time.deltaTime;
        if (lifeTimer > bulletLifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void EnnemiDetector()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {

            Rigidbody2D enemy = hitInfo.collider.GetComponent<Rigidbody2D>();
            if (hitInfo.collider.CompareTag("Ennemi"))
            {
                hitInfo.collider.GetComponent<EnnemisScript>().TakeDamage(damage);
                /*enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * poussée;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));*/
                Destroy(gameObject);
             }

            if (hitInfo.collider.CompareTag("Environement"))
            {
                Destroy(gameObject);
            }
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
