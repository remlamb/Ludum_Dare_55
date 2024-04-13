using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private GameObject[] projectileSpawners;

    [SerializeField] private GameObject enemy;

    private Transform currentSpawner;
    [SerializeField] private float timerBetweenSpawn;

    private float timer; 
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timerBetweenSpawn)
        {
            currentSpawner = projectileSpawners[Random.Range(0, projectileSpawners.Length)].transform;
            Instantiate(enemy, currentSpawner.position, Quaternion.identity);
            timer = 0.0f;
        }
    }
}
