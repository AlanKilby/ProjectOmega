using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Gun gun;
    public PlayerMouvement PM;
    public PlayerModuleStation PMS;
    UpgradePlatform UP;
    ConsumablePlatform CP;

    //Graphics 
    public SpriteRenderer gunSpriteRenderer;
    GameObject ownIcon;
    RectTransform ownIconRect;
    GameObject ownIconSlot;
    RectTransform ownIconSlotRect;
    [SerializeField] Vector3 iconOffsetWhenInHand;
    Vector3 ownIconSlotDefaultPosition;
    public GameObject ownRarityLight;

    //Technical Parts
    public GameObject[] shootingPoints;
    public int ammoCount;
    public bool isEquipped;
    bool canShoot;
    public float fireRate;
    float fireRateTimer;
    Inventory inventory;
    AudioSource audioSource;
    string gunID;
    public int gunSlot;
    bool gunAlreadyInInv;
    public bool gunInHand; //Ajout Ulric pour Pastille Pistol

    [Header("Loading Gun ?")]
    public GameObject chargedMaxBulletPrefab;
    public bool isALoadingGun;
    bool loadingGunCharging;
    bool loadingGunIsChargedMax;
    public float loadingGunTimeToChargeMax;
    float loadingGunTimer;
    public float loadingGunChargePercentage;
    public int chargedMaxDamageMultiplicator;
    public bool canStunWhenCharingMax;
    public float lifeTimeMultiplicatorWhenCharged;

    [Header("Screen Shake")]
    ScreenShakeEffect SSE;
    [SerializeField] float shakePower;
    [SerializeField] float shakeDuration;

    [Header("Unlimited Ammo ?")]
    public bool isUnlimitedAmmoGun;

    [Header("Type of Gun ?")]
    [SerializeField] private bool isHandgun;
    [SerializeField] private bool isShotgun;
    [SerializeField] private bool isGatling;


    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        PMS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModuleStation>();
        UP = GameObject.FindGameObjectWithTag("Player").GetComponent<UpgradePlatform>();
        CP = GameObject.FindGameObjectWithTag("Player").GetComponent<ConsumablePlatform>();
        SSE = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShakeEffect>();
        audioSource = gameObject.GetComponent<AudioSource>();
        canShoot = true;
        //isEquipped = false;
        gunSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunSpriteRenderer.sprite = gun.weaponSprites[0];
        gunID = gun.ID;
        Debug.Log(gunID);
        gunAlreadyInInv = false;
        loadingGunCharging = false;
        loadingGunTimer = 0.0f;
    }

    void Update()
    {
        //Sprite change
        if (isEquipped)
        {
            gunSpriteRenderer.sprite = gun.weaponSprites[1];
        }

        if(!isEquipped && ammoCount <= 0)
        {
            gunSpriteRenderer.sprite = gun.weaponSprites[2];
        }

        //Icon Offset When In Hand
        if(ownIconSlotRect != null)
        {
            if (gunInHand)
            {
                //ownIconSlotRect.anchoredPosition = ownIconSlotDefaultPosition + iconOffsetWhenInHand;
                ownIconSlotRect.anchoredPosition = new Vector3(ownIconSlotDefaultPosition.x + iconOffsetWhenInHand.x, -162.5f + iconOffsetWhenInHand.y, ownIconSlotDefaultPosition.z + iconOffsetWhenInHand.z);
            }
            else
            {
                //ownIconSlotRect.anchoredPosition = ownIconSlotDefaultPosition;
                ownIconSlotRect.anchoredPosition = new Vector3(ownIconSlotDefaultPosition.x, -162.5f, ownIconSlotDefaultPosition.z);
            }
        }

        //Shooting
        if (isEquipped && Input.GetButton("Fire1") && (inventory.ammoCounter[gun.ammoID] > 0 || CP.hasUnlimitedAmmo) && !PM.isDashing && !isHandgun && !GameManagement.GameIsPaused)
        {
            WeaponShooting();
        }

        if (isHandgun && !isGatling && !isShotgun && !GameManagement.GameIsPaused)
        {
            if (isEquipped && Input.GetButtonDown("Fire1") && inventory.ammoCounter[gun.ammoID] > 0 && !PM.isDashing)
            {
                FindObjectOfType<AudioManager>().Play("Handgun Charge");
                loadingGunCharging = true;
            }

            if (isEquipped && Input.GetButtonUp("Fire1") && inventory.ammoCounter[gun.ammoID] > 0 && !PM.isDashing)
            {
                FindObjectOfType<AudioManager>().StopPlaying("Handgun Charge");
                FindObjectOfType<AudioManager>().Play("Handgun Shot");
                WeaponShooting();
                loadingGunChargePercentage = 0.0f;
                loadingGunCharging = false;
            }

            if (!isEquipped || inventory.ammoCounter[gun.ammoID] <= 0 || PM.isDashing || !gunInHand)
            {
                FindObjectOfType<AudioManager>().StopPlaying("Handgun Charge");
                FindObjectOfType<AudioManager>().StopPlaying("Handgun Shot"); 
                loadingGunCharging = false;
            }

        }

        if (loadingGunCharging)
        {
            LoadingGunCharge();
        }

        //Fire Rate cooldown
        if (!canShoot)
        {
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer <= 0.0f)
            {
                canShoot = true;
                fireRateTimer = fireRate;
            }
        }

        //Ulric : Check If Gun is In Hand
        if(gunSlot == inventory.equippedSlot)
        {
            gunInHand = true;
        }
        else
        {
            gunInHand = false;
        }

        //Retrouve la caméra pour screenshake si changement de scene
        if(SSE == null)
        {
            SSE = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShakeEffect>();
        }

    }

    void WeaponShooting()
    {
        if (canShoot && gunSlot == inventory.equippedSlot)
        {
            for (int i = 0; i < shootingPoints.Length; i++)
            {
                if (!isALoadingGun)
                {
                    SSE.StartShake(shakePower, shakeDuration);
                    GameObject bullet = Instantiate(gun.ammoType, shootingPoints[i].transform.position, shootingPoints[i].transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gun.accuracy, gun.accuracy)));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(bullet.transform.up * gun.bulletVelocity, ForceMode2D.Impulse);

                    if (isGatling && !isHandgun)
                    {
                        FindObjectOfType<AudioManager>().Play("Gatling");
                    }

                    if (isShotgun && !isHandgun)
                    {
                        FindObjectOfType<AudioManager>().Play("Shotgun");
                    }

                }

                if (isALoadingGun && !loadingGunIsChargedMax)
                {
                    SSE.StartShake(shakePower, shakeDuration);
                    GameObject bullet = Instantiate(gun.ammoType, shootingPoints[i].transform.position, shootingPoints[i].transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gun.accuracy, gun.accuracy)));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    Bullet Bu = bullet.GetComponent<Bullet>();
                    //Bu.damage = Bu.damage * loadingGunChargePercentage; MARCHE PAS CAR % en Float et Damage en Int
                    if (loadingGunIsChargedMax)
                    {
                        Bu.damage = Bu.damage * chargedMaxDamageMultiplicator;
                        Bu.bulletLifeTime = Bu.bulletLifeTime * lifeTimeMultiplicatorWhenCharged;
                        if (canStunWhenCharingMax)
                        {
                            Bu.canStun = true;
                        }
                    }
                    rb.AddForce(bullet.transform.up * gun.bulletVelocity, ForceMode2D.Impulse);
                    loadingGunTimer = 0.0f;
                    loadingGunIsChargedMax = false;
                }

                if (isALoadingGun && loadingGunIsChargedMax)
                {
                    SSE.StartShake(shakePower, shakeDuration);
                    GameObject bullet = Instantiate(chargedMaxBulletPrefab, shootingPoints[i].transform.position, shootingPoints[i].transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gun.accuracy, gun.accuracy)));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    Bullet Bu = bullet.GetComponent<Bullet>();
                    //Bu.damage = Bu.damage * loadingGunChargePercentage; MARCHE PAS CAR % en Float et Damage en Int
                    if (loadingGunIsChargedMax)
                    {
                        Bu.damage = Bu.damage * chargedMaxDamageMultiplicator;
                        Bu.bulletLifeTime = Bu.bulletLifeTime * lifeTimeMultiplicatorWhenCharged;
                        if (canStunWhenCharingMax)
                        {
                            Bu.canStun = true;
                        }
                    }
                    rb.AddForce(bullet.transform.up * gun.bulletVelocity, ForceMode2D.Impulse);
                    loadingGunTimer = 0.0f;
                    loadingGunIsChargedMax = false;
                }
            }
            if (PMS.soulScream)
            {
                FindObjectOfType<AudioManager>().Play("SoulScream");
                /*FindObjectOfType<AudioManager>().StopPlaying("Gatling");
                FindObjectOfType<AudioManager>().StopPlaying("Shotgun");
                FindObjectOfType<AudioManager>().StopPlaying("Handgun Shot");
                */
            }

            if(!isUnlimitedAmmoGun && !CP.hasUnlimitedAmmo)
            {
                inventory.ammoCounter[gun.ammoID]--;
            }
            canShoot = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                // If the inventory IS NOT full AND the gun IS NOT already in the inventory pick up the gun
                if(inventory.isFull[i] == false && gunAlreadyInInv == false)
                {
                    if(ownRarityLight != null)
                    {
                        Destroy(ownRarityLight);
                    }

                    inventory.gunGameObject[i] = gameObject;

                    gunSlot = i;

                    isEquipped = true;

                    inventory.gunID[i] = gunID;

                    inventory.isFull[i] = true;

                    ownIconSlot = inventory.slots[i];

                    ownIconSlotDefaultPosition = ownIconSlot.GetComponent<RectTransform>().anchoredPosition;

                    ownIcon = Instantiate(gun.gunIcon, inventory.slots[i].transform, false);

                    ownIconRect = ownIcon.GetComponent<RectTransform>();

                    ownIconSlotRect = ownIconSlot.GetComponent<RectTransform>();


                    transform.SetParent(collision.transform);
                    
                    //transform.localPosition = new Vector2(-0.35f,0.15f);
                    transform.localPosition = new Vector2(-0.52f, 0.75f);
                    transform.rotation = collision.transform.rotation;

                    inventory.ammoCounter[gun.ammoID] += (int)(ammoCount * UP.ammoMultiplicatorCurrent);

                    ammoCount = 0;

                    break;
                }
                
                // If the inventory IS full AND the gun IS already in the inventory AND it's a gun of the same rarity pick up the ammo.
                if(inventory.isFull[i] == true  && ammoCount > 0 && gunID == inventory.gunID[i] && gun.rarity <= inventory.gunGameObject[i].GetComponent<Weapon>().gun.rarity)
                {
                    gunAlreadyInInv = true;
                    inventory.ammoCounter[gun.ammoID] += (int)(ammoCount * UP.ammoMultiplicatorCurrent);
                    ammoCount = 0;
                    FindObjectOfType<AudioManager>().Play("Reload");
                }
                else if (inventory.isFull[i] == true && gunID == inventory.gunID[i] && gun.rarity > inventory.gunGameObject[i].GetComponent<Weapon>().gun.rarity)
                {
                    if (ownRarityLight != null)
                    {
                        Destroy(ownRarityLight);
                    }

                    Destroy(inventory.gunGameObject[i]);
                    inventory.gunGameObject[i] = gameObject;

                    isEquipped = true;
                    gunSlot = i;

                    ownIconSlot = inventory.slots[i];

                    ownIconSlotDefaultPosition = ownIconSlot.GetComponent<RectTransform>().anchoredPosition;

                    ownIcon = Instantiate(gun.gunIcon, inventory.slots[i].transform, false);

                    ownIconRect = ownIcon.GetComponent<RectTransform>();

                    ownIconSlotRect = ownIconSlot.GetComponent<RectTransform>();


                    transform.SetParent(collision.transform);

                    //transform.localPosition = new Vector2(-0.35f,0.15f);
                    transform.localPosition = new Vector2(-0.52f, 0.75f);
                    transform.rotation = collision.transform.rotation;

                    inventory.ammoCounter[gun.ammoID] += (int)(ammoCount * UP.ammoMultiplicatorCurrent);

                    ammoCount = 0;

                    break;
                }

            }
            
        }
    }

    void LoadingGunCharge()
    {
        if (loadingGunTimer <= loadingGunTimeToChargeMax)
        {
            loadingGunTimer += Time.deltaTime;   
        }
        if(loadingGunTimer >= loadingGunTimeToChargeMax)
        {
            loadingGunIsChargedMax = true;
        }
        loadingGunChargePercentage = loadingGunTimer / loadingGunTimeToChargeMax;
    }



}
