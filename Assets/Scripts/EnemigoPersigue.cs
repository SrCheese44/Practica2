using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemigoPersigue : MonoBehaviour
{

    public GameObject Personaje;
    public GameObject Enemigo;
    public float speed;

    void Start()
    {
        
    }

   
    void Update()
    {
        Enemigo.transform.position = Vector3.MoveTowards(Enemigo.transform.position, Personaje.transform.position, speed*Time.deltaTime);
        transform.LookAt(Personaje.transform.position);
    }
}
