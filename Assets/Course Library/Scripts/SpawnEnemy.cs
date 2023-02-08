using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject enemy;
    private float spawnRange = 9.0f;
    private int enemyCount;
    private int waveNumber = 1;
    public GameController gc;
    public GameObject powerUp;
    void Start()
    {
        SpawnEnemyWave(waveNumber);

    }

    void SpawnEnemyWave(int enemies)
    {
        for (int i = 0; i < enemies; i++)
        {
            Instantiate(enemy, GenerateSpawnPosition(),
                enemy.transform.rotation);
        }
    }
    Vector3 GenerateSpawnPosition()
    {
        float enemyX = Random.Range(-spawnRange, spawnRange);
        float enemyY = Random.Range(-spawnRange, spawnRange);
        return (new Vector3(enemyX, 0.0f, enemyY));
    }


    void CheckPowerUP()
    {
        if (GameObject.FindGameObjectsWithTag("PickUp").Length == 0) {

            Instantiate(powerUp, GenerateSpawnPosition(),powerUp.transform.rotation);
        
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        GameObject oJogador = GameObject.Find("Player");
        if (oJogador != null)
        {
            if (enemyCount == 0)
            {
                SpawnEnemyWave(++waveNumber);
                CheckPowerUP();
            }
        }



    }
}



