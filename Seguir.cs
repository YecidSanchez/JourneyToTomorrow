using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguir : MonoBehaviour
{
    public MonoBehaviour scriptADesactivar;
    public MonoBehaviour scriptADesactivar2;
    public Transform objetoAperseguir;
    public float velocidad = 5f;
    public float limiteIzquierdo = -5f; // Establece el límite izquierdo
    public float limiteDerecho = 5f;   // Establece el límite derecho
    public Light luzSpot;               // Referencia a la luz spot
    public static bool morir;
    public static float tiempoEnRojo = 0f;    // Tiempo transcurrido en estado rojo

    void Update()
    {
        // Verifica si la luz spot está en color rojo
        if (luzSpot != null && luzSpot.color == Color.yellow)
        {
            tiempoEnRojo += Time.deltaTime; // Incrementa el tiempo en rojo
            
            // Continúa con la lógica de persecución solo si la luz está en rojo
            if (tiempoEnRojo >= 3f)
            {
                // Cambia la luz a verde
                morir = true;
                luzSpot.color = Color.red;
                scriptADesactivar.enabled = false;
                scriptADesactivar2.enabled = false;
                
            }

            if (objetoAperseguir != null)
            {
                // Calcula la dirección hacia el objeto a perseguir
                Vector3 direccion = objetoAperseguir.position - transform.position;
                direccion.y = 0; // No movemos en el eje Y

                // Normaliza la dirección para que tenga longitud 1
                direccion.Normalize();

                // Calcula el movimiento basado en la dirección y velocidad
                Vector3 movimiento = direccion * velocidad * Time.deltaTime;

                // Calcula la posición después del movimiento
                Vector3 nuevaPosicion = transform.position + movimiento;

                // Limita la posición dentro de los límites establecidos
                nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierdo, limiteDerecho);

                // Aplica la nueva posición al objeto que perseguirá
                transform.position = nuevaPosicion;
            }
        }
        else
        {
            // Reinicia el temporizador si la luz no está en rojo
            tiempoEnRojo = 0f;
        }
    }
}
