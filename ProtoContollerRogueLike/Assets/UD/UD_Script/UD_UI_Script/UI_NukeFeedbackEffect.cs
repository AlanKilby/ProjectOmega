using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NukeFeedbackEffect : MonoBehaviour
{
    Animator nukeFeedback;
    public bool explode;

    void Start()
    {
        explode = false;
        nukeFeedback = GetComponent<Animator>();
    }

    void Update()
    {
        nukeFeedback.SetBool("explode", explode);
    }

    public void SetExplodeFalse()
    {
        explode = false;
    }
}
