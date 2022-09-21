using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject[] bombs;
    private GameObject player;
    private Rigidbody bombRb;
    public float launchSpeed = 200.0f;
    public static bool redToken = false;
    public bool doShoot = true;
    public static float shootTime = 0.0f;
    public static int blueToken = 0;
    public static int greenToken = 0;
    // Start is called before the first frame update
    void Start()
    {
        bombRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player"); //Temporary method to find player
    }

    // Update is called once per frame
    void Update()
    {
        if (redToken)
        {
            shootTime += 10;
            redToken = false;
        }
        shootTime = shootTime + Time.deltaTime;
        if (shootTime > 0.6f)
        {
            doShoot = true;
        }
        Vector3 spawnLocation = new Vector3(player.transform.position.x, player.transform.position.y+2f,player.transform.position.z+0.5f);
        //Vector3 lookDirection = PlayerController.movementDirection;
        Vector3 lookDirection = player.transform.forward;
        Quaternion rotationNow = GetComponentInParent<Transform>().rotation;

        
        if(Input.GetKeyDown(KeyCode.J) && doShoot)
        {
           var instance = Instantiate(bombs[0] , spawnLocation ,transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(lookDirection * launchSpeed ,  ForceMode.Acceleration);
            Debug.Log("Normal Bomb");
        }
        if(Input.GetKeyDown (KeyCode.K) && blueToken >1)
        {
            var instance = Instantiate(bombs[1] , spawnLocation ,transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(lookDirection * launchSpeed ,  ForceMode.Acceleration);
            blueToken--;
            Debug.Log("Light Bomb");
            
        }
        if(Input.GetKeyDown (KeyCode.L) && greenToken >1)
        {
           var instance = Instantiate(bombs[2] , spawnLocation ,transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(lookDirection * launchSpeed ,  ForceMode.Acceleration);
            greenToken--;
            Debug.Log("Heavy Bomb");
        }

    }
}
