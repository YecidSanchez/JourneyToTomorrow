using UnityEngine;

public class Iman : MonoBehaviour
{
    public float velocidadX = 5f; // Velocidad de movimiento en el eje x
    public float velocidadY = 0.15f; // Velocidad de movimiento en el eje y
    public float limiteIzquierdo = -5f; // Límite izquierdo
    public float limiteDerecho = 5f; // Límite derecho
    //public bool mover = true; // Controla si el objeto debe moverse en el eje y
    private FixedJoint fixedJoint;
    public float subir;

    private int direccionX = 1; // Dirección de movimiento en el eje x (1: derecha, -1: izquierda)

    void Update()
    {
        // Calcula el desplazamiento en el eje x en base a la dirección actual
        float desplazamientoX = direccionX * velocidadX * Time.deltaTime;

        // Calcula la nueva posición en el eje x
        float nuevaPosicionX = transform.position.x + desplazamientoX;

        // Verifica si la nueva posición en el eje x está dentro de los límites
        if (nuevaPosicionX < limiteIzquierdo)
        {
            direccionX = 1; // Cambia la dirección a la derecha si alcanza el límite izquierdo
        }
        else if (nuevaPosicionX > limiteDerecho)
        {
            direccionX = -1; // Cambia la dirección a la izquierda si alcanza el límite derecho
        }

        // Aplica el desplazamiento en el eje x al objeto
        transform.Translate(Vector3.right * desplazamientoX);

        // Verifica si la variable mover es true y aplica el movimiento en el eje y
        if (control.mover == true)
        {
            // Ajusta directamente la posición en el eje Y al valor deseado
            transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
        }
        if (control.mover == false)
        {
            // Ajusta directamente la posición en el eje Y al valor deseado
            transform.position = new Vector3(transform.position.x, subir, transform.position.z);
        }
    }
}

