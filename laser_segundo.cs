using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_segundo : MonoBehaviour
{
    public Vector3 anguloMinimo = new Vector3(10.0f, 10.0f, 10.0f);
    public Vector3 anguloMaximo = new Vector3(30.0f, 30.0f, 30.0f);
    public float tiempoRotacionMinimo = 2.0f;
    public float tiempoRotacionMaximo = 5.0f;
    public float tiempoEsperaMinimo = 4.0f;
    public float tiempoEsperaMaximo = 8.0f;

    void Start()
    {
        // Inicia el bucle continuo
        StartCoroutine(RotarEnBucle());
    }

    IEnumerator RotarEnBucle()
    {
        while (true)
        {
            // Rota el objeto de manera aleatoria en el rango especificado
            yield return RotarObjetoCoroutine(new Vector3(Random.Range(anguloMinimo.x, anguloMaximo.x),
                                                           Random.Range(anguloMinimo.y, anguloMaximo.y),
                                                           Random.Range(anguloMinimo.z, anguloMaximo.z)),
                                               Random.Range(tiempoRotacionMinimo, tiempoRotacionMaximo));

            // Espera un tiempo aleatorio antes de la próxima iteración
            yield return new WaitForSeconds(Random.Range(tiempoEsperaMinimo, tiempoEsperaMaximo));
        }
    }

    IEnumerator RotarObjetoCoroutine(Vector3 angulo, float tiempoRotacion)
    {
        float tiempoInicio = Time.time;
        Quaternion rotacionInicial = transform.rotation;
        Quaternion rotacionFinal = Quaternion.Euler(angulo);

        while (Time.time - tiempoInicio < tiempoRotacion)
        {
            // Calcula la interpolación lineal entre la rotación inicial y final
            float factor = (Time.time - tiempoInicio) / tiempoRotacion;
            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, factor);

            yield return null;
        }

        // Asegurarse de que el objeto esté en la rotación exacta al final del movimiento
        transform.rotation = rotacionFinal;
    }
}
