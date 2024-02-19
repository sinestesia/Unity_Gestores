using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// ATENCION: Este script tiene que ir en el maniquí (objeto de la escena que tiene el componente Animator)
/// 
/// </summary>

public class AnimatorAvanzado : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agente;

    [Range(0f, 1f)]
    public float influenciaMirarHacia;

    public Transform objetivo;

    private void Awake()
    {
        agente = transform.parent.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        anim.SetLookAtPosition(objetivo.position);
        anim.SetLookAtWeight(influenciaMirarHacia, 0.125f, 1f);
        
    }
}
