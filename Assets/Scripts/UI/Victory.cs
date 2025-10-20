using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Victory : MonoBehaviour
{
    public GameObject panelVictoria;
    private int totalItems;
    private int itemsRecolectados = 0;
    void Start()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Items").Length;
        panelVictoria.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Items"))
        {
            Destroy(other.gameObject);
            itemsRecolectados++;
            if (itemsRecolectados >= totalItems)
            {
                MostrarPanelVictoria();
            }
        }
    }

    void MostrarPanelVictoria()
    {
        panelVictoria.SetActive(true);
        Time.timeScale = 0f; 
    }
    public void RegresarAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
