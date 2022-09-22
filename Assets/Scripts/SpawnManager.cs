using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnManager : MonoBehaviour
{
    public TextMeshProUGUI waveNum;
    private float spawnRange = 14;
    public GameObject enemyPrefabs;
    public GameObject[] powerupPrefab;
    public int enemyCount;
    public int waveNumber = 1 ;
    public float rotationSpeed = 720;
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        InvokeRepeating("SpawnPowerup", 2.0f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        waveNum.text = " Wave number is :  " + waveNumber.ToString();
        enemyCount =  GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPointX = Random.Range(-spawnRange , spawnRange);
        float spawnPointZ = Random.Range(-spawnRange , spawnRange);
        Vector3 randomPos = new Vector3 (spawnPointX , 0.3f , spawnPointZ);
        return randomPos;
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0 ; i<enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs , GenerateSpawnPosition() , enemyPrefabs.transform.rotation);
        }
    }
     void SpawnPowerup()
    {
        int index = Random.Range(0, powerupPrefab.Length);
        {
          var instance =  Instantiate(powerupPrefab[index], GenerateSpawnPosition(), powerupPrefab[index].transform.rotation);
          Destroy(instance , 4.0f);
        }

    }
}