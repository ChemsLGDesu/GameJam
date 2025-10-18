using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public string monsterName;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;
    public Vector2 spawnAreaMin = new Vector2(-10, -10);
    public Vector2 spawnAreaMax = new Vector2(10, 10);
    public float followRange = 5f;
    public float moveSpeed = 2f; 

    private float nextSpawnTime;
    protected bool isActive = true;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    private Transform playerTransform; 

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        SetNextSpawnTime();

        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    protected virtual void Update()
    {
        if (!isActive)
        {
            nextSpawnTime -= Time.deltaTime;
            if (nextSpawnTime <= 0f)
            {
                Respawn();
            }
        }
        else if (playerTransform != null)
        {
            FollowPlayerIfInRange();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            DestroyEnemy();
        }
    }

    protected void DestroyEnemy()
    {
        isActive = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (collider2D != null) collider2D.enabled = false;
        SetNextSpawnTime();
    }

    protected void Respawn()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        transform.position = randomPos;
        isActive = true;
        if (spriteRenderer != null) spriteRenderer.enabled = true;
        if (collider2D != null) collider2D.enabled = true;
    }

    protected void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void FollowPlayerIfInRange()
    {
        // Calcular la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Si el jugador está dentro del rango, moverse hacia él
        if (distanceToPlayer <= followRange)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }
}
