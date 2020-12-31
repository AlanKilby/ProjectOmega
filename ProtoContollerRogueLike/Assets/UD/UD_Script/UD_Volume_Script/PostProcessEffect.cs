using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessEffect : MonoBehaviour
{
    Volume volume;

    public float appearSpeed;
    public float disappearSpeed;
    [HideInInspector] public bool canAppear;
    [HideInInspector] public bool canDisappear;

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
        if (canAppear)
        {
            volume.weight = volume.weight + appearSpeed * 0.01f;
        }
        if (canDisappear)
        {
            volume.weight = volume.weight - disappearSpeed * 0.01f;
        }
    }

    public void LaunchEffectToMax()
    {
        volume.weight = 1f;
    }
}
