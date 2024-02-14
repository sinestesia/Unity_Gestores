using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Profe_Enemigo_Estados : MonoBehaviour
{
    public Estados estado;


    [Range(1f, 10f)] public float rayoLongitud;

    NavMeshAgent agente;
    Transform objetivo;

    Vector3 posInicial;
    Quaternion rotInicial;

    public float contadorTiempo;
    Animator anim;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        objetivo = GameObject.FindWithTag("Player").transform;
        anim = transform.GetChild(0).GetComponent<Animator>();

        posInicial = transform.position;
        rotInicial = transform.rotation;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (estado == Estados.PlayerPerdido) EsperaTrasPerdida();
        if (estado == Estados.PlayerDetectado) SiguiendoPlayer();
        if (estado == Estados.Volviendo)
        {
            Vector3 _dirPosInicialHastaEnemigo = posInicial - transform.position;
            Debug.DrawRay(transform.position, _dirPosInicialHastaEnemigo, Color.magenta);

            if (_dirPosInicialHastaEnemigo.magnitude < 0.125f) EstablecerEstado(Estados.Quieto);
        }


    }

    private void FixedUpdate()
    {
        RayoFrontal();
    }

    void EstablecerEstado (Estados _nuevoEstado)
    {
        estado = _nuevoEstado;
        Debug.Log("Estado enemigo: <color=yellow> " + estado.ToString() + " </color>");

        switch (estado)
        {
            // --------------------------------------------
            case Estados.Quieto:

                agente.isStopped = true;
                agente.stoppingDistance = 1.25f;
                transform.rotation = rotInicial;
                anim.SetBool("corriendo", false);
                break;
            // --------------------------------------------
            case Estados.PlayerDetectado:

                agente.isStopped = false;
                agente.stoppingDistance = 1.25f;

                contadorTiempo = 0f;
                anim.SetBool("corriendo", true);
                break;
            // --------------------------------------------
            case Estados.PlayerPerdido:

                agente.stoppingDistance = 0f;
                contadorTiempo = 0f;

                anim.SetBool("corriendo", false);
                break;
            // --------------------------------------------
            case Estados.Volviendo:

                agente.stoppingDistance = 0f;

                contadorTiempo = 0f;
                agente.SetDestination(posInicial);

                anim.SetBool("corriendo", true);
                break;
            // --------------------------------------------
        }
    }

    void RayoFrontal()
    {
        Ray rayo = new Ray(transform.position + Vector3.up * 1.5f, transform.forward); // falta definir ORIGEN y DIRECCION
        RaycastHit hit;

        bool resultado = Physics.Raycast(rayo, out hit, rayoLongitud);

        // "SI EL RAYO DETECTA ALGO..."
        if (resultado)
        {

            // "Y ESE ALGO TIENE LA ETIQUETA ASIGNADA PLAYER..."
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(rayo.origin, rayo.direction * hit.distance, Color.red);

                if (estado == Estados.Quieto) EstablecerEstado(Estados.PlayerDetectado);
            }
            // "ESE ALGO NO TIENE LA ETIQUETA ASIGNADA PLAYER..."
            else
            {
                Debug.DrawRay(rayo.origin, rayo.direction * hit.distance, Color.yellow);

                if (estado == Estados.PlayerDetectado) EstablecerEstado(Estados.PlayerPerdido);
            }


        }
        // "SI EL RAYO NO HA DETECTADO NADA..."
        else
        {
            Debug.DrawRay(rayo.origin, rayo.direction * rayoLongitud, Color.green);

            if (estado == Estados.PlayerDetectado) EstablecerEstado(Estados.PlayerPerdido);
        }
    }

    void SiguiendoPlayer()
    {
        if (contadorTiempo < 0.125f) contadorTiempo += Time.deltaTime;
        else
        {
            agente.SetDestination(objetivo.position);
            contadorTiempo = 0f;
        }
    }

    void EsperaTrasPerdida()
    {
        if (contadorTiempo < 5f) contadorTiempo += Time.deltaTime;
        else
        {
            EstablecerEstado(Estados.Volviendo);
            contadorTiempo = 0f;
        }
    }


    public enum Estados
    {
        Quieto,
        PlayerDetectado,
        PlayerPerdido,
        PlayerAtacado,
        Volviendo
    }
}
