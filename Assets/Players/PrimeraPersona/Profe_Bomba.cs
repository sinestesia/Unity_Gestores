using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Profe_Bomba : MonoBehaviour
{
    Rigidbody rb;
    float contadorTiempo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (contadorTiempo < 3f) contadorTiempo += Time.deltaTime;
        else
        {
            SimularExplosion();
            Destroy(gameObject);
        }
    }

    void SimularExplosion()
    {
        Vector3 explosionPos = transform.position;
        
        // Almaceno una variable de tipo "array de Colliders" o conjunto de colliders
        // al que asigno los componentes colliders proximos a la bomba en un margen de 2.5 unidades
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 2.5f);

        foreach (Collider hit in colliders)
        {
            // en todos los colliders detectados...
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            // le aplico una fuerza para simular una explosion
            if (rb != null) rb.AddExplosionForce(15f, explosionPos, 2.5f, 0.25f, ForceMode.VelocityChange);
        }
    }
}