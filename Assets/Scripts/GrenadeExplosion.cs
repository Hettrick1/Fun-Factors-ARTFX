using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    private GameObject fxExplosion;
    [SerializeField] private float sphereRadius;
    public void InvokeExplosion(float delay)
    {
        Invoke(nameof(Explosion), delay);
    }
    public void Explosion()
    { 
        fxExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        CameraShake.instance.StartCameraShake(0.7f, 0.8f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);

        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;
            if (obj.CompareTag("Target"))
            {
                obj.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, sphereRadius);
                obj.GetComponent<TargetScript>().Burn();
            }
           
        }
       
        Invoke(nameof(DestroyObjects), 1f);
        
    }
    private void DestroyObjects()
    {
        Destroy(fxExplosion.gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Target"))
        {
            Explosion();
        }
    }
}
