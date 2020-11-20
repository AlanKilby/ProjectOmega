using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DashCoolDawn : MonoBehaviour
{
    [SerializeField] PlayerMouvement PM = null;

    [SerializeField] Slider DashCooldown;

    void Start()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
    }

    void Update()
    {
        DashCooldown.value = PM.dashTimePercent;
    }
}
