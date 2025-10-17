using UnityEngine;

public class Recollectable_Item : ItemTypes
{   
    [SerializeField] private Transform target;
    public GameManager gm;
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
            gm.counter++;
            Destroy(gameObject);
        }
    }
}
