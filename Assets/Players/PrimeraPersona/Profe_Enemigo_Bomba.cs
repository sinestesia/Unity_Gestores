using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Profe_Enemigo_Bomba : MonoBehaviour
{
    public Estados estado;

    NavMeshAgent agente;
    Animator anim;
    Transform objetivo;

    float contadorTiempo;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        objetivo = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        EstablecerEstado(Estados.Quieto);
    }

    // Update is called once per frame
    void Update()
    {
        if (estado == Estados.PlayerDetectado)
        {
            Vector3 puntoA = objetivo.position;
            Vector3 puntoB = transform.position;

            puntoA.y = 0f;
            puntoB.y = 0f;

            float distanciaEntreObjetivo_Y_Enemigo = Vector3.Distance(puntoA, puntoB);
            if (distanciaEntreObjetivo_Y_Enemigo <= 1.25f) EstablecerEstado(Estados.Detonando);
        }

        if (estado == Estados.Detonando)
        {
            if (contadorTiempo <= 3f) contadorTiempo += Time.deltaTime;
            else EstablecerEstado(Estados.Detonado);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (estado == Estados.Quieto) EstablecerEstado(Estados.PlayerDetectado);
        }
    }

     void EstablecerEstado (Estados nuevoEstado)
    {
        estado = nuevoEstado;

        switch (estado)
        {
            case Estados.Quieto:

                agente.isStopped = true;
                agente.velocity = Vector3.zero;
                anim.SetBool("corriendo", false);

                break;
            case Estados.PlayerDetectado:

                agente.isStopped = false;
                anim.SetBool("corriendo", true);

                agente.SetDestination(objetivo.position);

                break;
            case Estados.Detonando:

                agente.isStopped = true;
                agente.velocity = Vector3.zero;
                anim.SetBool("corriendo", false);
                break;
            case Estados.Detonado:

                agente.isStopped = false;
                agente.velocity = Vector3.zero;

                contadorTiempo = 0f;

                gameObject.SetActive(false);
                break;
        }
    }

    public enum Estados
    {
        Quieto,
        PlayerDetectado,
        Detonando,
        Detonado
    }
}
