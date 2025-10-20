using UnityEngine;


public class EfectoSonido : MonoBehaviour
{
    [SerializeField] private AudioClip Point;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ControladorSonido.Instance.EjecutarSonido(Point);
            Destroy(gameObject);
        }
    }
}
