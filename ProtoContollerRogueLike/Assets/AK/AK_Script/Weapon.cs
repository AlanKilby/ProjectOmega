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
    public bool isALoadingGun;
    bool loadingGunCharging;
    bool loadingGunIsChargedMax;
    public float loadingGunTimeToChargeMax;
    float loadingGunTimer;
    public float loadingGunChargePercentage;
    public int chargedMaxDamageMultiplicator;
    public bool canStunWhenCharingMax;
    [Header("Unlimited Ammo ?")]
    public bool isUnlimitedAmmoGun;


    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        PMS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModuleStation>();
        UP = GameObject.FindGameObjectWithTag("Player").GetComponent<UpgradePlatform>();
        CP = GameObject.FindGameObjectWithTag("Player").GetComponent<ConsumablePlatform>();
        audioSource = gameObject.GetComponent<AudioSource>();
        canShoot = true;
        isEquipped = false;
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
        if (gunInHand)
        {
            ownIconRect.anchoredPosition = iconOffsetWhenInHand;
            ownIconSlotRect.anchoredPosition = ownIconSlotDefaultPosition + iconOffsetWhenInHand;
        }
        else
        {
            ownIconRect.anchoredPosition = new Vector3(0,0,0);
            ownIconSlotRect.anchoredPosition = ownIconSlotDefaultPosition;
        }

        //Shooting
        if (isEquipped && Input.GetButton("Fire1") && inventory.ammoCounter[gun.ammoID] > 0 && !PM.isDashing && !isALoadingGun)
        {
            WeaponShooting();
        }

        if (isALoadingGun)
        {
            if (isEquipped && Input.GetButtonDown("Fire1") && inventory.ammoCounter[gun.ammoID] > 0 && !PM.isDashing)
            {
                loadingGunCharging = true;
            }

            if(isEquipped && Input.GetButtonUp("Fire1") && inventory.ammoCounter[gun.ammoID] > 0 && !PM.isDashing)
            {
                WeaponShooting();
                loadingGunChargePercentage = 0.0f;
                loadingGunCharging = false;

            }

            if (!isEquipped || inventory.ammoCounter[gun.ammoID] <= 0 || PM.isDashing)
            {
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

    }

    void WeaponShooting()
    {
        if (canShoot && gunSlot == inventory.equippedSlot)
        {
            for (int i = 0; i < shootingPoints.Length; i++)
            {
                if (!isALoadingGun)
                {
                    GameObject bullet = Instantiate(gun.ammoType, shootingPoints[i].transform.position, shootingPoints[i].transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gun.accuracy, gun.accuracy)));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(bullet.transform.up * gun.bulletVelocity, ForceMode2D.Impulse);
                }
                if (isALoadingGun)
                {
                    GameObject bullet = Instantiate(gun.ammoType, shootingPoints[i].transform.position, shootingPoints[i].transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gun.accuracy, gun.accuracy)));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    Bullet Bu = bullet.GetComponent<Bullet>();
                    //Bu.damage = Bu.damage * loadingGunChargePercentage; MARCHE PAS CAR % en Float et Damage en Int
                    if (loadingGunIsChargedMax)
                    {
                        Bu.damage = Bu.damage * chargedMaxDamageMultiplicator;
                        if (canStunWhenCharingMax)
                        {
                            Bu.canStun = true;
                        }
                    }
                    rb.AddForce(bullet.transform.up * gun.bulletVelocity, ForceMode2D.Impulse);
                    loadingGunTimer = 0.0f;
                }
                                            
            }
            if (PMS.soulScream)
            {
                //Mettre le Play du Cri
            }
            else
            {
                audioSource.PlayOneShot(gun.gunSounds[0]);
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
                    
                    //Modif Gus
                    //transform.localPosition = new Vector2(-0.35f,0.15f);
                    transform.localPosition = new Vector2(-0.52f, 0.75f);
                    transform.rotation = collision.transform.rotation;

                    inventory.ammoCounter[gun.ammoID] += (int)(ammoCount * UP.ammoMultiplicatorCurrent);

                    ammoCount = 0;

                    break;
                }
                
                // If the inventory IS full AND the gun IS already in the inventory pick up the ammo.
                if(inventory.isFull[i] == true  && ammoCount > 0 && gunID == inventory.gunID[i])
                {
                    gunAlreadyInInv = true;
                    inventory.ammoCounter[gun.ammoID] += (int)(ammoCount * UP.ammoMultiplicatorCurrent);
                    ammoCount = 0;
                    audioSource.PlayOneShot(gun.gunSounds[1]);
                }

            }
            
        }
    }

    void LoadingGunCharge()
    {
        if(loadingGunTimer <= loadingGunTimeToChargeMax)
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
