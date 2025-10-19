using UnityEngine;

public class Enemy : Entity , IDamage
{
    [SerializeField] protected int Enemy_vida =40;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;
    public Vector2 spawnAreaMin = new Vector2(-10, -10);
    public Vector2 spawnAreaMax = new Vector2(10, 10);
    public float followRange = 5f;
    public float moveSpeed = 2f;
    private float nextSpawnTime;
    protected bool isActive = true;
    private SpriteRenderer spriteRenderer;
    private Collider2D collition;
    private Transform playerTransform;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collition = GetComponent<Collider2D>();
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

    private void OnTriggerEnter2D(Collider2D collition)
    {
        if (collition.tag == "Player")
        {
            collition.GetComponent<Player>().ReceiveDamage(10);           
        }
    }

    protected void DestroyEnemy()
    {
        isActive = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (collition != null) collition.enabled = false;
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
        if (collition != null) collition.enabled = true;
    }

    protected void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void FollowPlayerIfInRange()
    {
        
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        
        if (distanceToPlayer <= followRange)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void ReceiveDamage(int damage)
    {
        damage = 5;
        Enemy_vida -= damage;
        if(Enemy_vida <= 0)
        {
            Debug.Log("Enemy muelto X_X");
            Destroy(gameObject);
        }
    }
    
}
