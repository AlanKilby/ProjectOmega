using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDesign : MonoBehaviour
{
    private GameObject levelGen;
    public GameObject[] roomDesign0;
    public GameObject[] roomDesign1;
    public GameObject[] roomDesign2;




    int roomPicker;

    private void Start()
    {
        levelGen = GameObject.FindGameObjectWithTag("LevelGen");

        if(levelGen.GetComponent<LevelGeneration>().currentDifficulty <= 0)
        {
            roomPicker = Random.Range(0, roomDesign0.Length);
            Instantiate(roomDesign0[roomPicker], new Vector2(transform.position.x, transform.position.y), Quaternion.identity, transform);
        }
        else if (levelGen.GetComponent<LevelGeneration>().currentDifficulty == 1)
        {
            roomPicker = Random.Range(0, roomDesign1.Length);
            Instantiate(roomDesign1[roomPicker], new Vector2(transform.position.x, transform.position.y), Quaternion.identity, transform);
        }
        else if (levelGen.GetComponent<LevelGeneration>().currentDifficulty == 2)
        {
            roomPicker = Random.Range(0, roomDesign2.Length);
            Instantiate(roomDesign2[roomPicker], new Vector2(transform.position.x, transform.position.y), Quaternion.identity, transform);
        }

    }

}
