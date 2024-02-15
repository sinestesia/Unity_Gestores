using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Profe_Enemigo_Asustado : MonoBehaviour
{
    // ---------------------------------------------------------------
    #region 1) DEF. VARIABLES
    public Estados estado;

    float contadorTiempo;
    Vector3 posInicial;

    Transform zonaSegura;
    NavMeshAgent agente;
    Animator anim;
    #endregion
    // ---------------------------------------------------------------
    #region 2) FUNCIONES PREDET. UNITY
    private void Awake()
    {
        zonaSegura = GameObject.FindWithTag("ZonaSegura").transform;
        agente = GetComponent<NavMeshAgent>();
        posInicial = transform.position;

        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        EstablecerEstado(Estados.Quieto);
    }

    // Update is called once per frame
    void Update()
    {
        if (estado == Estados.Huye)
        {
            Vector3 puntoA = zonaSegura.position;
            Vector3 puntoB = transform.position;

            puntoA.y = 0f;
            puntoB.y = 0f;

            float distanciaEntreZonaSegura_Y_Enemigo = Vector3.Distance (puntoA, puntoB);
            if (distanciaEntreZonaSegura_Y_Enemigo <= 0.125f) EstablecerEstado(Estados.Espera);
        }

        if (estado == Estados.Espera)
        {
            if (contadorTiempo < 3f) contadorTiempo += Time.deltaTime;
            else EstablecerEstado(Estados.Vuelve);
        }

        if (estado == Estados.Vuelve)
        {
            Vector3 puntoA = posInicial;
            Vector3 puntoB = transform.position;

            puntoA.y = 0f;
            puntoB.y = 0f;

            float distanciaEntrePosInicial_Y_Enemigo = Vector3.Distance(puntoA, puntoB);
            if (distanciaEntrePosInicial_Y_Enemigo <= 0.125f) EstablecerEstado(Estados.Quieto);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (estado == Estados.Quieto || estado == Estados.Vuelve) EstablecerEstado(Estados.Huye);
        }
    }
    #endregion
    // ---------------------------------------------------------------
    #region 3) METODOS ORIGINALES
    void EstablecerEstado (Estados nuevoEstado)
    {
        estado = nuevoEstado;
        Debug.Log("Estado enemigo asustado: <color=green>" + estado.ToString() + "</color>");

        switch (estado)
        {
            // -------------------------------------------------------
            case Estados.Quieto:

                agente.isStopped = true;
                agente.velocity = Vector3.zero;

                contadorTiempo = 0f;

                anim.SetBool("corriendo", false);

                break;
            // -------------------------------------------------------
            case Estados.Huye:

                agente.isStopped = false;
                contadorTiempo = 0f;

                agente.SetDestination(zonaSegura.position);

                anim.SetBool("corriendo", true);
                break;
            // -------------------------------------------------------
            case Estados.Espera:

                agente.isStopped = true;
                agente.velocity = Vector3.zero;
                contadorTiempo = 0f;

                anim.SetBool("corriendo", false);

                break;
            // -------------------------------------------------------
            case Estados.Vuelve:

                agente.isStopped = false;
                contadorTiempo = 0f;

                agente.SetDestination(posInicial);

                anim.SetBool("corriendo", true);

                break;
            // -------------------------------------------------------
        }
    }
    #endregion
    // ---------------------------------------------------------------

    public enum Estados
    {
        Quieto,
        Huye,
        Espera,
        Vuelve
    }
}
