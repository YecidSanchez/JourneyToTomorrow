using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abrir_puerta : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool subir_puerta;
    public Color color_inte;
    public Color color_intea=Color.red;
    public Color color_inte_sale;
    public Material puerta;
    void Start()
    {
        //puerta.color = Color.red;
        ChangeMaterialEmissionColor(puerta, color_inte);
    }

    // Update is called once per frame
    void Update()
    {
       
        // puerta.color = Color.yellow;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("puerta"))
        {
            ChangeMaterialEmissionColor(puerta, color_inte_sale);
           
            subir_puerta = true;
            // MoveObjectToPosition(objetoAMover, objetoAMover.transform.position);
        }

    }
    void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale de la zona es la luz spotlight
        if (other.CompareTag("puerta"))
        {
            ChangeMaterialEmissionColor(puerta, color_inte);
            subir_puerta = false;
            
        }

    }
    void ChangeMaterialEmissionColor(Material material, Color color)
    {
        material.SetColor("_EmissionColor", color);
    }

}
