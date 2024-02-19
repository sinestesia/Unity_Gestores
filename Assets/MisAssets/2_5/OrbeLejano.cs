using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class OrbeLejano : MonoBehaviour
{
    public Estados estado;

    Transform objetivo;



    private void Awake()
    {
        objetivo = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (estado == Estados.Viajando)
        {
            Vector3 dirHaciaPlayer = objetivo.position - transform.GetChild(0).position + Vector3.up * 1.5f;
            Debug.DrawRay(transform.GetChild(0).position, dirHaciaPlayer);

            transform.Translate(dirHaciaPlayer * Time.deltaTime, Space.Self);
        }
    }

    private void OnMouseDown()
    {
        if (estado == Estados.Reposo) EstablecerEstado(Estados.Viajando);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player") && estado == Estados.Viajando)
        {
            EstablecerEstado(Estados.Recolectado);
        }
    }

    void EstablecerEstado (Estados nuevoEstado)
    {
        estado = nuevoEstado;

        switch (estado)
        {
            case Estados.Reposo:
                break;
            case Estados.Viajando:
                break;
            case Estados.Recolectado:

                gameObject.SetActive(false);
                break;
        }
    }

    public enum Estados
    {
        Reposo,
        Viajando,
        Recolectado
    }
}
