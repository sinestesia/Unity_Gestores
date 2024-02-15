using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PointClick_Controlador : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agente;
    Vector3 posDestino;
    Animator anim;

    public EstadosPoint estadoPoint;

    [Range(100f, 250f)]
    public float rayLongitud;

    private void Awake()
    {
        cam = Camera.main;
        agente = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool resultado = Physics.Raycast (ray, out hit, rayLongitud);
            if (resultado)
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                posDestino = hit.point;
                
                CambiarEstadosPoint(EstadosPoint.EnMovimiento);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * rayLongitud, Color.blue);
            }
        }

        Vector3 distanciaObjetivo = posDestino - transform.position;
        if (distanciaObjetivo.magnitude <= 0.125) {
            CambiarEstadosPoint(EstadosPoint.Quieto);
        }

        
    }
    void CambiarEstadosPoint(EstadosPoint nuevoEstado) {
        estadoPoint = nuevoEstado;
       switch (estadoPoint)
        {
            case EstadosPoint.Quieto:
                anim.SetBool("corriendo", false);
                break;
            case EstadosPoint.EnMovimiento:
                anim.SetBool("corriendo", true);
                agente.SetDestination(posDestino);
                break;
        }
    }
}

public enum EstadosPoint { 
    Quieto,
    EnMovimiento
}
