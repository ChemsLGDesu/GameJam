using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float force;
    void Start()
    {
        Destroy(gameObject, 2f);
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().ReceiveDamage(10);
            Destroy(gameObject);
        }
        if(collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
