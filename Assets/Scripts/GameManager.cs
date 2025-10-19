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
    public List<GameObject> phatoms = new();
    public Transform EnemyHolder;

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
            GameObject enemy = Instantiate(EnemyPrefab, EnemyHolder);
            float randomX = Random.Range(-5, 5);
            float randomY = Random.Range(-5, 5);
            enemy.transform.position = new Vector3(randomX, randomY, 0);            
            currentTime_1 = 0;
        }

    }
    public void DeleteAllEnemies()
    {
        Debug.Log("Eliminaste a todos los enemigos");
        foreach (GameObject enemy in phatoms)
        {
            Destroy(enemy);
        }
        phatoms.Clear(); 
    }
}

