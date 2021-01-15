using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleList : MonoBehaviour
{
    //int randomLoot;
    public GameObject[] modules;

    GameObject player;

    Inventory playerInventory;

    public bool canDrop;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }

    public void DropModule(Transform moduleParent, Transform thisObject)
    {
        // Il reste a faire un changement pour que le drop ne soit pas fait lorsque l'inventaire de modules est plein ! 
        int i;
        int randomLoot = Random.Range(0, modules.Length);
        //randomLoot = 1;
        Debug.Log(randomLoot);

        if(playerInventory.moduleGameObject[playerInventory.moduleGameObject.Length-1] == null)
        {
            for (i = 0; i <= playerInventory.moduleGameObject.Length; i++)
            {
                //Debug.Log(player.GetComponent<Inventory>().moduleGameObject[i]);


                if (i < playerInventory.moduleGameObject.Length && playerInventory.moduleGameObject[i] != null && modules[randomLoot].GetComponent<Module>().moduleID == playerInventory.moduleGameObject[i].GetComponent<Module>().moduleID)
                {
                    randomLoot = Random.Range(0, modules.Length);
                    i = -1;
                    Debug.Log(randomLoot);
                    Debug.Log("reset loop");
                }
                else if (i >= playerInventory.moduleGameObject.Length)
                {
                    GameObject drop = Instantiate(modules[randomLoot], thisObject.transform.position, Quaternion.identity);
                    drop.transform.SetParent(moduleParent);
                }
                //else if(i != player.GetComponent<Inventory>().moduleGameObject.Length && player.GetComponent<Inventory>().moduleGameObject[i] != null && modules[randomLoot] != player.GetComponent<Inventory>().moduleGameObject[i])
                //{
                //    Debug.Log("different " + i);
                //}





            }
        }      
        
    }
}
