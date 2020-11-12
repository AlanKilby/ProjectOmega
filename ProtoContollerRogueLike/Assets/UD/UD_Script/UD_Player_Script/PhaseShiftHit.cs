using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseShiftHit : MonoBehaviour
{
    public LayerMask whatIsEnnemi;
    PlayerMouvement PM;

    [SerializeField] float phaseShiftDetectionRadius;
    [HideInInspector] public int damageDealed;

    void Start()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
    }

    /*private void FixedUpdate()
    {
        if(PM.isDashing && PM.hasModulePhaseShift)
        {
            Collider2D hit = Physics2D.OverlapCircle(gameObject.transform.position, phaseShiftDetectionRadius, whatIsEnnemi);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemi"))
        {
            if (PM.isDashing && PM.hasModulePhaseShift)
            {
                collision.GetComponent<EnnemisScript>().TakeDamage(damageDealed);
            }
        }
        
    }


}
