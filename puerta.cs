using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f;
    public Material bombillo;
    public Color color_intea = Color.red;
    public Color color_inte_sale=Color.green;
    // Start is called before the first frame update
    void Start()
    {
        ChangeMaterialEmissionColor(bombillo, color_intea);
    }

    // Update is called once per frame
    void Update()
    {
        
            if (abrir_puerta.subir_puerta == true)
            {
                // Interpola suavemente entre la posición actual y la posición deseada
                float newY = Mathf.Lerp(transform.position.y, 0.29f, Time.deltaTime * velocidadMovimiento);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                ChangeMaterialEmissionColor(bombillo, color_inte_sale);
        }
            if (abrir_puerta.subir_puerta == false)
            {
                // Interpola suavemente entre la posición actual y la posición deseada
                float newY = Mathf.Lerp(transform.position.y, 0.094f, Time.deltaTime * velocidadMovimiento);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                ChangeMaterialEmissionColor(bombillo, color_intea);
        }
    }
    void ChangeMaterialEmissionColor(Material material, Color color)
    {
        material.SetColor("_EmissionColor", color);
    }
}
