using System.Collections;
using UnityEngine;

public class patrulla : MonoBehaviour
{
    public float limiteInferior = -5f;
    public float limiteSuperior = 5f;
    public float duracionTotal = 10f;
    public float velocidadX = 2f;
    public float probabilidadRotacion = 0.1f;
    public float duracionRotacion = 1.0f;
    public Light luzSpot;

    private Quaternion rotacionOriginal;

    void Start()
    {
        rotacionOriginal = transform.rotation;
        StartCoroutine(Patrullar());
    }

    IEnumerator Patrullar()
    {
        float tiempoPasado = 0f;

        while (tiempoPasado < duracionTotal)
        {
            // Usa Mathf.PingPong para moverse entre los límites
            float nuevaPosicionX = Mathf.PingPong(tiempoPasado * velocidadX, limiteSuperior - limiteInferior) + limiteInferior;

            // Aplica la nueva posición
            transform.position = new Vector3(nuevaPosicionX, transform.position.y, transform.position.z);

            if (luzSpot == null || luzSpot.color != Color.yellow)
            {
                if (Random.value < probabilidadRotacion)
                {
                    // Rotación de -20 grados en el eje X
                    transform.Rotate(-20f, 0f, 0f);

                    // Espera antes de restaurar la rotación
                    yield return new WaitForSeconds(duracionRotacion);

                    // Restaura la rotación original
                    transform.rotation = rotacionOriginal;
                }
            }

            yield return null;
            tiempoPasado += Time.deltaTime;
        }
    }
}
