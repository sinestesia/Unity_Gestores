using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// DESCRIPCION: Funcionamiento sencillo de IA. Al pulsar una tecla
/// localiza a Player navegando por la superficie
/// 
/// </summary>

public class Profe_Enemigo_Simple : MonoBehaviour
{
    // --------------------------------------------------------
    #region 1) DEFINICION VARIABLES
    // para poder usar la clase "NavMeshAgent" hay que incluir
    // la libreria "using UnityEngine.AI;"
    NavMeshAgent agente;

    public Transform objetivo;

    public bool siguiendo;
    public float tiempoSiguiendo;

    #endregion
    // --------------------------------------------------------
    #region 2) FUNCIONES PREDET. UNITY
    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        objetivo = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            siguiendo = !siguiendo;

            // "si siguiendo equivale a false..."
            if (!siguiendo) tiempoSiguiendo = 0f;
        }
        
        if (siguiendo) agente.SetDestination(objetivo.position);

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
    #endregion
    // --------------------------------------------------------
    #region 3) METODOS ORIGINALES

    #endregion
    // --------------------------------------------------------
}
