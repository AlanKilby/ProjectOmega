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

    CameraSystem isPlayerInRoom;
    public Transform currentRoom;

    void Start()
    {
        launchEggRateTimer = launchEggRate;
        canLaunchEgg = false;
        currentRoom = gameObject.transform.parent;
        isPlayerInRoom = currentRoom.GetComponentInParent<CameraSystem>();
    }

    void Update()
    {
        if (canLaunchEgg)
        {
            for(int i = 0; i < launchEggPoints.Length; i++)
            {
                GameObject egg = Instantiate(eggPrefab, launchEggPoints[i].transform.position, launchEggPoints[i].transform.rotation);
                Rigidbody2D rb = egg.GetComponent<Rigidbody2D>();
                rb.AddForce(egg.transform.up * eggLaunchVelocity, ForceMode2D.Impulse);
                egg.transform.SetParent(isPlayerInRoom.transform);
            }
            launchEggRateTimer = launchEggRate;
            canLaunchEgg = false;
        }
        LaunchEggTimer();
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
