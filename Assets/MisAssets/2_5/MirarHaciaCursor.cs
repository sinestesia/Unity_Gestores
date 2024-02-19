using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MirarHaciaCursor : MonoBehaviour
{
    public static MirarHaciaCursor THIS;

    Animator anim;
    Vector3 puntoMira;

    private void Awake()
    {
        THIS = this;
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        anim.SetLookAtPosition(puntoMira);
        anim.SetLookAtWeight(1f, 0.125f, 1f);
    }

    public void EstablecerPosMira(float _posExtra)
    {
        puntoMira = transform.position + transform.forward + Vector3.up * (1.65f + _posExtra);
    }
}
