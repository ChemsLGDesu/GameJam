using NUnit.Framework;
using System.Collections.Generic; // para listas
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int counter = 0;
    [SerializeField] private Player player;
    public GameObject RecollectableObject;
    public GameObject EnemyPrefab;
    public float currentTime;
    [SerializeField] private float currentTime_1;
    public float TimeSpawnBullet;
    [SerializeField] private float TimeSpawnPhatom;
    public List<Transform> Spawnerphatoms_1 ;    

    void Start()
    {

    }


    void Update()
    {
        SpawntimePhatoms();       
        currentTime += Time.deltaTime;
    }

    public void SpawntimePhatoms()
    {
        currentTime_1 += Time.deltaTime;
        if (currentTime_1 >= TimeSpawnPhatom)
        {
            int indexRandom = Random.Range(0,Spawnerphatoms_1.Count);
            Instantiate(EnemyPrefab,Spawnerphatoms_1[indexRandom].position,Quaternion.identity);
            currentTime_1 = 0;
        }

    }
}

