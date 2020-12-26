using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageThree : MonoBehaviour
{
    [SerializeField] GameObject eggPrefab;
    [SerializeField] GameObject[] launchEggPoints;
    [SerializeField] float eggLaunchVelocity;
    [SerializeField] float launchEggRate;
    float launchEggRateTimer;
    bool canLaunchEgg;

    void Start()
    {
        launchEggRateTimer = launchEggRate;
    }

    void Update()
    {
        if (canLaunchEgg)
        {
            for(int i = 0; i < launchEggPoints.Length; i++)
            {
                GameObject bullet = Instantiate(eggPrefab, launchEggPoints[i].transform.position, launchEggPoints[i].transform.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bullet.transform.up * eggLaunchVelocity, ForceMode2D.Impulse);
            }
        }
    }

    void LaunchEggTimer()
    {
        if (!canLaunchEgg)
        {
            launchEggRateTimer -= Time.deltaTime;
            if (launchEggRateTimer <= 0)
            {
                canLaunchEgg = true;
            }
        }
    }
}
