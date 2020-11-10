using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Gun gun;
    public PlayerMouvement PM;

    //Graphics 
    public SpriteRenderer gunSpriteRenderer;

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

    GameObject player;
    bool pickUpAvailable = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        audioSource = gameObject.GetComponent<AudioSource>();
        canShoot = true;
        isEquipped = false;
        gunSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunSpriteRenderer.sprite = gun.weaponSprites[0];
        gunID = gun.ID;
        Debug.Log(gunID);
        gunAlreadyInInv = false;
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

        //Shooting
        if (isEquipped && Input.GetButton("Fire1") && inventory.ammoCounter[gun.ammoID] > 0 && !PM.isDashing)
        {
            WeaponShooting();
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

        if (Input.GetKeyDown("v"))
        {
            DropWeapon();
        }

        if(pickUpAvailable == true && Input.GetKeyDown("e"))
        {
            PickUpWeapon();
        }
        



    }

    void WeaponShooting()
    {
        if (canShoot && gunSlot == inventory.equippedSlot)
        {
            for (int i = 0; i < shootingPoints.Length; i++)
            {
                GameObject bullet = Instantiate(gun.ammoType, shootingPoints[i].transform.position, shootingPoints[i].transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gun.accuracy, gun.accuracy)));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bullet.transform.up * gun.bulletVelocity, ForceMode2D.Impulse);
                                            
            }
            audioSource.PlayOneShot(gun.gunSounds[0]);
            inventory.ammoCounter[gun.ammoID]--;
            canShoot = false;
        }

    }

    void DropWeapon()
    {
        if(gunSlot == inventory.equippedSlot)
        {
            transform.SetParent(inventory.gunHolder.transform);
            inventory.gunGameObject[gunSlot] = null;
            isEquipped = false;
            inventory.gunID[gunSlot] = null;
            inventory.isFull[gunSlot] = false;

            // Destroys Gun Icon from UI
            foreach (Transform child in inventory.slots[gunSlot].transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    void PickUpWeapon()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            // If the inventory IS NOT full AND the gun IS NOT already in the inventory pick up the gun
            if (inventory.isFull[i] == false && gunAlreadyInInv == false)
            {
                inventory.gunGameObject[i] = gameObject;

                gunSlot = i;

                isEquipped = true;

                inventory.gunID[i] = gunID;

                inventory.isFull[i] = true;

                Instantiate(gun.gunIcon, inventory.slots[i].transform, false);

                transform.SetParent(player.transform);

                transform.localPosition = new Vector2(-0.35f, 0.15f);

                transform.rotation = player.transform.rotation;

                inventory.ammoCounter[gun.ammoID] += ammoCount;

                ammoCount = 0;

                break;
            }

            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickUpAvailable = true;

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                // If the inventory IS full AND the gun IS already in the inventory pick up the ammo.
                if (inventory.isFull[i] == true && ammoCount > 0 && gunID == inventory.gunID[i])
                {
                    gunAlreadyInInv = true;
                    inventory.ammoCounter[gun.ammoID] += ammoCount;
                    ammoCount = 0;
                    audioSource.PlayOneShot(gun.gunSounds[1]);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickUpAvailable = false;
        }
    }
}
