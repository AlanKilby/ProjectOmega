using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public LayerMask whatIsEnvironment;

    SwordAttack SA;

    public bool playerIsMoving;
    public bool hasModulePhaseShift;

    public float playerSpeed = 5.0f;
    //public float timeSinceAccelerated;
    //public float timeSinceDeccelerated;
    //public float accelerationTime = 0.5f;
    //public float deccelerationTime = 0.5f;

    public float dashSpeed;
    public float dashTime;
    private float dashTimeHolder;
    public bool isDashing = false;
    public bool canDash;
    public float dashReloadTimeSet;
    [HideInInspector] public float dashReloadTimeUpgraded;
    public float dashReloadTimer;
    public float dashTimePercent;
    public float playerRBVelovityX;
    public float playerRBVelovityY;

    private bool wallIsNear;
    public float wallDetectionRadius;
    //public Vector3 wallDetectionPosition;
    public GameObject wallDetectionPosition;

    //Ajout Gus
    private string currentAnimation;

    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_WALK = "PlayerWalk";
    const string PLAYER_DASH = "PlayerDash";
    //

    const string PLAYER_DASH_ATTACK = "PlayerDashAttack";

    public Rigidbody2D rb;
    public Camera cam;
    [SerializeField] private Animator anim;

    public Vector2 playerInput;
    Vector2 mousePos;
    [SerializeField] Vector3 lastInput;

    Vector2 dashVelocity;
    
    [Header("Screen Shake")]
    ScreenShakeEffect SSE;
    [SerializeField] float shakePower;
    [SerializeField] float shakeDuration;

    //public AnimationCurve acceleration = AnimationCurve.EaseInOut(0, 0, 0.75f, 2);
    //public AnimationCurve decceleration = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private void Start()
    {
        dashTimeHolder = dashTime;
        anim = GetComponent<Animator>();
        SA = GetComponent<SwordAttack>();
        hasModulePhaseShift = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        SSE = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShakeEffect>();
        dashReloadTimeUpgraded = dashReloadTimeSet;
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //UpdateAnim();

        //Retrouve la caméra pour screenshake si changement de scene
        if (SSE == null)
        {
            SSE = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShakeEffect>();
        }

    }

    /*void UpdateAnim()
    {
        //anim.SetBool("isDashing", isDashing);
    }*/

    private void FixedUpdate()
    {
        Mouvement();
        if (!isDashing)
        {
            Aim();
        }
        else LookLock();
        DashAttempt();
        Dash();
        DashTimer();
        RBVelocityShow();
        InputDetector();
        CheckSurroundings();
    }

    void Mouvement()
    {
        //Ajout Gus
        if (!isDashing)
        //
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                playerIsMoving = true;
                if (!SA.isAttacking && !isDashing)
                {
                    ChangeAnimationState(PLAYER_WALK);
                }
            }
            else
            {
                playerIsMoving = false; 
                if (!SA.isAttacking && !isDashing)
                {
                    ChangeAnimationState(PLAYER_IDLE);
                }
            }
        }


        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //if (!playerIsMoving)
        //{
        //    timeSinceAccelerated = 0;
        //    timeSinceDeccelerated += Time.deltaTime;
        //}
        //else if (playerIsMoving)
        //{
        //    timeSinceAccelerated += Time.deltaTime;
        //    timeSinceDeccelerated = 0;
        //}

        //float accelerationMultiplier = 1;
        //if (accelerationTime > 0)
        //    accelerationMultiplier = acceleration.Evaluate(timeSinceAccelerated / accelerationTime);

        //float deccelerationMultiplier = 1;
        //if (deccelerationTime > 0)
        //    deccelerationMultiplier = decceleration.Evaluate(timeSinceDeccelerated / deccelerationTime);

        if (GameManagement.GameIsPaused)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0, rb.velocity.y * 0);
        }
        else
        {
            if (playerIsMoving)
            {
                rb.velocity = playerInput.normalized * playerSpeed; /** accelerationMultiplier;*/
            }

            if (!playerIsMoving)
            {
                //rb.velocity = new Vector2(rb.velocity.x * deccelerationMultiplier, rb.velocity.y * deccelerationMultiplier);
                rb.velocity = new Vector2(rb.velocity.x * 0, rb.velocity.y * 0);
            }
        }

    }

    void Aim()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90.0f;
        rb.rotation = angle;
    }

    void InputDetector()
    {
        if (!isDashing)
        {
            lastInput = playerInput;
        }
    }

    void Footstep() 
    {
        FindObjectOfType<AudioManager>().Play("Footstep");
    }
        

    void LookLock()
    {
        float angle = Mathf.Atan2(lastInput.y, lastInput.x) * Mathf.Rad2Deg - 90.0f;
        rb.rotation = angle;
    }

    void RBVelocityShow()
    {
        playerRBVelovityX = transform.InverseTransformDirection(rb.velocity).x;
        playerRBVelovityY = transform.InverseTransformDirection(rb.velocity).y;
    }

    void DashAttempt()
    {
        if (Input.GetKey(KeyCode.Space) && canDash && rb.velocity != new Vector2(0, 0) && !GameManagement.GameIsPaused && !wallIsNear)
        {
            SSE.StartShake(shakePower, shakeDuration);
            isDashing = true;
            dashVelocity = new Vector2(rb.velocity.x * dashSpeed, rb.velocity.y * dashSpeed);
            if (!hasModulePhaseShift)
            {
                //Ajout Gus
                FindObjectOfType<AudioManager>().Play("Dash");
                ChangeAnimationState(PLAYER_DASH);
                //
            }
            /*if (hasModulePhaseShift)
            {
                //Ajout Gus
                FindObjectOfType<AudioManager>().Play("Sword");
                FindObjectOfType<AudioManager>().Play("Dash");
                ChangeAnimationState(PLAYER_DASH_ATTACK);
                //
            }*/


        }
    }
    void Dash()
    {
        if (wallIsNear && isDashing)
        {
            print("touch Wall");
            rb.velocity = new Vector3(0, 0, 0);
            dashTime = 0.0f;
        }

        if (dashTime <= 0)
        {
            isDashing = false;
            dashTime = dashTimeHolder;
        }

        if (isDashing == true && dashTime > 0)
        {
            canDash = false;
            dashReloadTimer = 0.0f;
            rb.velocity = dashVelocity;
            //rb.velocity = new Vector2(rb.velocity.x * dashSpeed, rb.velocity.y * dashSpeed);
            dashTime -= Time.deltaTime;
        }       

    }

    void DashTimer()
    {
        if (!canDash)
        {
            dashReloadTimer += Time.deltaTime;
        }
        if (dashReloadTimer > dashReloadTimeUpgraded)
        {
            canDash = true;
        }
        dashTimePercent = dashReloadTimer / dashReloadTimeUpgraded;
    }

    private void CheckSurroundings()
    {
        wallIsNear = Physics2D.OverlapCircle(wallDetectionPosition.transform.position, wallDetectionRadius, whatIsEnvironment);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(wallDetectionPosition.transform.position, wallDetectionRadius);
    }

    //Ajout Gus
    void ChangeAnimationState(string newAnimation)
    {
       
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);

        currentAnimation = newAnimation;

     //


    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Environement"))
        {
            if (isDashing)
            {
                rb.velocity = new Vector3(0, 0, 0);
                isDashing = false;
                dashTime = dashTimeHolder;
            }
        }
    }*/
}
