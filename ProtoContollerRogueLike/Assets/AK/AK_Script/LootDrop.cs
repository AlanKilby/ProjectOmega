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
    private ModuleList moduleList;


    private void Start()
    {
        weaponList = GameObject.FindGameObjectWithTag("LootList").GetComponent<WeaponList>();
        moduleList = GameObject.FindGameObjectWithTag("LootList").GetComponent<ModuleList>();

        dropModule = (dropGun + dropModule);
        dropPowerUp = (dropModule + dropPowerUp);

        baseDropRate = baseDropRate / 100;
        dropGun /= 100;
        dropModule /= 100;
        dropPowerUp /= 100;

    }
    public void DropLoot()
    {
        if (Random.value <= baseDropRate)
        {
            float randomDrop = Random.value;

            if(randomDrop <= dropGun)
            {
                Debug.Log("Drop Gun");
                Instantiate(weaponList.weapons[Random.Range(0,weaponList.weapons.Length)], gameObject.transform.position, Quaternion.Euler(0,0,Random.Range(-180,180)));
                Destroy(gameObject);
            }
            else if (randomDrop > dropGun && randomDrop <= dropModule)
            {
                Debug.Log("Drop Module");
                Instantiate(moduleList.modules[Random.Range(0, moduleList.modules.Length)], gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (randomDrop > dropModule && randomDrop <= dropPowerUp )
            {
                Debug.Log("Drop Power Up");
                Destroy(gameObject);
            }
        }
    }
}
