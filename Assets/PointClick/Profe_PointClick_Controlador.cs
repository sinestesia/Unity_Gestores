using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Profe_PointClick_Controlador : MonoBehaviour
{
    // --------------------------------------------------------
    #region 1) DEF. VARIABLES
    public Estados estado;

    [Range(100f, 250f)]
    public float rayLongitud;

    Camera cam;
    NavMeshAgent agente;
    Animator anim;

    Vector3 posDestino;
    #endregion
    // --------------------------------------------------------
    #region 2) FUNCIONES PREDET. UNITY
    private void Awake()
    {
        cam = Camera.main;
        agente = GetComponent<NavMeshAgent>();

        // almacena el componente "Animator" del primer hijo que tenga "MiPersonaje"
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
                Debug.DrawRay (ray.origin, ray.direction * hit.distance, Color.red);

                posDestino = hit.point;
                EstablecerEstado(Estados.EnMovimiento);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * rayLongitud, Color.blue);
            }
        }

        if (estado == Estados.EnMovimiento)
        {
            // Defino dos variables temporales para localizar los dos puntos a checkear
            Vector3 _puntoA = transform.position;
            Vector3 _puntoB = posDestino;

            // Descarto la diferencia de alturas que exista en ambas
            _puntoA.y = 0f;
            _puntoB.y = 0f;

            // Calculo la direccion que hay entre A y B
            Vector3 _direccionAB = _puntoB - _puntoA;

            // Dibujo un rayo en color magenta
            Debug.DrawRay(_puntoA, _direccionAB , Color.magenta);

            // Calculo la distancia entre los dos puntos
            float _distanciaEntrePuntos = Vector3.Distance (_puntoA, _puntoB);

            // Compruebo si "MiPersonaje" esta lo suficientemente proximo al destino
            // para asignarle el estado "Quieto"
            if (_distanciaEntrePuntos < 0.1f) EstablecerEstado(Estados.Quieto);
        }
    }
    #endregion
    // --------------------------------------------------------
    #region 3) METODOS ORIGINALES
    void EstablecerEstado (Estados _nuevoEstado)
    {
        estado = _nuevoEstado;

        switch (estado)
        {
            // -----------------------------------------------
            case Estados.Quieto:

                anim.SetBool("corriendo", false);
                break;
            // -----------------------------------------------
            case Estados.EnMovimiento:
                agente.SetDestination(posDestino);
                anim.SetBool("corriendo", true);
                break;
            // -----------------------------------------------
        }
    }
    #endregion
    // --------------------------------------------------------

    public enum Estados{ Quieto, EnMovimiento }
}