using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MovimientoCubo : MonoBehaviour
{

    Rigidbody rb;


    private float movX, movZ;
    private bool estaSaltando;
    public float JumpPower;
    public float speed = 15;
    
    private int ContadorMonedas = 0;
    private int MonedasFinal = 3;

    public GameObject NivelEnd;
    public TMP_Text TextoNivelEnd;
    public TMP_Text textoContador;
    private float tiempo;

    public AudioSource Musica;
    public AudioSource FX;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump")&& estaSaltando == false)
        {
            estaSaltando = true;
            rb.AddForce(rb.velocity.x, JumpPower, rb.velocity.z, ForceMode.Impulse);
        }

        if(ContadorMonedas == MonedasFinal)
        {
            NivelEnd.SetActive(true);
            Time.timeScale = 0;
            mensajefinal();
            Musica.Stop();
           
        }

        contador();
    }


    private void FixedUpdate()
    {

        Vector3 NuevaVelocidad = new Vector3(movX * speed, rb.velocity.y, movZ * speed);
        rb.velocity = NuevaVelocidad;
       
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

    void mensajefinal()
    {
        TextoNivelEnd.text = "¡Nivel Compleatdo!";
    }




}

    
