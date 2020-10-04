using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    private bool playerIsMoving;

    public float playerSpeed = 5.0f;
    public float timeSinceAccelerated;
    public float timeSinceDeccelerated;
    public float accelerationTime = 0.5f;
    public float deccelerationTime = 0.5f;

    public float dashSpeed;
    public float dashTime;
    private float dashTimeHolder;
    public bool isDashing = false;

    public Rigidbody2D rb;
    public Camera cam;

    public Vector2 playerInput;
    Vector2 mousePos;

    public AnimationCurve acceleration = AnimationCurve.EaseInOut(0, 0, 0.75f, 2);
    public AnimationCurve decceleration = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private void Start()
    {
        dashTimeHolder = dashTime;
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Mouvement();
        Aim();
        DashAttempt();
        Dash();
    }

    void Mouvement()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            playerIsMoving = true;
        }
        else playerIsMoving = false;

        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!playerIsMoving)
        {
            timeSinceAccelerated = 0;
            timeSinceDeccelerated += Time.deltaTime;
        }
        else if (playerIsMoving)
        {
            timeSinceAccelerated += Time.deltaTime;
            timeSinceDeccelerated = 0;
        }

        float accelerationMultiplier = 1;
        if (accelerationTime > 0)
            accelerationMultiplier = acceleration.Evaluate(timeSinceAccelerated / accelerationTime);

        float deccelerationMultiplier = 1;
        if (deccelerationTime > 0)
            deccelerationMultiplier = decceleration.Evaluate(timeSinceDeccelerated / deccelerationTime);

        if (playerIsMoving)
        {
            rb.velocity = playerInput.normalized * playerSpeed * accelerationMultiplier;
        }

        if (!playerIsMoving)
        {
            rb.velocity = new Vector2(rb.velocity.x * deccelerationMultiplier, rb.velocity.y * deccelerationMultiplier);
        }

    }

    void Aim()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90.0f;
        rb.rotation = angle;
    }

    void DashAttempt()
    {
        if (Input.GetKeyDown("space"))
        {
            isDashing = true;
        }
    }
    void Dash()
    {
        if (isDashing == true && dashTime > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * dashSpeed, rb.velocity.y * dashSpeed);
            dashTime -= Time.deltaTime;
        }


        if (dashTime <= 0)
        {
            isDashing = false;
            dashTime = dashTimeHolder;
        }
    }
}
