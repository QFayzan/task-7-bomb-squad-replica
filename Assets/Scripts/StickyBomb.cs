using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    private Rigidbody rb;
    public float delay =2.0f;
    public float bombRadius = 2.0f;
    public float explosionForce = 700.0f;
    public float countdown;
    public GameObject explosionEffect;
    bool hasExploded = false;
   public bool doExplode = false;
    void Start()
    {
        doExplode = false;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded && doExplode )
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
       if (rb !=null)
       {
        rb.AddExplosionForce(explosionForce , transform.position , bombRadius);
       }
        var player = nearbyObject.GetComponent<PlayerController>();
       if (player != null)
       {
        player.playerHealth -=4;
       }
       var enemy = nearbyObject.GetComponent<EnemyController>();
       if (enemy != null) 
       {
        enemy.enemyHealth -=4;
       }
       hasExploded =true;
       Destroy(gameObject);  
    }
    }
   
    
}
