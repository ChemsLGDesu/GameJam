using UnityEngine;

public class Kit : MonoBehaviour
{
    [SerializeField] private float givehealth = 2;
    [SerializeField] private Transform target;
    void Start()
    {
        //target = GameObject.FindGameObjectsWithTag("Player").GetComponent<Transform>
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("El player a incrementado su vida en"+givehealth);
            //collision.GetComponent<Player>

        }
    }
}
