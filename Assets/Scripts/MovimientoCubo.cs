using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCubo : MonoBehaviour
{

    Rigidbody rb;
    private float movX, movZ;
    private bool haSaltado = false;
    public float speed = 15;
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            haSaltado = true;

        }
    }


    private void FixedUpdate()
    {

        Vector3 NuevaVelocidad = new Vector3(movX * speed, rb.velocity.y, movZ * speed);
        rb.velocity = NuevaVelocidad;
        /*
        if (haSaltado)
        {

        }
        */
    }
}

    
