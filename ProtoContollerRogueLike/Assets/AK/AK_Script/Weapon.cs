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
    


    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        audioSource = gameObject.GetComponent<AudioSource>();
        canShoot = true;
        isEquipped = false;
        gunSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunSpriteRenderer.sprite = gun.weaponSprites[0];
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
        if (canShoot)
        {
            GameObject bullet = Instantiate(gun.ammoType, shootingPoints[0].transform.position, shootingPoints[0].transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gun.accuracy, gun.accuracy)));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.up * gun.bulletVelocity, ForceMode2D.Impulse);
            inventory.ammoCounter--;
            audioSource.PlayOneShot(gun.gunSounds[0]);
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
                    isEquipped = true;

                    inventory.isFull[i] = true;

                    Instantiate(gun.gunIcon, inventory.slots[i].transform, false);

                    transform.SetParent(collision.transform);

                    transform.localPosition = new Vector2(-0.35f,0.15f);

                    transform.rotation = collision.transform.rotation;

                    inventory.ammoCounter += ammoCount;

                    ammoCount = 0;

                    break;
                }
                else if(inventory.isFull[i] == true && ammoCount > 0)
                {
                    inventory.ammoCounter += ammoCount;
                    ammoCount = 0;
                    audioSource.PlayOneShot(gun.gunSounds[1]);
                }

            }
            
        }
    }

    
}
