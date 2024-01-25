using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguir : MonoBehaviour
{
    public MonoBehaviour scriptADesactivar;
    public MonoBehaviour scriptADesactivar2;
    public Transform objetoAperseguir;
    public float velocidad = 5f;
    public float limiteIzquierdo = -5f; // Establece el l�mite izquierdo
    public float limiteDerecho = 5f;   // Establece el l�mite derecho
    public Light luzSpot;               // Referencia a la luz spot
    public static bool morir;
    public static float tiempoEnRojo = 0f;    // Tiempo transcurrido en estado rojo

    void Update()
    {
        // Verifica si la luz spot est� en color rojo
        if (luzSpot != null && luzSpot.color == Color.yellow)
        {
            tiempoEnRojo += Time.deltaTime; // Incrementa el tiempo en rojo
            
            // Contin�a con la l�gica de persecuci�n solo si la luz est� en rojo
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
                // Calcula la direcci�n hacia el objeto a perseguir
                Vector3 direccion = objetoAperseguir.position - transform.position;
                direccion.y = 0; // No movemos en el eje Y

                // Normaliza la direcci�n para que tenga longitud 1
                direccion.Normalize();

                // Calcula el movimiento basado en la direcci�n y velocidad
                Vector3 movimiento = direccion * velocidad * Time.deltaTime;

                // Calcula la posici�n despu�s del movimiento
                Vector3 nuevaPosicion = transform.position + movimiento;

                // Limita la posici�n dentro de los l�mites establecidos
                nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierdo, limiteDerecho);

                // Aplica la nueva posici�n al objeto que perseguir�
                transform.position = nuevaPosicion;
            }
        }
        else
        {
            // Reinicia el temporizador si la luz no est� en rojo
            tiempoEnRojo = 0f;
        }
    }
}
