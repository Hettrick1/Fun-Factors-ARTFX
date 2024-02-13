using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrenadeScript : MonoBehaviour
{
    [SerializeField] private float throwGrenadeForce;
    [SerializeField] private int grenadeAmount;

    [SerializeField] private Transform[] grenadePosition;
    [SerializeField] private GameObject grenade;
    [SerializeField] private float grenadeDelay;

    bool canShoot;
    private Transform targetTransform;
    [SerializeField] private Transform weaponTransform;

    public static GrenadeScript instance;

    private void Start()
    {
        instance = this;
        targetTransform = weaponTransform;
    }

    public void ShootGrenade(InputAction.CallbackContext context)
    {
        if (context.performed && grenadeAmount > 0)
        {
            GetComponent<Animator>().SetTrigger("Shoot");
            CameraShake.instance.StartCameraShake(0.15f, 0.2f);
            for (int i=0; i<grenadePosition.Length; i++)
            {
                grenadeAmount--;
                GameObject lastGrenade = Instantiate(grenade, grenadePosition[i].position + transform.forward, grenadePosition[i].rotation);
                if (FirstPersonCamera.instance.AimCenter().distance == 0)
                {
                    lastGrenade.GetComponent<Rigidbody>().AddForce(((transform.forward * throwGrenadeForce) + (transform.up * throwGrenadeForce * 0.3f)), ForceMode.Impulse);
                }
                else
                {
                    lastGrenade.GetComponent<Rigidbody>().AddForce(((transform.forward * Mathf.Clamp(FirstPersonCamera.instance.AimCenter().distance, 0f, throwGrenadeForce)) + (transform.up * Mathf.Clamp(FirstPersonCamera.instance.AimCenter().distance, 0f, throwGrenadeForce) * 0.3f)), ForceMode.Impulse);
                }

                lastGrenade.GetComponent<GrenadeExplosion>().InvokeExplosion(grenadeDelay);
            }
        }
    }

    private void ShootGrenade()
    {

    }

    public void AddGrenade(int nbrGrenade)
    {
        grenadeAmount += nbrGrenade;
    }
}
