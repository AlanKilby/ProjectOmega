using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnvironnementInteraction : MonoBehaviour
{
    PlayerHealth PH;
    PlayerMouvement PM;
    PostProcessEffect PPEGreen;
    public Collider2D ownCollider2D;

    [Header("Acide Interaction")]
    [SerializeField] LayerMask whatIsAcide;
    private bool isInAcide;
    private bool acideDealDamageActive;

    [SerializeField] int acideDamage;
    [SerializeField] float acideDetectionRadius;
    [SerializeField] float acideDetectionRadiusXoffset;
    [SerializeField] float acideDetectionRadiusYoffset;
    [SerializeField] float acideDamageRate;
    private float acideDamageRateTimer;

    [Header("*OBSOLETE* Ravine Interaction")]
    [SerializeField] LayerMask whatIsRavin;
    private bool ravineDealDamageActive;
    private bool isNearRavine;
    private bool isTouchingRavine;
    [SerializeField] int ravineDamage;
    [SerializeField] float ravineDamageRate;
    [SerializeField] float ravineDetectionRespawnIfDie;
    private float ravineDamageRateTimer;
    Vector3 lastPositionBeforeRavine;

    void Start()
    {
        PH = GetComponent<PlayerHealth>();
        PM = GetComponent<PlayerMouvement>();
        PPEGreen = GameObject.Find("InAcideEffect").GetComponent<PostProcessEffect>();
    }

    void Update()
    {
        AcideDealDamage();
        //RavineCollision(); //N'est plus appelée car la méchanique est abandonnée
        //RavinDetector();
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
        //LastPositionBeforeRavineSave(); //N'est plus appelée car la méchanique est abandonnée
        PostProcessGreenEffect();
    }

    private void LastPositionBeforeRavineSave()
    {
        if (!isNearRavine)
        {
            lastPositionBeforeRavine = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

    private void RavineCollision()
    {
        if(isTouchingRavine && !PM.isDashing && ravineDealDamageActive)
        {
            PH.TakeDamage(ravineDamage);
            FindObjectOfType<AudioManager>().StopPlaying("Damage Hit");
            FindObjectOfType<AudioManager>().Play("Chasm");
            gameObject.transform.position = lastPositionBeforeRavine;
            ravineDealDamageActive = false;
            
        }
        if (ravineDealDamageActive == false)
        {
            ravineDamageRateTimer -= Time.deltaTime;
            if (ravineDamageRateTimer <= 0.0f)
            {
                ravineDealDamageActive = true;
                

            }
        }
    }

    private void AcideDealDamage()
    {
        if (!PM.isDashing)
        {
            if (isInAcide && acideDealDamageActive)
            {
                PH.TakeDamage(acideDamage);
                FindObjectOfType<AudioManager>().StopPlaying("Damage Hit");
                FindObjectOfType<AudioManager>().Play("Acid");

                acideDealDamageActive = false;
                acideDamageRateTimer = acideDamageRate;
            }
            if (acideDealDamageActive == false)
            {
                acideDamageRateTimer -= Time.deltaTime;
                if (acideDamageRateTimer <= 0.0f)
                {
                    acideDealDamageActive = true;
                }
            }
        }
    }

    void PostProcessGreenEffect()
    {
        if (isInAcide)
        {
            PPEGreen.canDisappear = false;
            PPEGreen.canAppear = true;
        }
        else
        {
            PPEGreen.canDisappear = true;
            PPEGreen.canAppear = false;
        }
    }

    private void CheckSurroundings()
    {
        Vector3 offset = new Vector3(acideDetectionRadiusXoffset, acideDetectionRadiusYoffset, 0.0f);
        isInAcide = Physics2D.OverlapCircle(gameObject.transform.position + offset, acideDetectionRadius, whatIsAcide);
        //isNearRavine = Physics2D.OverlapCircle(gameObject.transform.position + offset, ravineDetectionRespawnIfDie, whatIsRavin); //N'est plus appelée car la méchanique est abandonnée
        //isTouchingRavine = Physics2D.OverlapCircle(gameObject.transform.position + offset, acideDetectionRadius, whatIsRavin); //N'est plus appelée car la méchanique est abandonnée
    }

    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(acideDetectionRadiusXoffset, acideDetectionRadiusYoffset, 0.0f);
        Gizmos.DrawWireSphere(gameObject.transform.position, acideDetectionRadius);
        Gizmos.DrawWireSphere(gameObject.transform.position, ravineDetectionRespawnIfDie);
    }

    /*private void RavinDetector()
    {
        RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, acideDetectionRadius, new Vector2 (0,0), whatIsRavin);
        if (hitInfo.collider != null)
        {
            if (PM.isDashing && hitInfo.collider.CompareTag("Ravin"))
            {
                EnableRavinCollision(hitInfo.collider);
                print("enableCollision");
            }
            if (!PM.isDashing)
            {
                DiseableRavinCollision(hitInfo.collider);
                print("diseableCollision");
            }
        }
    }

    private void EnableRavinCollision(Collider2D ravinCollider)
    {
        Physics2D.IgnoreCollision(ravinCollider, ownCollider2D, true);
    }

    private void DiseableRavinCollision(Collider2D ravinCollider)
    {
        Physics2D.IgnoreCollision(ravinCollider, ownCollider2D, false);
    }*/

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PM.isDashing && collision.gameObject.CompareTag("Ravin"))
        {
            EnableRavinCollision(collision.collider);
            print("enableCollision");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (PM.isDashing && collision.gameObject.CompareTag("Ravin"))
        {
            EnableRavinCollision(collision.collider);
            print("enableCollision");
        }
        if (!PM.isDashing)
        {
            DiseableRavinCollision(collision.collider);
            print("diseableCollision");
        }
    }
   private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ravin"))
        {
            DiseableRavinCollision(collision.collider);
            print("diseableCollision");
        }
    }*/

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
