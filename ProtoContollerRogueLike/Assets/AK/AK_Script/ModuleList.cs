using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleList : MonoBehaviour
{
    int randomLoot;
    public GameObject[] modules;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void DropModule(Transform moduleParent, Transform thisObject)
    {
        // Il reste a faire un changement pour que le drop ne soit pas fait lorsque l'inventaire de modules est plein ! 

        randomLoot = Random.Range(0, modules.Length - 1);
        Debug.Log(randomLoot);

        for(int i = 0; i < modules.Length; i++)
        {
            if (player.GetComponent<Inventory>().moduleSlots[i] == modules[randomLoot])
            {
                randomLoot = Random.Range(0, modules.Length - 1);
                i = 0;
            }
            
            

        }

        GameObject drop = Instantiate(modules[Random.Range(0, modules.Length)], gameObject.transform.position, Quaternion.identity);
        drop.transform.SetParent(moduleParent);
    }
}
