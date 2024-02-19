using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipePersiaControlador : MonoBehaviour
{
    // --------------------------------------------------------------
    #region 1) DEFINICION VARIABLES
    float ejeHorizontal;    // eje virtual Horizontal
    float ejeVertical;      // eje virtual Vertical

    Vector3 dirMov;         // direccion de Movimiento
    Rigidbody rb;           // componente Rigidbody de "MiPersonaje"

    public float velMov;    // velocidad de Movimiento
    public float velRot;    // velocidad de Rotacion
    public bool rotHaciaCamara;

    Animator anim;
    Camera cam;

    public bool rotando;
    float ejeHorizontalUsado;

    #endregion
    // --------------------------------------------------------------
    #region 2) FUNCIONES PREDET UNITY
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        EstablecerEjeHorizontal();
        EstablecerDirMovimiento();

        RotacionNueva();
    }

    private void FixedUpdate()
    {
        // desplazamiento del personaje sincronizado con simulacion de fisicas
        rb.MovePosition(rb.position + dirMov * velMov * Time.fixedDeltaTime);
    }
    #endregion
    // --------------------------------------------------------------
    #region 3) METODOS ORIGINALES
    void EstablecerEjeHorizontal()
    {
        ejeHorizontal = Input.GetAxisRaw("Horizontal");
        ejeVertical = Input.GetAxisRaw("Vertical");

        // ACTUALIZA EL ANIMATOR EN EL PARAMETRO "corriendo"
        if (ejeHorizontal == 0f) anim.SetBool("corriendo", false);
        else anim.SetBool("corriendo", true);


        MirarHaciaCursor.THIS.EstablecerPosMira(ejeVertical);
    }

    void EstablecerDirMovimiento()        
    {
        dirMov = Vector3.right * ejeHorizontal;
    }


    // VERSION ANTIGUA
    void Rotacion()
    {
        if (ejeHorizontal != 0f)
        {
            Quaternion _rotInicial = transform.rotation;
            Quaternion _rotFinal = Quaternion.identity; 

            if (rotHaciaCamara) _rotFinal = Quaternion.Euler(Vector3.up * (180f - 90f * ejeHorizontal)); // rot de frente
            else _rotFinal = Quaternion.Euler(Vector3.up * (90f * ejeHorizontal)); // rot de espaldas

            Quaternion _rotGradual = Quaternion.RotateTowards(_rotInicial, _rotFinal, velRot * Time.deltaTime);            
            transform.rotation = _rotGradual;
        }
    }

    // VERSION NUEVA
    void RotacionNueva()
    {
        if (ejeHorizontal != 0f)
        {
            rotando = true;
            ejeHorizontalUsado = ejeHorizontal;
        }

        if (rotando)
        {
            Quaternion _rotInicial = transform.rotation;
            Quaternion _rotFinal = Quaternion.identity;

            Vector3 _dirRotacion = Vector3.zero;

            if (rotHaciaCamara)
            {
                _dirRotacion = Vector3.up * (180f - 90f * ejeHorizontalUsado);
                _rotFinal = Quaternion.Euler(_dirRotacion); // rot de frente
            }
            else
            {
                _dirRotacion = Vector3.up * (90f * ejeHorizontalUsado);
                _rotFinal = Quaternion.Euler(_dirRotacion); // rot de espaldas
                Debug.DrawRay(transform.position, _dirRotacion, Color.yellow);
            }

            Quaternion _rotGradual = Quaternion.RotateTowards(_rotInicial, _rotFinal, velRot * Time.deltaTime);
            transform.rotation = _rotGradual;

            Debug.DrawRay(transform.position, transform.forward, Color.cyan);
            Debug.DrawRay(transform.position, Vector3.right * ejeHorizontalUsado, Color.black);

            float angulos = Vector3.Angle(transform.forward, Vector3.right * ejeHorizontalUsado);
            Debug.Log("Angulos = " + angulos);
            
            if (angulos == 0f) rotando = false;
        }
    }
    #endregion
    // --------------------------------------------------------------
}
