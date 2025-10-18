using UnityEngine;
using UnityEngine.UI;
public class BarraDeVida : MonoBehaviour
{
    private Player player;
    public float currentlife = 50;
    [Header("Interfaz")]

    public Image BarraSalud;
    public Text TextoSalud;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        currentlife = player.Health;
    }
    void Update()
    {
        ActualizarInterfaz();
    }
    public void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = player.Health / currentlife;
        TextoSalud.text = "+ " + player.Health.ToString("f0");
    }
}
