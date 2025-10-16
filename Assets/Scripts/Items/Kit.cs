using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Kit : ItemTypes
{
    [SerializeField] private Transform target;
    public int givehealth = 2;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("El player a incrementado su vida en"+givehealth);
            collision.GetComponent<Player>().Health += givehealth;
            Destroy(gameObject);
        }
    }
}
