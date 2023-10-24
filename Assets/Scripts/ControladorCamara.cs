using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{
    public GameObject Personaje;
    public float offsetZ, offsetY;
    private bool TerceraPersona = true;
    [Header("camara 1 persona")]
    public float sensX;
    public float sensY;
    private float movX, movY;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (TerceraPersona)
            {
                TerceraPersona = false;
            }
            else
            {
                TerceraPersona = true;
            }
         

        }
        if (TerceraPersona)
        {
            transform.position = new Vector3(Personaje.transform.position.x, Personaje.transform.position.y + 1, Personaje.transform.position.z - 7);
        }
        else
        {
           transform.position = Personaje.transform.position;
        }
    }

}
