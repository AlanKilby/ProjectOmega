using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Gun gun;

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
    


    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        audioSource = gameObject.GetComponent<AudioSource>();
        canShoot = true;
        isEquipped = false;
        gunSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunSpriteRenderer.sprite = gun.weaponSprites[0];
        gunID = gun.ID;
        Debug.Log(gunID);
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
        if (isEquipped && Input.GetButton("Fire1") && inventory.ammoCounter > 0)
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
            inventory.ammoCounter--;
            canShoot = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    inventory.gunGameObject[i] = gameObject;

                    gunSlot = i;

                    isEquipped = true;

                    inventory.gunID[i] = gunID;

                    inventory.isFull[i] = true;

                    Instantiate(gun.gunIcon, inventory.slots[i].transform, false);

                    transform.SetParent(collision.transform);

                    transform.localPosition = new Vector2(-0.35f,0.15f);

                    transform.rotation = collision.transform.rotation;

                    inventory.ammoCounter += ammoCount;

                    ammoCount = 0;

                    break;
                }
                
                if(inventory.isFull[i] == true  && ammoCount > 0 && gunID == inventory.gunID[i])
                {
                    inventory.ammoCounter += ammoCount;
                    ammoCount = 0;
                    audioSource.PlayOneShot(gun.gunSounds[1]);
                }

            }
            
        }
    }

    
}
