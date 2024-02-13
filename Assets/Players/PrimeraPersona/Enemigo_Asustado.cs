using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo_Asustado : MonoBehaviour
{
    public Estados estado;
    public Transform refugio;
    public Transform origen;
    private float temporizador;
    private NavMeshAgent agente;
    private Animator anim;
    public GameObject contenedorAnim;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        //anim = transform.GetChild(0).GetComponent<Animator>();
        anim = contenedorAnim.GetComponent<Animator>();
    }

    private void Start()
    {
        EstablecerEstado(Estados.Quieto);
    }

    void Update()
    {
        
        //Lógica de cambio de estados
        if (estado == Estados.Huye) {
            float distanciaEntreRefugioyEnemigo = Vector3.Distance(refugio.position, transform.position);
            if (distanciaEntreRefugioyEnemigo <= 0.125f) {
                EstablecerEstado(Estados.Espera);
            }
        }
        if (estado == Estados.Vuelve) {
            //TODO: Si llega a punto de origen poner estado Quieto
            float distanciaEntreOrigenyEnemigo = Vector3.Distance(origen.position, transform.position);
            if (distanciaEntreOrigenyEnemigo <= 0.125f)
            {
                EstablecerEstado(Estados.Quieto);
            }
        }
        if (estado == Estados.Espera) {
            temporizador += Time.deltaTime;
            if (temporizador >= 3f) {
                EstablecerEstado(Estados.Vuelve);
                temporizador = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && (estado == Estados.Quieto || estado == Estados.Vuelve)) {
            Debug.Log("Jugador detectado");
            EstablecerEstado(Estados.Huye);
        }
        
    }
    public void EstablecerEstado(Estados nuevoEstado) 
    {
  
        estado = nuevoEstado;

        switch (estado)
        {
            case Estados.Quieto:
                agente.isStopped = true;
                anim.SetBool("corriendo",false);
                break;
            case Estados.Huye:
                agente.SetDestination(refugio.position);
                agente.isStopped = false;
                anim.SetBool("corriendo", true);
                break;
            case Estados.Espera:
                agente.isStopped = true;
                anim.SetBool("corriendo", false);
                break;
            case Estados.Vuelve:
                agente.SetDestination(origen.position);
                agente.isStopped = false;
                anim.SetBool("corriendo", true);
                break;
        }


    }

}

public enum Estados
{
    Quieto,
    Huye,
    Espera,
    Vuelve
}