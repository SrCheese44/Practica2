using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{
    public GameObject Personaje;
    public float offsetZ, offsetY;
    private bool TP = true;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (TP)
            {
                TP = false;
            }
            else
            {
                TP = true;
            }
         

        }
        if (TP)
        {
            transform.position = new Vector3(Personaje.transform.position.x, Personaje.transform.position.y + 1, Personaje.transform.position.z - 7);
        }
        else
        {
            transform.position = Personaje.transform.position;
        }
    }

}