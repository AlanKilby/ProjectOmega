﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnnemiSummoner : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public LayerMask whatIsEnvironement;

    public float fleeSpeed;
    public float summonRate;
    private float summonRateTimer;
    [SerializeField] private float ownRBvelocityX;
    [SerializeField] private float ownRBvelocityY;
    [SerializeField] [Range(0.0f, 1.0f)] private float lastHopeShootvelocityLimiteX;
    [SerializeField] [Range(0.0f, 1.0f)] private float lastHopeShootvelocityLimiteY;

    private bool canSummon;
    private bool isSummoning;
    public bool playerInAffraidArea;
    public bool lastHopeSummon;
    public bool playerInSight;

    public Transform playerPos;
    public RoomTriggerCollider thisRoom;

    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject minionsPrefab;
    public GameObject ownAffraidArea;
    [SerializeField] private Rigidbody2D ownRB;


    void Start()
    {
        playerInAffraidArea = false;
        thisRoom = gameObject.GetComponentInParent<RoomTriggerCollider>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ownRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom == true)
        {
            if (playerInSight)
            {
                LookToPlayer();
            }
            FireRateTimer();
            if (playerInSight && canSummon && (!playerInAffraidArea || lastHopeSummon))
            {
                Summon();
                summonRateTimer = summonRate;
                canSummon = false;
            }
            Move();
        }
    }

    private void FixedUpdate()
    {
        PlayerInSight();
    }

    private void Move()
    {
        if (!playerInSight)
        {
            gameObject.GetComponent<AIPath>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            ownAffraidArea.SetActive(false);
            playerInAffraidArea = false;
        }
        if (playerInSight && !isSummoning)
        {
            gameObject.GetComponent<AIPath>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            ownAffraidArea.SetActive(true);
        }
        if (playerInSight && isSummoning)
        {
            gameObject.GetComponent<AIPath>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            ownAffraidArea.SetActive(false);
            playerInAffraidArea = false;
        }

        if (playerInAffraidArea)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Vector3 fleeDirection = playerPos.position - gameObject.transform.position;
            fleeDirection.Normalize();
            ownRB.AddForce(fleeDirection * (-fleeSpeed));
        }
        if (!playerInAffraidArea)
        {
            ownRB.velocity = new Vector2(0, 0);
        }

        ownRBvelocityX = Mathf.Abs(ownRB.velocity.x);
        ownRBvelocityY = Mathf.Abs(ownRB.velocity.y);

        Vector2 lastHope = new Vector2(lastHopeShootvelocityLimiteX, lastHopeShootvelocityLimiteY);
        if (ownRBvelocityX <= lastHope.x && ownRBvelocityY <= lastHope.y)
        {
            lastHopeSummon = true;
        }
        else lastHopeSummon = false;
    }

    private void LookToPlayer()
    {
        // Cette partie du script fut trouvee sur le forum Unity https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html?_ga=2.230719519.1043224240.1601999147-1783980511.1597703941
        Vector3 diff = playerPos.position - gameObject.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void Summon()
    {
        spawnPoint1.GetComponent<EnnemiSummonerSpawner>().SpawnEnemy();
        spawnPoint2.GetComponent<EnnemiSummonerSpawner>().SpawnEnemy();
        spawnPoint3.GetComponent<EnnemiSummonerSpawner>().SpawnEnemy();
    }

    private void FireRateTimer()
    {
        if (!canSummon)
        {
            summonRateTimer -= Time.deltaTime;
            isSummoning = true;
            if (summonRateTimer < 0.0f)
            {
                isSummoning = false;
                canSummon = true;
            }
        }
    }

    private void PlayerInSight()
    {
        RaycastHit2D sight = Physics2D.Linecast(transform.position, playerPos.position, whatIsEnvironement);
        playerInSight = true;
        if (sight.collider.CompareTag("Environement") && sight != null)
        {
            playerInSight = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, playerPos.position);
    }
}