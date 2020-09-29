using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private SwordAttack SA;

    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;
    public Transform firePoint5;
    public Transform swordHitBoxPos;
    public GameObject gatlingBulletPrefab;
    public GameObject shootgunBulletPrefab;
    public GameObject pistolBulletPrefab;

    public float bulletForce = 20.0f;
    public float shootgunBulletForce = 15.0f;
    public float gatlingFireRate = 0.1f;
    public float shootgunFireRate = 0.1f;
    public float swordFireRate = 0.1f;
    public float pistolFireRate = 0.1f;
    public float currentGatlingFireRate;
    public float currentShootgunFireRate;
    public float currentPistolFireRate;
    float fireRateTimer;
    float pistolFireRateTimer;
    float swordFireRateTimer;
    public float gatlingImprecision;
    public float shootgunImprecision;
    public float pistolImprecision;
    public float swordAttackRadius;
    public float gatlingMaxAmmo;
    public float gatlingCurrentAmmo;
    public float shootgunMaxAmmo;
    public float shootgunCurrentAmmo;

    private bool canShoot;
    private bool canSwordShoot;
    private bool pistolCanShoot;
    private bool gatlingEquiped;
    private bool shootgunEquiped;
    private bool pistolEquiped;

    private bool gatlingOnFire;
    private bool shootgunOnFire;
    private bool swordOnFire;

    private void Start()
    {
        gatlingEquiped = true;
        shootgunEquiped = false;
        pistolEquiped = false;
        gatlingCurrentAmmo = gatlingMaxAmmo;
        shootgunCurrentAmmo = shootgunMaxAmmo;

        currentGatlingFireRate = gatlingFireRate;
        currentPistolFireRate = pistolFireRate;
        currentShootgunFireRate = shootgunFireRate;
    }

    void Update()
    {
        if (Input.GetButton("Fire2") && canSwordShoot)
        {
            SwordShoot();
            canSwordShoot = false;
            swordFireRateTimer = swordFireRate;
        }

        if (Input.GetButton("Fire1") && canShoot && gatlingEquiped && gatlingCurrentAmmo>0.0f)
        {
            GatlingShoot();
            canShoot = false;
            fireRateTimer = currentGatlingFireRate;
        }

        if (Input.GetButton("Fire1") && canShoot && shootgunEquiped && shootgunCurrentAmmo>0.0f)
        {
            ShootgunShoot();
            canShoot = false;
            fireRateTimer = currentShootgunFireRate;
        }
        
        if (Input.GetButton("Fire1") && pistolCanShoot && pistolEquiped)
        {
            PistolShoot();
            pistolCanShoot = false;
            pistolFireRateTimer = currentPistolFireRate;
        }

        if (!canShoot)
        {
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer < 0.0f)
            {
                canShoot = true;
            }
        }
        
        if (!canSwordShoot)
        {
            swordFireRateTimer -= Time.deltaTime;
            if (swordFireRateTimer < 0.0f)
            {
                canSwordShoot = true;
            }
        }
        
        if (!pistolCanShoot)
        {
            pistolFireRateTimer -= Time.deltaTime;
            if (pistolFireRateTimer < 0.0f)
            {
                pistolCanShoot = true;
            }
        }

        ChangeWeapon();

    }

    void GatlingShoot()
    {
        GameObject bullet = Instantiate(gatlingBulletPrefab, firePoint1.position, firePoint1.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gatlingImprecision, gatlingImprecision)));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);
        gatlingCurrentAmmo--;
    }

    void ShootgunShoot()
    {
        GameObject bullet1 = Instantiate(shootgunBulletPrefab, firePoint1.position, firePoint1.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-shootgunImprecision, shootgunImprecision)));
        Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet1.transform.up * bulletForce, ForceMode2D.Impulse);

        GameObject bullet2 = Instantiate(shootgunBulletPrefab, firePoint2.position, firePoint2.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-shootgunImprecision, shootgunImprecision)));
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(bullet2.transform.up * bulletForce, ForceMode2D.Impulse);

        GameObject bullet3 = Instantiate(shootgunBulletPrefab, firePoint3.position, firePoint3.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-shootgunImprecision, shootgunImprecision)));
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
        rb3.AddForce(bullet3.transform.up * bulletForce, ForceMode2D.Impulse);

        GameObject bullet4 = Instantiate(shootgunBulletPrefab, firePoint4.position, firePoint4.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-shootgunImprecision, shootgunImprecision)));
        Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();
        rb4.AddForce(bullet4.transform.up * bulletForce, ForceMode2D.Impulse);

        GameObject bullet5 = Instantiate(shootgunBulletPrefab, firePoint5.position, firePoint5.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-shootgunImprecision, shootgunImprecision)));
        Rigidbody2D rb5 = bullet5.GetComponent<Rigidbody2D>();
        rb5.AddForce(bullet5.transform.up * bulletForce, ForceMode2D.Impulse);
        shootgunCurrentAmmo--;
    }

    void PistolShoot()
    {
        GameObject bullet = Instantiate(pistolBulletPrefab, firePoint1.position, firePoint1.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-pistolImprecision, pistolImprecision)));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);
    }

    void SwordShoot()
    {
        if(SA != null)
        {
            SA.gotInput = true;
            //SA.CheckAttackHitBox();
            //A changer quand on aura les anims
        }
    }

    void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q) && gatlingOnFire == true)
        {
            ChangeToShootgun();
        }

        if (Input.GetKeyDown(KeyCode.Q) && shootgunOnFire == true)
        {
            ChangeToSword();
        }
        
        if (Input.GetKeyDown(KeyCode.Q) && swordOnFire == true)
        {
            ChangeToGatling();
        }

        if (gatlingEquiped)
        {
            gatlingOnFire = true;
            shootgunOnFire = false;
            swordOnFire = false;
        }

        if (shootgunEquiped)
        {
            gatlingOnFire = false;
            shootgunOnFire = true;
            swordOnFire = false;
        }

        if (pistolEquiped)
        {
            gatlingOnFire = false;
            shootgunOnFire = false;
            swordOnFire = true;
        }
    }

    void ChangeToGatling()
    {
        gatlingEquiped = true;
        shootgunEquiped = false;
        pistolEquiped = false;
    }

    void ChangeToShootgun()
    {
        gatlingEquiped = false;
        shootgunEquiped = true;
        pistolEquiped = false;
    }
    
    void ChangeToSword()
    {
        gatlingEquiped = false;
        shootgunEquiped = false;
        pistolEquiped = true;
    }

    public void UpgradeRateOfFire(float fireRateMultiplier)
    {
        currentPistolFireRate = currentPistolFireRate * fireRateMultiplier;
        currentGatlingFireRate = currentGatlingFireRate * fireRateMultiplier;
        currentShootgunFireRate = currentShootgunFireRate * fireRateMultiplier;
    }
}

