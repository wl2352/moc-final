using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration = 0;
    private float shakeMagnitude;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration <= 0)
        {
            // Decay
            shakeMagnitude *= 0.99f;
            if (shakeMagnitude <= 0.00001 && shakeMagnitude > 0)
            {
                transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
                shakeMagnitude = 0;
            }
        }
        else
        {
            shakeDuration -= Time.deltaTime;
        }
        transform.localPosition = new Vector3(Random.value * shakeMagnitude, Random.value * shakeMagnitude, transform.localPosition.z);
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}