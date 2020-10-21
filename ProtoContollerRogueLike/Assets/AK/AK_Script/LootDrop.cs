using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public float baseDropRate;
    public float dropGun;
    public float dropModule;
    public float dropPowerUp;

    private WeaponList weaponList;


    private void Start()
    {
        weaponList = GameObject.FindGameObjectWithTag("Weapon List").GetComponent<WeaponList>();

        baseDropRate = baseDropRate / 100;
        dropGun = dropGun / 100;
        dropModule = (dropGun + dropModule) / 100;
        dropPowerUp = (dropModule + dropPowerUp) / 100;
    }
    public void DropLoot()
    {
        if (Random.value <= baseDropRate)
        {
            if(Random.value <= dropGun)
            {
                Debug.Log("Drop Gun");
                Instantiate(weaponList.weapons[0], gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (Random.value > dropGun && Random.value <= dropModule)
            {
                Debug.Log("Drop Module"); 
                Destroy(gameObject);
            }
            else if (Random.value > dropModule && Random.value <= dropPowerUp )
            {
                Debug.Log("Drop Power Up");
                Destroy(gameObject);
            }
        }
    }
}
