using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeLight : MonoBehaviour
{
    private Rigidbody rb;
    public float delay =7.0f;
    public float bombRadius = 5.0f;
    public float explosionForce = 700.0f;
    float countdown;
    public GameObject explosionEffect;
    bool hasExploded = false;
    bool doExplode = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded )
        {
            Explosion();
            hasExploded = true;
        }
    }
    void Explosion()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
       Collider[] colliders =  Physics.OverlapSphere(transform.position , bombRadius);
       foreach(Collider nearbyObject in colliders)
       {
       Rigidbody rb =  nearbyObject.GetComponent<Rigidbody>();
       if (rb.gameObject.tag =="Enemy" && doExplode)
       {
        rb.isKinematic = true;
        rb.AddExplosionForce(explosionForce , transform.position , bombRadius);
        hasExploded = true;
        EnemyController.enemyHealth-=4;
       }
       
       }
        
    }
    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag ==("Enemy") )
        {
            doExplode = true;
             countdown = delay;
        }
    }
}
