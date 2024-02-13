using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    [SerializeField]private Transform cameraTransform;
    private Vector3 originalCameraPosition;
    bool canShake;
    float shakeFrequency;


    private void Start()
    {
        instance = this;
        originalCameraPosition = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (canShake)
        {
            cameraTransform.localPosition = originalCameraPosition + Random.insideUnitSphere * shakeFrequency;
        }
        else
        {
            cameraTransform.localPosition = originalCameraPosition;
        }
    }

    public void StartCameraShake(float shake, float time)
    {
        shakeFrequency = shake;
        canShake = true;
        Invoke(nameof(StopCameraShake), time);
    }

    private void StopCameraShake()
    {
        canShake = false;
    }


}
