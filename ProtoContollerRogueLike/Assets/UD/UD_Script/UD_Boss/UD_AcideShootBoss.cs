using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_AcideShootBoss : MonoBehaviour
{
    [HideInInspector] public float shootAcideDelay;

    [SerializeField] Transform shootPoint1;
    [SerializeField] Transform shootPoint2;
    [SerializeField] Transform shootPoint3;

    [SerializeField] GameObject acideBullet;
    [SerializeField] float shootRateMin;
    [SerializeField] float shootRateMax;
    float shootRateTimer1;
    float shootRateTimer2;
    float shootRateTimer3;
    [SerializeField] float bulletSpeed;
    [SerializeField] float shootImprecision;

    [HideInInspector] public bool isOn;
    bool canShoot1;
    bool canShoot2;
    bool canShoot3;

    void Start()
    {
        isOn = true;
    }

    void Update()
    {
        if (isOn)
        {
            if (canShoot1)
            {
                GameObject acideShoot1 = Instantiate(acideBullet, shootPoint1.position, gameObject.transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-shootImprecision, shootImprecision)));
                Rigidbody2D rb = acideShoot1.GetComponent<Rigidbody2D>();
                rb.AddForce(acideShoot1.transform.up * bulletSpeed, ForceMode2D.Impulse);
                float f = Random.Range(shootRateMin, shootRateMax);
                shootRateTimer1 = f;
                canShoot1 = false;
            }
            if (canShoot2)
            {
                GameObject acideShoot2 = Instantiate(acideBullet, shootPoint2.position, gameObject.transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-shootImprecision, shootImprecision)));
                Rigidbody2D rb = acideShoot2.GetComponent<Rigidbody2D>();
                rb.AddForce(acideShoot2.transform.up * bulletSpeed, ForceMode2D.Impulse);
                float f = Random.Range(shootRateMin, shootRateMax);
                shootRateTimer2 = f;
                canShoot2 = false;
            }
            if (canShoot3)
            {
                GameObject acideShoot3 = Instantiate(acideBullet, shootPoint3.position, gameObject.transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-shootImprecision, shootImprecision)));
                Rigidbody2D rb = acideShoot3.GetComponent<Rigidbody2D>();
                rb.AddForce(acideShoot3.transform.up * bulletSpeed, ForceMode2D.Impulse);
                float f = Random.Range(shootRateMin, shootRateMax);
                shootRateTimer3 = f;
                canShoot3 = false;
            }
        }
        ShootTimer();
    }

    void ShootTimer()
    {
        shootAcideDelay -= Time.deltaTime;
        if (shootAcideDelay<=0.0f)
        {
            isOn = false;
        }
        if (!canShoot1)
        {
            shootRateTimer1 -= Time.deltaTime;
            if (shootRateTimer1 <= 0.0f)
            {
                canShoot1 = true;
            }
        }
        if (!canShoot2)
        {
            shootRateTimer2 -= Time.deltaTime;
            if (shootRateTimer2 <= 0.0f)
            {
                canShoot2 = true;
            }
        }
        if (!canShoot3)
        {
            shootRateTimer3 -= Time.deltaTime;
            if (shootRateTimer3 <= 0.0f)
            {
                canShoot3 = true;
            }
        }
    }
}
