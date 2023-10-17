using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform PuntoRotar; 
  
    void Update()
    {
        transform.RotateAround(PuntoRotar.position, Vector3.up, 100*Time.deltaTime);
    }
}
