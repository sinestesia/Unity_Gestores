using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PrincipePersiaController : MonoBehaviour
{
    float ejeHorizontal;    // orientacion de movimiento
    float ultimaOrientacion;// ultima orientacion del personaje  
    Vector3 dirMov;         // dirección movimiento
    Rigidbody rb;           // componente rigidbody de MiPersonaje


    Animator anim;

    public Estados estado;
    public float velMov;    // velocidad de Movimiento
    public float velRot;    // velocidad de Rotacion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }
    private void Start()
    {
        EstablecerEstado(Estados.Quieto);
    }
    void Update()
    {
        EstablecerEjeHorizontal();
        EstablecerDirMovimiento();
        Rotacion();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dirMov * velMov * Time.fixedDeltaTime);
        
    }
    void EstablecerEjeHorizontal() {
        ejeHorizontal = Input.GetAxisRaw("Horizontal");
        if (ejeHorizontal != 0)
        {
            ultimaOrientacion = ejeHorizontal;
            EstablecerEstado(Estados.Corriendo);
        }
        else {
            EstablecerEstado(Estados.Quieto);
        }
    }
    void EstablecerDirMovimiento() { 
        dirMov = Vector3.right * ejeHorizontal;
    }
    void Rotacion()
    {
        if (ultimaOrientacion != 0f)
        {
            Quaternion _rotInicial = transform.rotation;
            Quaternion _rotFinal = Quaternion.Euler(Vector3.up * (180f - 90f * ultimaOrientacion));
            Quaternion _rotGradual = Quaternion.RotateTowards(_rotInicial, _rotFinal, velRot * Time.deltaTime);
            transform.rotation = _rotGradual;
            if (_rotInicial == _rotFinal) {
                ultimaOrientacion = 0f;
            }
        }

    }
    void EstablecerEstado(Estados nuevoEstado)
    {
        estado = nuevoEstado;
        Debug.Log("Estado enemigo asustado: <color=green>" + estado.ToString() + "</color>");

        switch (estado)
        {
            // -------------------------------------------------------
            case Estados.Quieto:
                anim.SetBool("corriendo", false);

                break;
      
  
            // -------------------------------------------------------
            case Estados.Corriendo:
                anim.SetBool("corriendo", true);

                break;
                // -------------------------------------------------------
        }
    }
}

public enum Estados
{
    Quieto,
    Corriendo
}