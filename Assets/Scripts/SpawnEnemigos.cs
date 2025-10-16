using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public Transform jugador;
    public float distanciaExtra = 2f;
    public float tiempoEntreSpawns = 2f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tiempoEntreSpawns)
        {
            SpawnEnemigo();
            timer = 0f;
        }
    }

    void SpawnEnemigo()
    {
        Vector3 posJugador = jugador.position;

        Vector3 esquinaInferiorIzq = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 esquinaSuperiorDer = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        float xMin = esquinaInferiorIzq.x - distanciaExtra;
        float xMax = esquinaSuperiorDer.x + distanciaExtra;
        float yMin = esquinaInferiorIzq.y - distanciaExtra;
        float yMax = esquinaSuperiorDer.y + distanciaExtra;

        int borde = Random.Range(0, 4);
        Vector3 spawnPos = Vector3.zero;

        switch (borde)
        {
            case 0: // izquierda
                spawnPos = new Vector3(xMin, Random.Range(yMin, yMax), 0);
                break;
            case 1: // derecha
                spawnPos = new Vector3(xMax, Random.Range(yMin, yMax), 0);
                break;
            case 2: // abajo
                spawnPos = new Vector3(Random.Range(xMin, xMax), yMin, 0);
                break;
            case 3: // arriba
                spawnPos = new Vector3(Random.Range(xMin, xMax), yMax, 0);
                break;
        }

        Instantiate(enemigoPrefab, spawnPos, Quaternion.identity);
    }
}