using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
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

    //Ajout Gus
    private string currentAnimation;

    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_WALK = "PlayerWalk";
    const string PLAYER_DASH = "PlayerDash";
    //

    public Rigidbody2D rb;
    public Camera cam;
    [SerializeField] private Animator anim;

    public Vector2 playerInput;
    Vector2 mousePos;
    [SerializeField] Vector3 lastInput;


    //public AnimationCurve acceleration = AnimationCurve.EaseInOut(0, 0, 0.75f, 2);
    //public AnimationCurve decceleration = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private void Start()
    {
        dashTimeHolder = dashTime;
        anim = GetComponent<Animator>();
        hasModulePhaseShift = false;
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //UpdateAnim();

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
                ChangeAnimationState(PLAYER_WALK);
            }
            else
            {
                playerIsMoving = false;
                ChangeAnimationState(PLAYER_IDLE);
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
        if (Input.GetKey(KeyCode.Space) && canDash && rb.velocity != new Vector2(0, 0))
        {
            isDashing = true;
            //Ajout Gus
            ChangeAnimationState(PLAYER_DASH);
            //
            
            
        }
    }
    void Dash()
    {
        if (isDashing == true && dashTime > 0)
        {
            canDash = false;
            dashReloadTimer = 0.0f;
            rb.velocity = new Vector2(rb.velocity.x * dashSpeed, rb.velocity.y * dashSpeed);
            dashTime -= Time.deltaTime;
        }


        if (dashTime <= 0)
        {
            isDashing = false;
            dashTime = dashTimeHolder;
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

    //Ajout Gus
    void ChangeAnimationState(string newAnimation)
    {
       
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);

        currentAnimation = newAnimation;

     //


    }
}
