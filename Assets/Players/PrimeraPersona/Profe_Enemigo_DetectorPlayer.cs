using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Profe_Enemigo_DetectorPlayer : MonoBehaviour
{
    public float longitudRayo;
    Transform ojos;

    NavMeshAgent agente;

    public Transform objetivo;

    public bool siguiendo;
    public float tiempoSiguiendo;


    private void Awake()
    {
        ojos = transform.GetChild(1);
        agente = GetComponent<NavMeshAgent>();
        objetivo = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (siguiendo)
        {
            if (tiempoSiguiendo < 0.125f) tiempoSiguiendo += Time.deltaTime;
            else
            {
                agente.SetDestination(objetivo.position);
                tiempoSiguiendo = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        RayoFrontal();
    }

    void RayoFrontal()
    {
        Ray rayo = new Ray(ojos.position, transform.forward); // falta definir ORIGEN y DIRECCION
        RaycastHit hit;

        bool resultado = Physics.Raycast(rayo, out hit, longitudRayo);

        // "SI EL RAYO DETECTA ALGO..."
        if (resultado)
        {

            // "Y ESE ALGO TIENE LA ETIQUETA ASIGNADA PLAYER..."
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(rayo.origin, rayo.direction * hit.distance, Color.red);
                siguiendo = true;
            }
            // "ESE ALGO NO TIENE LA ETIQUETA ASIGNADA PLAYER..."
            else
            {
                Debug.DrawRay(rayo.origin, rayo.direction * hit.distance, Color.yellow);
                siguiendo = false;
            }


        }
        // "SI EL RAYO NO HA DETECTADO NADA..."
        else
        {
            Debug.DrawRay (rayo.origin, rayo.direction * longitudRayo, Color.green);
            siguiendo = false;
        }
    }
}
