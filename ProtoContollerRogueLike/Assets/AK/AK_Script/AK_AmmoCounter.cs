using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AK_AmmoCounter : MonoBehaviour
{
    private Text counter;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        counter = gameObject.GetComponent<Text>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.ammoCounter[inventory.gunGameObject[inventory.equippedSlot].GetComponent<Weapon>().gun.ammoID]<=99999)
        {
            counter.text = inventory.ammoCounter[inventory.gunGameObject[inventory.equippedSlot].GetComponent<Weapon>().gun.ammoID].ToString();
        }
        else
        {
            counter.text = "99999";
        }
    }
}
