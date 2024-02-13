using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : MonoBehaviour
{
    public Rigidbody flechaOriginal;
    public Transform origen;

    public float contadorTiempo;
    public float tiempoMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contadorTiempo < tiempoMax) contadorTiempo += Time.deltaTime;
        else
        {
            CrearClon();
            contadorTiempo = 0f;
        }
        
    }

    void CrearClon()
    {
        Rigidbody clonFlecha = Instantiate(flechaOriginal, origen.position, origen.rotation);
        clonFlecha.AddForce(clonFlecha.transform.forward * 10f, ForceMode.VelocityChange);

        Destroy(clonFlecha.gameObject, 2f);
    }
}
