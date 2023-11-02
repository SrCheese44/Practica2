using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoCubo : MonoBehaviour
{

    Rigidbody rb;


    private float movX, movZ;
    private bool estaSaltando;
    public float JumpPower;
    public float speed = 15;
    
    private int ContadorMonedas = 0;
    private int MonedasFinal = 7;

    public GameObject NivelEnd;
    public TMP_Text TextoNivelEnd;
    public TMP_Text textoContador;
    private float tiempo;

    public AudioSource Musica;
    public AudioSource FX;
    public AudioSource BounceEffect;

    private bool GameOver = false;
    public GameObject Pausa;

    public GameObject Derrota;
    public TMP_Text TextoDerrota;

    public ParticleSystem CoinShine;

    private bool frozen = false;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump")&& !estaSaltando)
        {
            estaSaltando = true;
            rb.AddForce(rb.velocity.x, JumpPower, rb.velocity.z, ForceMode.Impulse);
        }

        pantallafinal();
        contador();
        ResetNivel();
        MenuPausa();

    }

    
    private void FixedUpdate()
    {
        if (!frozen)
        {
            Vector3 NuevaVelocidad = new Vector3(movX * speed, rb.velocity.y, movZ * speed);
            rb.velocity = NuevaVelocidad;
           
        }
       
    }

    void MenuPausa()
    { //Si no se ha perdido el juego, tener la opción de pausar
        if (!GameOver) 
        { 
            if(Input.GetKeyDown(KeyCode.Escape) && !Pausa.activeSelf)
            {
                Pausa.SetActive(true);
                Time.timeScale = 0;
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && Pausa.activeSelf)
            {
                Pausa.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    void contador()
    {//Contador ascendente
        tiempo += Time.deltaTime;
        textoContador.text = tiempo.ToString("#0.00");
    }
   
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("moneda"))
        {
            ContadorMonedas++;
            Destroy(col.gameObject);
            FX.Play();
            CoinShine.Play();
            CoinShine.transform.position = transform.position;
        }
        if (col.gameObject.CompareTag("EasterEgg"))
        {
            NivelEnd.SetActive(true);
            Time.timeScale = 0;
            TextoNivelEnd.text = "Has encontrado la verdad del universo.";
            Musica.Stop();
        }

        if (col.gameObject.CompareTag("BoostVelocity"))
        {
            rb.Sleep();
            frozen = true;
            rb.AddForce(Vector3.forward * 20, ForceMode.Impulse);
        }
        //este item ralentizará el tiempo
        if (col.gameObject.CompareTag("Slowtime"))
        {
            
            Destroy(col.gameObject);
            Time.timeScale = 0.3f;
           
        }
        if (col.gameObject.CompareTag("fastTime"))
        {
            Destroy(col.gameObject);
            Time.timeScale = 2;
            
        }
        if (col.gameObject.CompareTag("Antigravedad"))
        {
            rb.useGravity = false;
        }
       
    }
    private void OnTriggerExit(Collider col)
    {

        if (col.gameObject.CompareTag("Antigravedad"))
        {
            rb.useGravity = true;
        }




    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemigo"))
        {
           
            Time.timeScale = 0;
            Derrota.SetActive(true);
            TextoDerrota.text = "Intentálo de nuevo";
            Musica.Stop();
        }

        if (col.gameObject.CompareTag("Elastico"))
        {
            rb.AddForce(Vector3.up * 11, ForceMode.Impulse);
            BounceEffect.Play();    
            
        }
        if (col.gameObject.CompareTag("Antigravedadown"))
        {
            rb.AddForce(Vector3.down * 2, ForceMode.Impulse);
        }


    }
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            estaSaltando = false; 
            frozen = false;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            estaSaltando = true;
        }
    }

    void ResetNivel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            Time.timeScale = 1;
            
        }
    }
    void pantallafinal()
    {
        if (ContadorMonedas == MonedasFinal)
        {
            NivelEnd.SetActive(true);
            Time.timeScale = 0;
            mensajefinal();
            Musica.Stop();

        }
    }
    void mensajefinal()
    {

        if(tiempo > 15)
        {
            TextoNivelEnd.text = "¡Mejora tu tiempo!";
        }
        else if(tiempo < 7)
        {
            TextoNivelEnd.text = "¡Que rápido!";
        }
        else
        {
         TextoNivelEnd.text = "¡Nivel Completado!";
            
        }
    }

}

    
