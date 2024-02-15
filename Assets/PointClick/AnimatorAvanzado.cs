using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// ATENCION: Este script tiene que ir en el maniquí (objeto de la escena que tiene el componente Animator)
/// 
/// </summary>

public class AnimatorAvanzado : MonoBehaviour
{
    Animator anim;

    [Range(0f, 1f)]
    public float influenciaMirarHacia;

    public bool modoMirar;
    public Transform objetivo;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (modoMirar)
        {
            anim.SetLookAtPosition(objetivo.position);
            anim.SetLookAtWeight(influenciaMirarHacia, 0.125f, 1f);
        }
    }
}
