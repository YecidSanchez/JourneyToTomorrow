using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class control : MonoBehaviour
{
    public float runspeed;
    public float caminarSpeed=0.1f;

    Rigidbody myrb;
    Animator myanim;
    bool voltear;
    bool agachar;
    bool subir;
    bool saltar;
    bool push;
    bool Ver_agachar;

    public float tiempoKinematic = 20f;
    private CapsuleCollider capsule;
    public Transform emptyObject;
    public float fuerzaSalto = 10f;
    public float alturaSalto = 1.3f;

    private float jumpTimer = 2f;
    public float maxJumpDuration = 0.5f;
    public float fuerzaEmpuje;
    public Rigidbody objetoAEmpujar;

    public Light spotlight; // Variable para asignar la luz spotlight desde el Inspector
    public Color originalColor;
    public Color color_inte=Color.green;
    public Color color_inte_sale = Color.red;
    public Color newColor = Color.yellow;
    public Material interruptor;
    public static bool mover;
    public static bool alzar;
    public GameObject objetoAMover; // Asigna el objeto que quieres mover desde el Inspector
    public Vector3 nuevaPosicion; // Asigna la nueva posición deseada desde el Inspector
    public GameObject objetoAlQuePegar;
    private Vector3 posicionRelativa;
    private Quaternion rotacionRelativa;
    public GameObject objetoAseguir;
    public bool subir2;
    private bool juegoPausado = false;
    public GameObject imagenPausa;
    public Image imagenDelCanvas;
    public GameObject menu;
    public float tiempoEntreSaltos = 1.0f;
    private float tiempoUltimoSalto;

    private void Start()
    {

        Time.timeScale = 1f;
        myrb = GetComponent<Rigidbody>();
        myanim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();
        voltear = true;
        subir = true;
        saltar = false;
        Ver_agachar = false;
        objetoAEmpujar.isKinematic = false;
        ChangeMaterialEmissionColor(interruptor, color_inte_sale);

        if (myrb == null)
        {
            Debug.LogError("Este objeto no tiene un componente Rigidbody.");
        }
        else
        {
            StartCoroutine(ActivarDesactivarKinematic());
        }

        if (spotlight == null)
        {
            spotlight = GameObject.FindGameObjectWithTag("Spotlight").GetComponent<Light>();
        }

        originalColor = spotlight.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            subir = !subir;
            fuerzaSalto = 0.233F;
            Ver_agachar = false;
            objetoAEmpujar.mass = 1f;
            subir2 = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && saltar==false)
        {
            agachar = !agachar;
            saltar = false;
            Ver_agachar = true;
            RestablecerPadre();
            myanim.SetBool("getup", false);
            objetoAEmpujar.mass = 100f;

        }

        /*if (Input.GetKeyDown(KeyCode.Space) )
        {
            saltar = true;
           StartCoroutine(ResetSaltar());
           // myrb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            jumpTimer = 0f;

        }*/
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > tiempoUltimoSalto + tiempoEntreSaltos)
        {
            saltar = true;
            StartCoroutine(ResetSaltar());
            tiempoUltimoSalto = Time.time;
            jumpTimer = 0f;
        }
        if (subir2==true)
        {

            float nuevaPosY = 0.06262043f; // Modifica este valor según tus necesidades
            capsule.center = new Vector3(capsule.center.x, nuevaPosY, capsule.center.z);

            // Cambiar la altura
            float nuevaAltura = 0.1326532f; // Modifica este valor según tus necesidades
            capsule.height = nuevaAltura;
            objetoAEmpujar.mass = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Cambiar el estado de pausa
            juegoPausado = !juegoPausado;

            // Llamar a la función para gestionar la pausa
            GestionarPausa();
        }
        
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        myanim.SetFloat("speed", Mathf.Abs(move));

        float sneaking = Input.GetKey("s") ? 1f : 0f;
        myanim.SetFloat("sneaking", sneaking);

        if (sneaking > 0 )
        {
            caminarSpeed = 0.05F;
            myrb.velocity = new Vector3(move * caminarSpeed, myrb.velocity.y, 0);
            subir = false;
            runspeed = 0.1f;
            CambiarPropiedadesCollider();
            fuerzaSalto = 0F;
            objetoAEmpujar.isKinematic = true;
            objetoAEmpujar.mass = 100f;

        }
        else
        {
            myrb.velocity = new Vector3(move * runspeed, myrb.velocity.y, 0);
            agachar = false;
            runspeed = 0.4f;
            CambiarPropiedadesCollider();
            

        }

        if (move > 0 && !voltear && myrb.isKinematic==false)
        {
            Flip();
        }
        else if (move < 0 && voltear && myrb.isKinematic == false)
        {
            Flip();
        }
        myanim.SetBool("subir", subir);
        myanim.SetBool("agachar", agachar);
        myanim.SetBool("saltar", saltar);
        myanim.SetBool("push",push );
        if (saltar)
        {
            myrb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);

            jumpTimer += Time.fixedDeltaTime;
            if (jumpTimer >= maxJumpDuration)
            {
                saltar = false;
            }
        }
        if (myrb.position.x>6.8f) { 
        if (Input.GetKey("e")) ;
        {
            myanim.SetBool("push", true);
            EmpujarObjeto();
            objetoAEmpujar.isKinematic = false;
            //CambiarPropiedadesCollider();
        }
        }
        if (!Input.GetKey("e"))
        {
            myanim.SetBool("push", false);
            objetoAEmpujar.isKinematic = true;
            runspeed = 0.4f;
        }
      if (Seguir.morir == true)
        {

            myanim.SetBool("morir", true);
            myrb.isKinematic = true;

            /* if (other.CompareTag("pot"))
              {
                  ChangeSpotlightColor(newColor);
              }*/
        }

    }

    void Flip()
    {
        voltear = !voltear;

        // Cambiar la rotación en lugar de la escala
        transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator ActivarDesactivarKinematic()
    {
        // Desactiva isKinematic
        myrb.isKinematic = true;

        // Espera el tiempo especificado
        yield return new WaitForSeconds(tiempoKinematic);

        // Vuelve a activar isKinematic
        myrb.isKinematic = false;
    }
    void CambiarPropiedadesCollider()
    {
        // Verificar si el CapsuleCollider se encontró correctamente
        if (capsule != null && agachar==true)
        {
            // Cambiar la posición en Y
            float nuevaPosY = 0.04547971f; // Modifica este valor según tus necesidades
            capsule.center = new Vector3(capsule.center.x, nuevaPosY, capsule.center.z); 

            // Cambiar la altura
            float nuevaAltura = 0.09837173f; // Modifica este valor según tus necesidades
            capsule.height = nuevaAltura;
        }
        else if (capsule != null && subir == true)
        {
            float nuevaPosY = 0.06262043f; // Modifica este valor según tus necesidades
            capsule.center = new Vector3(capsule.center.x, nuevaPosY, capsule.center.z);

            // Cambiar la altura
            float nuevaAltura = 0.1326532f; // Modifica este valor según tus necesidades
            capsule.height = nuevaAltura;
        }if
        (capsule != null && push == true)
        {
            float nuevaPosz = 0.01417403f; // Modifica este valor según tus necesidades
            capsule.center = new Vector3(capsule.center.x, capsule.center.y, nuevaPosz);

            // Cambiar la altura
            float radius = 0.02701151f; // Modifica este valor según tus necesidades
            capsule.radius = radius;
        }
    }
    IEnumerator ResetSaltar()
    {
        // Esperar un pequeño tiempo antes de restablecer saltar a false
        yield return new WaitForSeconds(0.8f);
        saltar = false;
        CambiarPropiedadesCollider();
    }
    private void OnAnimatorMove()
    {
        if (saltar)
        {
          //AjustarCapsuleCollider();

            // Incrementa el temporizador de salto
            jumpTimer += Time.deltaTime;

            // Si el temporizador alcanza la duración máxima del salto, detén el salto
            if (jumpTimer >= maxJumpDuration)
            {
                saltar = false;
                jumpTimer = 0f;
            }
        }
    }

    private void AjustarCapsuleCollider()
    {
        // Ajusta la posición del CapsuleCollider según la posición actual de la animación
        capsule.center = new Vector3(emptyObject.localPosition.x, emptyObject.localPosition.y, capsule.center.z);
    }
    void EmpujarObjeto()
    {
        // Obtener la dirección de empuje (puedes ajustar esto según tus necesidades)
        Vector3 direccionEmpuje = transform.forward;

        // Aplicar la fuerza al Rigidbody
        myrb.AddForce(direccionEmpuje * fuerzaEmpuje);
        if
        (push == true)
        {
            float nuevaPosz = 0.01417403f; // Modifica este valor según tus necesidades
            capsule.center = new Vector3(capsule.center.x, capsule.center.y, nuevaPosz);

            // Cambiar la altura
            float radius = 0.02701151f; // Modifica este valor según tus necesidades
            capsule.radius = radius;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interruptor"))
        {
            ChangeMaterialEmissionColor(interruptor, color_inte);
            mover = true;
           // MoveObjectToPosition(objetoAMover, objetoAMover.transform.position);
        }
        if(other.CompareTag("laser"))
        {
            myrb.isKinematic = true;
            myanim.SetBool("morir", true);
            // MoveObjectToPosition(objetoAMover, objetoAMover.transform.position);
            StartCoroutine(ReiniciarDespuesDeEspera(4f));
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("iman"))
        {
            // Verificar si se asignó un objeto a seguir en x
            if (objetoAseguir != null)
            {
                // Establecer el objeto actual como hijo del objeto a seguir
                transform.SetParent(objetoAseguir.transform);

                Debug.Log("Objeto se ha pegado al objeto a seguir.");
                Debug.Log("Posición actual: " + transform.position);
                Debug.Log("Rotación actual: " + transform.rotation.eulerAngles);

                myrb.isKinematic = true;
                myanim.SetBool("getup", true);
                subir2 = true;
                // Invocar el método para restablecer el padre después de 2 segundos (puedes ajustar el tiempo según tus necesidades)

            }
            else
            {
                Debug.LogWarning("Objeto a seguir no asignado. Asigne un objeto en el Inspector.");
            }
        }
    }
   
    void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale de la zona es la luz spotlight
        if (other.CompareTag("interruptor"))
        {
            mover = false;
            ChangeMaterialEmissionColor(interruptor, color_inte_sale);
        }
       
    }

    void ChangeSpotlightColor(Color color)
    {
        spotlight.color = color;
    }
    void ChangeMaterialEmissionColor(Material material, Color color)
    {
        material.SetColor("_EmissionColor", color);
    }
    void MoveObjectToPosition(GameObject objeto, Vector3 posicion)
    {
        objeto.transform.position = posicion;
    }
    void RestablecerPadre()
    {
        // Restablecer el padre del objeto a su estado anterior
      
        myrb.isKinematic = false;
        transform.SetParent(null);
        // myrb.gravityScale = 1.0f;
        // Puedes realizar otras acciones después de restablecer el padre si es necesario
    }

    void GestionarPausa()
    {
        // Si el juego está pausado, detener el tiempo
        if (juegoPausado)
        {
            Time.timeScale = 0f; // Esto establece el tiempo a cero, pausando el juego
                                 // Aquí puedes agregar lógica adicional de pausa, como mostrar un menú de pausa, etc.
            imagenPausa.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Esto restablece el tiempo a su velocidad normal, reanudando el juego
            imagenPausa.SetActive(false);
            // Aquí puedes agregar lógica adicional para reanudar el juego
        }
        
    }
    IEnumerator ReiniciarDespuesDeEspera(float tiempoEspera)
    {
        myrb.isKinematic = true;
        //myanim.SetTrigger("morir"); // Configura el trigger "morir"

        // Espera durante el tiempo especificado
        yield return new WaitForSeconds(tiempoEspera);

        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

