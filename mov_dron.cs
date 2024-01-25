using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mov_dron : MonoBehaviour
{
    public List<Light> spotlights; // Lista de luces para asignar desde el Inspector
    public Color originalColor;
    public Color newColor = Color.yellow;

    void Start()
    {
        // Si no se han asignado luces desde el Inspector, intenta encontrarlas automáticamente
        if (spotlights == null || spotlights.Count == 0)
        {
            // Encuentra todas las luces con la etiqueta "Spotlight" en la escena
            Light[] foundSpotlights = GameObject.FindObjectsOfType<Light>();
            spotlights = new List<Light>(foundSpotlights.Length);

            foreach (Light foundSpotlight in foundSpotlights)
            {
                if (foundSpotlight.CompareTag("Spotlight"))
                {
                    spotlights.Add(foundSpotlight);
                }
            }
        }

        originalColor = spotlights[0].color; // Tomamos el color de la primera luz por defecto
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en la zona es el dron
        if (other.CompareTag("pot"))
        {
            ChangeSpotlightColors(newColor);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale de la zona es el dron
        if (other.CompareTag("pot"))
        {
            ChangeSpotlightColors(originalColor);
        }
    }

    void ChangeSpotlightColors(Color color)
    {
        foreach (Light spotlight in spotlights)
        {
            spotlight.color = color;
        }
    }
}
