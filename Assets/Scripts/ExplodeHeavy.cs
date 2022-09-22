using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeHeavy : MonoBehaviour
{
    public float delay =3.0f;
    public float bombRadius = 5.0f;
    public float explosionForce = 2000.0f;
    float countdown;
    public GameObject explosionEffect;
    bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
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
       }
        Destroy(gameObject);
    }
}
