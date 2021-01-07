using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageFour : MonoBehaviour
{
    public CameraSystem thisRoom;

    [Header("Fire Rate of this Attack")]
    [SerializeField] float attackLaunchRate;
    float attackLaunchRateTimer;
    bool canFire;

    [Header("Fire Point for each Phase of this Attack")]
    [SerializeField] Transform[] firePointPhaseOne;
    [SerializeField] Transform[] firePointPhaseTwo;
    [SerializeField] Transform[] firePointPhaseThree;
    [SerializeField] Transform[] firePointPhaseFour;

    [Header("Stats for all Phase of this Attack")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletPrefabSpeed;
    [SerializeField] int shootPerPointForEachPhase;
    [SerializeField] float timeBetweenShootPerPointEachPhase;
    [SerializeField] float timeBetweenEachPhase;

    void Start()
    {
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        canFire = false;
        attackLaunchRateTimer = attackLaunchRate;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            LaunchEggTimer();
            if (canFire)
            {
                print("launch couroutine");
                StartCoroutine(StageFourAttack());
                attackLaunchRateTimer = attackLaunchRate;
                canFire = false;
            }
        }
    }

    IEnumerator StageFourAttack()
    {
        //Phase One Of the Attack
        for (int i = 0; i < shootPerPointForEachPhase; i++)
        {
            for(int j = 0; j < firePointPhaseOne.Length; j++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointPhaseOne[j].position, firePointPhaseOne[j].rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bullet.transform.up * bulletPrefabSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(timeBetweenShootPerPointEachPhase);
        }
        print("couroutine phase One finish");
        yield return new WaitForSeconds(timeBetweenEachPhase);

        //Phase Two Of the Attack
        for (int i = 0; i < shootPerPointForEachPhase; i++)
        {
            for (int j = 0; j < firePointPhaseTwo.Length; j++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointPhaseTwo[j].position, firePointPhaseTwo[j].rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bullet.transform.up * bulletPrefabSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(timeBetweenShootPerPointEachPhase);
        }
        print("couroutine phase Two finish");
        yield return new WaitForSeconds(timeBetweenEachPhase);

        //Phase Three Of the Attack
        for (int i = 0; i < shootPerPointForEachPhase; i++)
        {
            for (int j = 0; j < firePointPhaseThree.Length; j++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointPhaseThree[j].position, firePointPhaseThree[j].rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bullet.transform.up * bulletPrefabSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(timeBetweenShootPerPointEachPhase);
        }
        print("couroutine phase Three finish");
        yield return new WaitForSeconds(timeBetweenEachPhase);

        //Phase Four Of the Attack
        for (int i = 0; i < shootPerPointForEachPhase; i++)
        {
            for (int j = 0; j < firePointPhaseFour.Length; j++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointPhaseFour[j].position, firePointPhaseFour[j].rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bullet.transform.up * bulletPrefabSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(timeBetweenShootPerPointEachPhase);
        }
        print("couroutine phase Four finish");
    }

    void LaunchEggTimer()
    {
        if (!canFire)
        {
            attackLaunchRateTimer -= Time.deltaTime;
            if (attackLaunchRateTimer <= 0)
            {
                canFire = true;
            }
        }
    }
}
