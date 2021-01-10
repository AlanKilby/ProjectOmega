using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleList : MonoBehaviour
{
    int randomLoot;
    public GameObject[] modules;

    GameObject player;

    public bool canDrop;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void DropModule(Transform moduleParent, Transform thisObject)
    {
        // Il reste a faire un changement pour que le drop ne soit pas fait lorsque l'inventaire de modules est plein ! 

        randomLoot = Random.Range(0, modules.Length);
        //randomLoot = 1;
        Debug.Log(randomLoot);

        for(int i = 0; i <= player.GetComponent<Inventory>().moduleGameObject.Length; i++)
        {
            Debug.Log(i);



            if (i >= player.GetComponent<Inventory>().moduleGameObject.Length)
            {
                GameObject drop = Instantiate(modules[randomLoot], thisObject.transform.position, Quaternion.identity);
                drop.transform.SetParent(moduleParent);
            }
            if (i != player.GetComponent<Inventory>().moduleGameObject.Length && player.GetComponent<Inventory>().moduleGameObject[i] != null && modules[randomLoot] == player.GetComponent<Inventory>().moduleGameObject[i])
            {
                randomLoot = Random.Range(0, modules.Length);
                i = 0;
                Debug.Log(randomLoot);
                Debug.Log("reset loop");
            }
            //else if(i != player.GetComponent<Inventory>().moduleGameObject.Length && player.GetComponent<Inventory>().moduleGameObject[i] != null && modules[randomLoot] != player.GetComponent<Inventory>().moduleGameObject[i])
            //{
            //    Debug.Log("different " + i);
            //}





        }

       
        
    }
}
