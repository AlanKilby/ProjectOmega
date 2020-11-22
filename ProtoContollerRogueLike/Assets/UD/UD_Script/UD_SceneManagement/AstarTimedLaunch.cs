using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarTimedLaunch : MonoBehaviour
{
    [SerializeField] GameObject astarObject;
    public float waitTime;

    private void Start()
    {
        astarObject.SetActive(false);
        Invoke("SpawnAstar", waitTime);
    }

    private void SpawnAstar()
    {
        astarObject.SetActive(true);
    }
}
