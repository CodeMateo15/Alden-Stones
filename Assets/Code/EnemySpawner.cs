using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy;
    Vector2 whereToSpawn;

    private int rand;
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;
    private float enemyCount;
    private float enemyMax;
    private float spawnsupervisor;

    void Start()
    {
        enemyMax = Random.Range(1, 3);
        spawnsupervisor = Random.Range(1, 100);
        UnityEngine.Debug.Log(spawnsupervisor);
    }

    void Update()
    {
        if(spawnsupervisor < 41)
        {
            if (enemyCount < enemyMax)
            {
                if (Time.time > nextSpawn)
                {
                    nextSpawn = Time.time + spawnRate;
                    whereToSpawn = new Vector2(transform.position.x, transform.position.y);
                    rand = Random.Range(0, enemy.Length);
                    Instantiate(enemy[rand], whereToSpawn, enemy[rand].transform.rotation);
                    enemyCount = enemyCount + 1f;
                }
            }
        }
    }
}