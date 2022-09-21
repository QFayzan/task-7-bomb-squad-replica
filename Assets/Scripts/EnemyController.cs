using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
private Animator enemyAnim;
private GameObject player;      //so far its being used to search for player
 public float moveSpeed = 4.5f; // movespeed value
 private bool isDead = false;
 public bool isKillable = false;// used to prevent destruction when they wall collide without player input
 private Rigidbody enemyRb;   // rigidbody for movement
 public static int enemyHealth = 10;
 public float rotationSpeed = 720;
 
void Start()
{
    enemyHealth = 10;
    isDead = false;
    enemyAnim = GetComponent<Animator>();
    isKillable = false; //Must be set again in start to make sure new prefab dont spawn with this enabled
    player = GameObject.Find("Player"); //Temporary method to find player
    enemyRb = GetComponent<Rigidbody>();//referenced for movement script
   
}

void Update()
{
    //have something that reduces health here
    Vector3 lookDirection = (player.transform.position - transform.position).normalized; //Basic tracking using normalized vector to finalize distance
    //Vector3 lookDistance = (player.transform.position - transform.position); //the goal was to make objects seem inactive unless player is close didnt work

    // get a vector towards player's transform x and z values
    Vector3 movementDirection = player.transform.position - transform.position;
   
    if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation ,toRotation , rotationSpeed * Time.deltaTime);
        }
       // as long as u are 4 units farther keep going towards player
      //  if(movementDirection.x > 4 || movementDirection.z >4 )
      //  {
       // enemyRb.AddForce(lookDirection* moveSpeed);
      //  }
       // else
       // {
      ////  enemyRb.velocity = Vector3.zero;
       // }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput ==0 && verticalInput==0 )
        {
            enemyAnim.SetBool("isMoving" , false);
        }
        else 
        {
             enemyAnim.SetBool("isMoving" , true);
        }
        if (enemyHealth <1 && !isDead ) //the convulted thing is done to separate all dying logic and such in one fucntion for particles etc
        {
            Die();
        }
}
    void Die()
    {
        isDead = true;
        enemyAnim.SetBool("Death_b", true);
        enemyAnim.SetInteger("DeathType_int", 2);
        Destroy(this.gameObject , 5.0f);
        //score system
        PlayerController.score += 25; 
        //score system implementation
    }
}