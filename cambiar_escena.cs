using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class cambiar_escena : MonoBehaviour
{
    public static bool continuar;
    public GameObject imagenPausa;
    public Image imagenDelCanvas;
    // Este método se llama cuando se hace clic en el botón
    public void CambiarAEscena(string scene_name)
    {
        SceneManager.LoadScene(scene_name); // Cambia a la escena con el nombre especificado
    }
     public void SalirDeLaAplicacion()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void continuarjuego()
    {
        Time.timeScale = 1f; // Esto restablece el tiempo a su velocidad normal, reanudando el juego
        imagenPausa.SetActive(false);
    }
    public void controles()
    {
        imagenDelCanvas.gameObject.SetActive(true);

    }
    public void controles_salir()
    {
        imagenDelCanvas.gameObject.SetActive(false);

    }
}
