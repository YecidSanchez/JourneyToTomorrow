using UnityEngine;

public class movDron : MonoBehaviour
{

    public Light luzSpot;
    public GameObject objetoAseguir;

    private float tiempoEnRojo = 0f;
    private float tiempoSiguiendo = 0f;
    private Color colorRojo = Color.yellow;
    private Color colorVerde = Color.red;

    public float velocidadMaxima = 5f;
    public float suavidad = 0.1f;
    public float limiteIzquierdo = -5f;
    public float limiteDerecho = 5f;
    public float tiempoMinRojo = 0.5f;
    public float tiempoSeguimiento = 4f;
    public float factorProporcional = 2f;  // Ajusta este valor para controlar la velocidad de seguimiento

    private float velocidadActual;

    void FixedUpdate()
    {
        MoverAleatoriamente();
        VerificarCambioColor();
    }

    void MoverAleatoriamente()
    {
        float movimientoHorizontal = Random.Range(-1f, 1f);
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0f, 0f);

        velocidadActual = Mathf.SmoothDamp(velocidadActual, movimientoHorizontal * velocidadMaxima, ref velocidadActual, suavidad);

        transform.Translate(Vector3.right * velocidadActual * Time.fixedDeltaTime);

        float posicionX = Mathf.Clamp(transform.position.x, limiteIzquierdo, limiteDerecho);
        transform.position = new Vector3(posicionX, transform.position.y, transform.position.z);
    }

    void VerificarCambioColor()
    {
        if (luzSpot.color == colorRojo)
        {
            tiempoEnRojo += Time.fixedDeltaTime;

            if (tiempoEnRojo >= tiempoMinRojo)
            {
                CambiarColorLuz(colorVerde);

                if (tiempoSiguiendo < tiempoSeguimiento)
                {
                    SeguirObjeto();
                    tiempoSiguiendo += Time.fixedDeltaTime;
                }
                else
                {
                    tiempoSiguiendo = 0f;
                }
            }
        }
        else
        {
            tiempoEnRojo = 0f;
            tiempoSiguiendo = 0f;
        }
    }

    void CambiarColorLuz(Color nuevoColor)
    {
        luzSpot.color = nuevoColor;
    }

    void SeguirObjeto()
    {
        if (objetoAseguir != null)
        {
            // Calcular la dirección hacia el objeto a seguir
            Vector3 direccion = (objetoAseguir.transform.position - transform.position).normalized;

            // Calcular la velocidad proporcional al factorProporcional y la distancia al objeto
            float velocidadProporcional = Mathf.Clamp(Vector3.Distance(transform.position, objetoAseguir.transform.position) * factorProporcional, 0f, velocidadMaxima);

            // Mover el objeto hacia el objeto a seguir con la velocidad proporcional
            transform.Translate(direccion * velocidadProporcional * Time.fixedDeltaTime);
        }
    }
}
