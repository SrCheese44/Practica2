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
    private int MonedasFinal = 5;

    public GameObject NivelEnd;
    public TMP_Text TextoNivelEnd;
    public TMP_Text textoContador;
    private float tiempo;

    public AudioSource Musica;
    public AudioSource FX;

    private bool GameOver = false;
    public GameObject Pausa;

    public GameObject Derrota;
    public TMP_Text TextoDerrota;
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

        Vector3 NuevaVelocidad = new Vector3(movX * speed, rb.velocity.y, movZ * speed);
        rb.velocity = NuevaVelocidad;
       
    }

    void MenuPausa()
    {
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
    {
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
        }

    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemigo"))
        {
            Destroy(rb);
            Time.timeScale = 0;
            Derrota.SetActive(true);
            TextoDerrota.text = "LOSER";
            Musica.Stop();
        }
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            estaSaltando = false; 
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
        TextoNivelEnd.text = "¡Nivel Completado!";
    }

}

    
