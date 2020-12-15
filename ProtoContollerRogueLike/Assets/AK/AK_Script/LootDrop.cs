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

    public CameraSystem thisRoom;

    
    private void Start()
    {
        weaponList = GameObject.FindGameObjectWithTag("LootList").GetComponent<WeaponList>();
        moduleList = GameObject.FindGameObjectWithTag("LootList").GetComponent<ModuleList>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();

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

                weaponList.DropGun(thisRoom.transform, gameObject.transform);
                //GameObject drop = Instantiate(weaponList.weapons[Random.Range(0,weaponList.weapons.Length)], gameObject.transform.position, Quaternion.Euler(0,0,Random.Range(-180,180)));
                //drop.transform.SetParent(thisRoom.transform);
                Destroy(gameObject);
            }
            else if (randomDrop > dropGun && randomDrop <= dropModule)
            {
                Debug.Log("Drop Module");
                GameObject drop = Instantiate(moduleList.modules[Random.Range(0, moduleList.modules.Length)], gameObject.transform.position, Quaternion.identity);
                drop.transform.SetParent(thisRoom.transform);
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
