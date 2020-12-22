using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageOne : MonoBehaviour
{
    [Header("Acide Spray")]
    [SerializeField] GameObject acideWarning;
    [SerializeField] GameObject acideSpray;
    [SerializeField] float sprayRate;
    float sprayRateTimer;
    [SerializeField] float warningDelay;
    bool canSpray;

    [Header("Acide Shoot")]
    [SerializeField] GameObject acideShoot;
    [SerializeField] float shootRate;
    float shootRateTimer;
    bool canShoot;

    void Start()
    {
        acideWarning.SetActive(false);
    }

    void Update()
    {
        AcideSprayTimer();
        if (canSpray)
        {
            StartCoroutine(AcideSprayShoot());
        }
    }

    void AcideSprayTimer()
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

    IEnumerator AcideSprayShoot()
    {
        sprayRateTimer = sprayRate;
        canSpray = false;
        acideWarning.SetActive(true);
        yield return new WaitForSeconds(warningDelay);
        acideWarning.SetActive(false); 
        GameObject acideSprayLaunched = Instantiate(acideSpray, gameObject.transform.localPosition, gameObject.transform.rotation);
    }
}
