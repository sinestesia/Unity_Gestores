using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profe_Lanzador_Granada : MonoBehaviour
{
    public Rigidbody granadaOriginal;
    public float fuerzaLanzamiento;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) LanzarBomba();
    }

    void LanzarBomba()
    {
        Rigidbody _clonGranada = Instantiate(granadaOriginal, transform.position, transform.rotation);
        _clonGranada.AddForce(transform.forward * fuerzaLanzamiento + Vector3.up, ForceMode.VelocityChange);
    }
}
