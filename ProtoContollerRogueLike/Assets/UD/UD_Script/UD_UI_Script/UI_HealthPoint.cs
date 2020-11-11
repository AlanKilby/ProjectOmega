using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthPoint : MonoBehaviour
{
    [SerializeField] PlayerHealth PH;

    Text currentHealth;

    void Start()
    {
        currentHealth = GetComponent<Text>();
    }

    void Update()
    {
        if(currentHealth != null)
        {
            currentHealth.text = PH.currentPlayerHealth.ToString();
        }
    }
}
