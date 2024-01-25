using UnityEngine;
using System.Collections;

public class laser_puerta : MonoBehaviour
{
    public Vector3 posicionOriginal;
    public Vector3 posicionDeseada;
    public float tiempoEsperaMinimo = 4.0f;
    public float tiempoEsperaMaximo = 8.0f;
    public float velocidadMovimientoMinima = 2.0f;
    public float velocidadMovimientoMaxima = 5.0f;

    void Start()
    {
        // Inicia el bucle continuo
        StartCoroutine(MoverEnBucle());
    }

    IEnumerator MoverEnBucle()
    {
        while (true)
        {
            // Mueve hacia la posición deseada con velocidad aleatoria
            yield return MoverObjetoCoroutine(posicionDeseada, Random.Range(tiempoEsperaMinimo, tiempoEsperaMaximo), Random.Range(velocidadMovimientoMinima, velocidadMovimientoMaxima));

            // Espera un tiempo aleatorio antes de mover de vuelta a la posición original
            yield return new WaitForSeconds(Random.Range(tiempoEsperaMinimo, tiempoEsperaMaximo));

            // Mueve de vuelta a la posición original con velocidad aleatoria
            yield return MoverObjetoCoroutine(posicionOriginal, Random.Range(tiempoEsperaMinimo, tiempoEsperaMaximo), Random.Range(velocidadMovimientoMinima, velocidadMovimientoMaxima));

            // Espera un tiempo aleatorio antes de la próxima iteración
            yield return new WaitForSeconds(Random.Range(tiempoEsperaMinimo, tiempoEsperaMaximo));
        }
    }

    IEnumerator MoverObjetoCoroutine(Vector3 destino, float tiempoEspera, float velocidadMovimiento)
    {
        float tiempoInicio = Time.time;
        Vector3 origen = transform.position;

        while (Time.time - tiempoInicio < tiempoEspera)
        {
            transform.position = Vector3.Lerp(origen, destino, (Time.time - tiempoInicio) * velocidadMovimiento);
            yield return null;
        }

        // Asegurarse de que el objeto esté en la posición exacta al final del movimiento
        transform.position = destino;
    }
}