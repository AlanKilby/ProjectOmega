using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public LayerMask whatIsEnvironement;

    public float speed;
    public float affraidArea;
    public float spiderFireRate;
    private float spiderFireRateTimer;
    public float spiderImprecision;
    public float spiderBulletForce;

    private bool canShoot;
    public bool playerInAffraidArea;
    [SerializeField] private bool playerInSight;

    public Transform firePoint;
    public Transform playerPos;
    public RoomTriggerCollider thisRoom;

    public GameObject spiderBulletPrefab;
    public GameObject ownAffraidArea;
    [SerializeField] private Rigidbody2D ownRB;

    private void Start()
    {
        //thisRoom = gameObject.GetComponentInParent<RoomTriggerCollider>();  ///////A REMETTRE PLUS TARD
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //ownRB = GetComponent<Rigidbody2D>(); //Créer bug de pathfiding
    }
    private void Update()
    {
        if (thisRoom.playerIsInTheRoom == true)
        {
            if (playerInSight)
            {
                LookToPlayer();
            }
            //CheckPlayerPosition();
            FireRateTimer();
            if(playerInSight && canShoot)
            {
                Shoot();
                spiderFireRateTimer = spiderFireRate;
                canShoot = false;
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
        else
        {
            gameObject.GetComponent<AIPath>().enabled = false;
            ownAffraidArea.SetActive(true);
        }

        /*if (playerInAffraidArea)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        if (!playerInAffraidArea)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }*/
    }

    private void LookToPlayer()
    {
        // Cette partie du script fut trouvee sur le forum Unity https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html?_ga=2.230719519.1043224240.1601999147-1783980511.1597703941
        Vector3 diff = playerPos.position - gameObject.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void CheckPlayerPosition()
    {
        Collider2D playerPos = Physics2D.OverlapCircle(transform.position, affraidArea);
        if(playerPos != null)
        {
            if (playerPos.CompareTag("Player"))
            {
                playerInAffraidArea = true;
            }
            else
            {
                playerInAffraidArea = false;
            }
        }
        if (playerInAffraidArea)
        {
            ownRB.AddForce(transform.forward * (-speed), ForceMode2D.Force);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(spiderBulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-spiderImprecision, spiderImprecision)));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.up * spiderBulletForce, ForceMode2D.Impulse);
    }

    private void FireRateTimer()
    {
        if (!canShoot)
        {
            spiderFireRateTimer -= Time.deltaTime;
            if (spiderFireRateTimer < 0.0f)
            {
                canShoot = true;
            }
        }
    }

    private void PlayerInSight()
    {
        RaycastHit2D sight = Physics2D.Linecast(transform.position, playerPos.position, whatIsEnvironement);
        /*if (sight.collider.CompareTag("Player"))
        {
            print("see player");
            playerInSight = true;
        }
        else if (!sight.collider.CompareTag("Player"))
        {
            playerInSight = false; print("no see player");
        }*/
        playerInSight = true;
        if (sight.collider.CompareTag("Environement") && sight != null)
        {
            playerInSight = false;
        }
        /*if (!sight.collider.CompareTag("Environement"))
        {
            playerInSight = true;
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, playerPos.position);
        Gizmos.DrawWireSphere(transform.position, affraidArea);
    }

}
