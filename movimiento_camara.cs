using UnityEngine;

public class movimiento_camara : MonoBehaviour
{
    public Transform objetivo;   // El objeto que seguir� la c�mara
    public Vector3 offset = new Vector3(0f, 2f, -1.21f);   // Offset de la c�mara con respecto al objetivo
    public float suavizado = 5f;   // Velocidad de suavizado

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Calcula la posici�n relativa sin seguir al objetivo en los ejes Y y Z
            Vector3 nuevaPosicion = new Vector3(objetivo.position.x + offset.x, transform.position.y, offset.z);

            // Mueve suavemente la c�mara hacia la nueva posici�n
            transform.position = Vector3.Lerp(transform.position, nuevaPosicion, suavizado * Time.deltaTime);
        }
    }
}
