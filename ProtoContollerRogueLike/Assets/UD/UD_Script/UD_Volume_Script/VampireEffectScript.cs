using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VampireEffectScript : MonoBehaviour
{
    Volume volume;

    public float disapearSpeed;

    void Start()
    {
        volume = GetComponent<Volume>();
    }

    void Update()
    {
        volume.weight = Mathf.Clamp(volume.weight, 0f, 1f);
    }

    private void FixedUpdate()
    {
        volume.weight = volume.weight - disapearSpeed * 0.01f;
    }

    public void RedEffect()
    {
        volume.weight = 1f;
    }
}