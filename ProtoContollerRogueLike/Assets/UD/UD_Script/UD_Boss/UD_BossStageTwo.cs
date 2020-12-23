using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossStageTwo : MonoBehaviour
{
    [Header("Tentacle Manager")]
    [SerializeField] GameObject tentacleObject;
    [SerializeField] Transform[] tentacleSpawn;
    /*[SerializeField] Transform tentacleSpawn1;
    [SerializeField] Transform tentacleSpawn2;
    [SerializeField] Transform tentacleSpawn3;
    [SerializeField] Transform tentacleSpawn4;*/
    [SerializeField] float spawnRate;
    float spawnRateTimer;
    [SerializeField] float tentacleLifeDelaySet;
    bool canSpawn;

    void Start()
    {
        
    }

    void Update()
    {
        if (canSpawn)
        {
            for (int i = 0; i < tentacleSpawn.Length; i++)
            {
                GameObject tentacle = Instantiate(tentacleObject, tentacleSpawn[i].position, tentacleSpawn[i].rotation);
                UD_TentacleBoss tentacleScript = tentacleSpawn[i].GetComponent<UD_TentacleBoss>();
                tentacleScript.lifeTimer = tentacleLifeDelaySet;
            }
            /*GameObject tentacle1 = Instantiate(tentacleObject, tentacleSpawn1.position, tentacleSpawn1.rotation);
            UD_TentacleBoss tentacle1Script = tentacleSpawn1.GetComponent<UD_TentacleBoss>();
            tentacle1Script.lifeTimer = tentacleLifeDelaySet;
            GameObject tentacle2 = Instantiate(tentacleObject, tentacleSpawn2.position, tentacleSpawn2.rotation);
            GameObject tentacle3 = Instantiate(tentacleObject, tentacleSpawn3.position, tentacleSpawn3.rotation);
            GameObject tentacle4 = Instantiate(tentacleObject, tentacleSpawn4.position, tentacleSpawn4.rotation);*/ //VERSION DEGUEUE DE CE QUI EST FAIT AU DESSUS
            spawnRateTimer = spawnRate;
            canSpawn = false;
        }
        AcideSprayTimer();
    }

    void AcideSprayTimer()
    {
        if (!canSpawn)
        {
            spawnRateTimer -= Time.deltaTime;
            if (spawnRateTimer <= 0)
            {
                canSpawn = true;
            }
        }
    }
}
