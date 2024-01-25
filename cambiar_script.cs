using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiar_script : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite spriteNormal;
    public Sprite spriteHover;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteNormal; // Establece el sprite inicial
    }

    void OnMouseEnter()
    {
        spriteRenderer.sprite = spriteHover; // Cambia el sprite cuando el ratón entra
    }

    void OnMouseExit()
    {
        spriteRenderer.sprite = spriteNormal; // Restaura el sprite cuando el ratón sale
    }
}
