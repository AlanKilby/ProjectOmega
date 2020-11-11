using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnvironnementInteraction : MonoBehaviour
{
    [SerializeField] LayerMask whatIsAcide;

    PlayerHealth PH;

    private bool isInAcide;
    private bool dealDamageActive;

    [SerializeField] int acideDamage;
    [SerializeField] float acideDetectionRadius;
    [SerializeField] float acideDetectionRadiusXoffset;
    [SerializeField] float acideDetectionRadiusYoffset;
    [SerializeField] float acideDamageRate;
    private float acideDamageRateTimer;

    void Start()
    {
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        AcideDealDamage();
    }

    private void AcideDealDamage()
    {
        if(isInAcide && dealDamageActive)
        {
            PH.TakeDamage(acideDamage);
            dealDamageActive = false;
            acideDamageRateTimer = acideDamageRate;
        }
        if(dealDamageActive == false)
        {
            acideDamageRateTimer -= Time.deltaTime;
            if(acideDamageRateTimer <= 0.0f)
            {
                dealDamageActive = true;
            }
        }
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        Vector3 offset = new Vector3(acideDetectionRadiusXoffset, acideDetectionRadiusYoffset, 0.0f);
        isInAcide = Physics2D.OverlapCircle(gameObject.transform.position + offset, acideDetectionRadius, whatIsAcide);
    }

    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(acideDetectionRadiusXoffset, acideDetectionRadiusYoffset, 0.0f);
        Gizmos.DrawWireSphere(gameObject.transform.position, acideDetectionRadius);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        print("inCollisionEnter");
        if (collision.gameObject.CompareTag("AcidePool"))
        {
            isInAcide = true;
            print("inAcideEnter");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        print("inCollisionStay");
        if (collision.gameObject.CompareTag("AcidePool"))
        {
            isInAcide = true;
            print("inAcideStay");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("AcidePool"))
       {
            isInAcide = false;
       }
    }*/
}
