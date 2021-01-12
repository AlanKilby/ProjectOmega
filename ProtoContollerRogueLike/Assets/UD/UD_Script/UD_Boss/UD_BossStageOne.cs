using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageOne : MonoBehaviour
{
    public CameraSystem thisRoom;

    [Header("Acide Spray")]
    [SerializeField] GameObject acideWarning;
    [SerializeField] GameObject acideSpray;
    //[SerializeField] float sprayRate;
    float sprayRateTimer;
    [SerializeField] float warningDelay;
    bool canSpray;

    [Header("Acide Shoot")]
    [SerializeField] UD_AcideShootBoss acideShootObject;
    //[SerializeField] float shootRate;
    float shootRateTimer;
    [SerializeField] float shootAcideDelaySet;
    bool canShoot;

    void Start()
    {
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        //shootRateTimer = shootRate;
        canShoot = false;
        //sprayRateTimer = sprayRate;
        canSpray = false;
        acideWarning.SetActive(false);
    }

    void Update()
    {
        /* (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            AcideSprayTimer();
            AcideShootTimer();
            if (canSpray)
            {
                StartCoroutine(AcideSprayShoot());
            }
            if (canShoot)
            {
                acideShootObject.isOn = true;
                acideShootObject.shootAcideDelay = shootAcideDelaySet;
                shootRateTimer = shootRate;
                canShoot = false;
            }
        }*/
    }

    public IEnumerator AcideSprayShoot()
    {
        //sprayRateTimer = sprayRate;
        canSpray = false;
        acideWarning.SetActive(true);
        yield return new WaitForSeconds(warningDelay);
        acideWarning.SetActive(false);
        GameObject acideSprayLaunched = Instantiate(acideSpray, gameObject.transform.position, gameObject.transform.rotation);
        acideSprayLaunched.transform.SetParent(thisRoom.transform);
    }

    public void StartAcideShoot()
    {
        acideShootObject.isOn = true;
        acideShootObject.shootAcideDelay = shootAcideDelaySet;
        //shootRateTimer = shootRate;
    }

    /*void AcideSprayTimer()
    {
        if (!canSpray)
        {
            sprayRateTimer -= Time.deltaTime;
            if(sprayRateTimer <= 0)
            {
                canSpray = true;
            }
        }
    }

    void AcideShootTimer()
    {
        if (!canShoot)
        {
            shootRateTimer -= Time.deltaTime;
            if (shootRateTimer <= 0)
            {
                canShoot = true;
            }
        }
    }*/
    
}
