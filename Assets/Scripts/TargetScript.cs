using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] private GameObject flames;
    [SerializeField] private GameObject flameEffects;
    [SerializeField] private float life;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float sphereRadius;

    bool isBurning;
    private void Update()
    {
        if (isBurning)
        {
            life -= Time.deltaTime;
            if (life <= 0)
            {
                life = 0;
                Explosion();
            }
        }
    }
    public void Explosion()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        CameraShake.instance.StartCameraShake(0.7f, 0.8f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);

        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;
            if (obj.CompareTag("Target"))
            {
                obj.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, sphereRadius);
                obj.GetComponent<TargetScript>().Burn();
            }
            Invoke(nameof(DestroyObjects), 1f);
        }
    }
    private void DestroyObjects()
    {
        Destroy(gameObject);
    }

    public void Burn()
    {
        flames.SetActive(true);
        isBurning = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBurning)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity,~gameObject.layer))
            {
                Vector3 firePosition = hit.point - new Vector3(0, 0.3f, 0);
                print(hit.transform.position);
                Instantiate(flameEffects, firePosition, Quaternion.Euler(-90,0,0));
            }
            if (collision.gameObject.tag == "Target")
            {
                collision.gameObject.GetComponent<TargetScript>().Burn();
            }
            
        }
    }
}
