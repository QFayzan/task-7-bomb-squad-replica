using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;
    public int playerHealth = 10;
    public TextMeshProUGUI scoreText;  //For Scoring system
    public TextMeshProUGUI healthText;
    public static int score= 0;        //score value to be shared with enemy destroy function
    private Rigidbody rb;      // rigidbody for the physics
    public float speed = 60000.0f;   //basic movespeed 
    public float rotationSpeed = 720;
    private bool isDead = false; //basic dying implementation
    public static Vector3 movementDirection; //used to specify vector3
    // 3 bombs 3 bools 3 powerups
    public static bool spawnHeavyBomb = false;
    public static bool spawnLightBomb = false;
    public static bool spawnbomb =  false;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //takes rigidbody for movement etc
        playerAnim = GetComponent<Animator>(); //for animations
    }

    void Update()
    {
        healthText.text = "HP is :" + playerHealth.ToString();
        scoreText.text = " Current Score :" + score.ToString(); //UI assignment done in inspector

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput ==0 && verticalInput==0 )
        {
            playerAnim.SetBool("isMoving" , false);
        }
        else 
        {
             playerAnim.SetBool("isMoving" , true);
        }
        //Sets the rotation of the character in the direction it is moving
        Vector3 movementDirection = new Vector3 (horizontalInput, 0, verticalInput);
       // movementDirection.Normalize();
        //End of direction setting
        if (!isDead)          //only move when alive
        {
        rb.AddForce(Vector3.right * speed * horizontalInput );
        rb.AddForce(Vector3.forward * speed * verticalInput );        
        }
        //Smooth Rotation code that uses rotate speed
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation ,toRotation , rotationSpeed * Time.deltaTime);
        }
        if (playerHealth <1 || transform.position.y <-0.5f) //the convulted thing is done to separate all dying logic and such in one fucntion for particles etc
        {
            Die();
        }
    }
    //Basically end the game if player hits a wall and stuff
    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag ==("Enemy") )
        {
            playerHealth--;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            if (other.name.Contains("Powerup Blue"))
            {
                BombSpawner.blueToken +=3;
            }
            else if (other.name.Contains("Powerup Red"))
            {
                BombSpawner.redToken = true;
            }
            else if (other.name.Contains("Powerup Green"))
            {
                BombSpawner.greenToken +=3;
            }
            //Play collection sound and effect here
            Destroy(other.gameObject);
        }
       
    }
    //the only non performance killer way to stop a game that i could remember at 11 pm
    void Die()
    {
        isDead = true;
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        Destroy(this.gameObject , 5.0f);
       // Time.timeScale = 0; //all decorations and restart logic can be placed here
    }
 }



