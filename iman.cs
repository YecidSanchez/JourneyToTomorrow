using UnityEngine;

public class Iman : MonoBehaviour
{
    public float velocidadX = 5f; // Velocidad de movimiento en el eje x
    public float velocidadY = 0.15f; // Velocidad de movimiento en el eje y
    public float limiteIzquierdo = -5f; // L�mite izquierdo
    public float limiteDerecho = 5f; // L�mite derecho
    //public bool mover = true; // Controla si el objeto debe moverse en el eje y
    private FixedJoint fixedJoint;
    public float subir;

    private int direccionX = 1; // Direcci�n de movimiento en el eje x (1: derecha, -1: izquierda)

    void Update()
    {
        // Calcula el desplazamiento en el eje x en base a la direcci�n actual
        float desplazamientoX = direccionX * velocidadX * Time.deltaTime;

        // Calcula la nueva posici�n en el eje x
        float nuevaPosicionX = transform.position.x + desplazamientoX;

        // Verifica si la nueva posici�n en el eje x est� dentro de los l�mites
        if (nuevaPosicionX < limiteIzquierdo)
        {
            direccionX = 1; // Cambia la direcci�n a la derecha si alcanza el l�mite izquierdo
        }
        else if (nuevaPosicionX > limiteDerecho)
        {
            direccionX = -1; // Cambia la direcci�n a la izquierda si alcanza el l�mite derecho
        }

        // Aplica el desplazamiento en el eje x al objeto
        transform.Translate(Vector3.right * desplazamientoX);

        // Verifica si la variable mover es true y aplica el movimiento en el eje y
        if (control.mover == true)
        {
            // Ajusta directamente la posici�n en el eje Y al valor deseado
            transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
        }
        if (control.mover == false)
        {
            // Ajusta directamente la posici�n en el eje Y al valor deseado
            transform.position = new Vector3(transform.position.x, subir, transform.position.z);
        }
    }
}

