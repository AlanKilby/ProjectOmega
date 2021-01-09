using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeEffect : MonoBehaviour
{
    [HideInInspector] public float power;
    [HideInInspector] public float duration;

    [HideInInspector] public bool shouldShake;

    Transform cameraPos;

    Vector3 startPos;
    float initialDuration;

    void Start()
    {
        cameraPos = GetComponent<Transform>();
        startPos = cameraPos.localPosition;
        initialDuration = duration;
    }


    void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                Vector2 shakePos = Random.insideUnitSphere * power;
                cameraPos.localPosition = new Vector3(startPos.x + shakePos.x, startPos.y + shakePos.y, transform.localPosition.z);
                duration -= Time.deltaTime;
            }
            else
            {
                duration = initialDuration;
                cameraPos.localPosition = startPos;
                shouldShake = false;
            }
        }
    }

    public void StartShake(float shakePower, float shakeDuration)
    {
        duration = shakeDuration;
        power = shakePower;
        shouldShake = true;
    }
}
