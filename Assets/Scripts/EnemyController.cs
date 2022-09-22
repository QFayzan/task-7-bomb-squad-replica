using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
private Animator enemyAnim;
private GameObject player;      //so far its being used to search for player
 public float moveSpeed = 4.5f; // movespeed value
 private bool isDead = false;
 private Rigidbody enemyRb;   // rigidbody for movement
 public int enemyHealth = 10;
 public float rotationSpeed = 720;
 
void Start()
{
    enemyHealth = 10;
    isDead = false;
    enemyAnim = GetComponent<Animator>();
    player = GameObject.Find("Player"); //Temporary method to find player
    enemyRb = GetComponent<Rigidbody>();//referenced for movement script
   
}

void Update()
{
    if (!isDead)
    {
    if (transform.position.y >1)
    {
        transform.position = new Vector3 (transform.position.x , 1, transform.position.y);
    }
    Vector3 lookDirection = (player.transform.position - transform.position).normalized; //Basic tracking using normalized vector to finalize distance
    //Vector3 lookDistance = (player.transform.position - transform.position); //the goal was to make objects seem inactive unless player is close didnt work

    // get a vector towards player's transform x and z values
    Vector3 movementDirection = player.transform.position - transform.position;
   
    if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation ,toRotation , rotationSpeed * Time.deltaTime);
        }
       enemyRb.AddForce(lookDirection* moveSpeed);
    }
        if (enemyHealth <1 && !isDead ) //the convulted thing is done to separate all dying logic and such in one fucntion for particles etc
        {
            Die();
        }
}
void OnCollisionEnter (Collision other)
    {
         if (other.gameObject.tag=="Sticky")
       {  
        other.rigidbody.isKinematic=true;     //allows mine to stick
        other.transform.parent = this.transform; //merges transform property of mine with enemy"
        other.gameObject.tag = "Enemy"; // sets tag to enemy so as to not double collide when multiple enemies
        other.gameObject.GetComponent<StickyBomb>().doExplode = true;
        other.gameObject.GetComponent<StickyBomb>().countdown = 3.0f;
                    
       }
    }
    void Die()
    {
        isDead = true;
        enemyRb.detectCollisions= false; // disables rigidbody of dead enemy
        enemyAnim.SetBool("Death_b", true);
        enemyAnim.SetInteger("DeathType_int", 2);
        Destroy(this.gameObject , 5.0f);
        //score system
        PlayerController.score += 25; 
        //score system implementation
    }
}