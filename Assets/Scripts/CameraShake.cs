using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private float ShakeTime = 0.0f;
    [SerializeField]
    private float decreaseRate = 5.0f;
    [SerializeField]
    private float magnitude = 0.7f;

    private Vector3 originalPosition;

    void OnEnable()
    {
        originalPosition = transform.position;
    }

    public void ShakeFor(float time)
    {
        ShakeTime = time;
    }

    void Update()
    {
        if (ShakeTime > 0)
        {
            transform.position = originalPosition + Random.insideUnitSphere * magnitude;
            ShakeTime -= Time.deltaTime * decreaseRate;
        }
        else
        {
            transform.position = originalPosition;
        }
    }
}

