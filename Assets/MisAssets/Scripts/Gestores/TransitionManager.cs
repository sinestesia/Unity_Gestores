using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instancia;

    Animator anim;

    private void Awake()
    {
        instancia = this;
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void Transicion_SeMuestraEmpieza()
    {
        // se activa el Componente Animator
        anim.enabled = true;

        // se reproduce el clip de animacion incluido en el estado "Mostrar"
        anim.Play("Mostrar", 0, 0f);
    }

    public void Transicion_SeMuestraTermina()
    {
        Debug.Log("ACABA DE TERMINAR LA PRIMERA MITAD");

        //TODO: Aqui ira el cambio de escena
        GameManager.instancia.CargarEscenaDuranteTransicion();
        anim.Play("Ocultar", 0, 0f);
    }

    public void Transicion_SeOcultaTermina()
    {
        GameManager.instancia.EstablecerEstadoDuranteTransicion();

        anim.Rebind();
        anim.enabled = false;
    }
}
