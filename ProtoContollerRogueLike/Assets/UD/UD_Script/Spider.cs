using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float speed;
    public float affraidArea;
    public float spiderFireRate;
    private float spiderFireRateTimer;
    public float spiderImprecision;
    public float spiderBulletForce;

    private bool canShoot;
    private bool playerInSight;

    public Transform firePoint;
    public Transform playerPos;
    public RoomTriggerCollider thisRoom;

    public GameObject spiderBulletPrefab;
    [SerializeField] private Rigidbody2D ownRB;

    private void Start()
    {
        thisRoom = gameObject.GetComponentInParent<RoomTriggerCollider>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (thisRoom.playerIsInTheRoom == true)
        {
            // Cette partie du script fut trouvee sur le forum Unity https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html?_ga=2.230719519.1043224240.1601999147-1783980511.1597703941
            Vector3 diff = playerPos.position - gameObject.transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

            CheckPlayerPosition();
            FireRateTimer();
            PlayerInSight();
            if(playerInSight && canShoot)
            {
                Shoot();
            }
        }
    }

    private void CheckPlayerPosition()
    {
        Collider2D playerPos = Physics2D.OverlapCircle(transform.position, affraidArea);
        if(playerPos != null)
        {
            if (playerPos.CompareTag("Player"))
            {
                ownRB.AddForce(transform.forward * (-speed), ForceMode2D.Force);
            }
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
        RaycastHit2D sight = Physics2D.Linecast(transform.position, playerPos.position);
        if (sight.collider.CompareTag("Player"))
        {
            playerInSight = true;
        }
        else playerInSight = false;
    }

}
