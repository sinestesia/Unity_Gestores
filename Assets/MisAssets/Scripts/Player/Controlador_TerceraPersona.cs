using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Controlador_TerceraPersona : MonoBehaviour
{
    public Movimiento movimiento;

    public float velTraslacion;
    public float velRotacion;

    public bool estaAgachado;

    [Header ("=== VARIABLES DE BLOQUEO PUERTAS")]
    public bool enZonaBloqueo;
    public GameObject bloqueo;

    Vector2 ejesVirtuales;
    Vector3 dirMovimiento;

    Transform cam;
    Rigidbody rb;

    private void Awake()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EstablecerMovimiento(movimiento);        
    }

    public bool rotacionSuavizada;

    // Update is called once per frame
    void Update()
    {
        EstablecerEjesVirtuales();
        EstablecerDirMovimiento();
        ActualizarEstadoMovimiento();

        if (ejesVirtuales.magnitude != 0f)
        {
            Debug.DrawRay(transform.position, dirMovimiento, Color.black);

            if (rotacionSuavizada)
            {
                // ROTACION GRADUAL
                Quaternion rotFinal = Quaternion.LookRotation(dirMovimiento);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotFinal, velRotacion * Time.deltaTime);
            }
            else
            {
                // ROTACION DIRECTA O BRUSCA
                transform.LookAt(transform.position + dirMovimiento);
            }
        }

        if (enZonaBloqueo && Input.GetKeyDown(KeyCode.E)) InteractuarConBloqueo();

        if (Input.GetKeyDown(KeyCode.Space)) Saltar();
        if (Input.GetKeyDown(KeyCode.LeftShift))Agacharse();
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dirMovimiento * velTraslacion * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Flechas"))
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.zero;

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bloqueos"))
        {
            enZonaBloqueo = true;
            bloqueo = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bloqueos"))
        {
            enZonaBloqueo = false;
            bloqueo = null;
        }
    }

    void EstablecerMovimiento (Movimiento nuevoEstado)
    {
        movimiento = nuevoEstado;

        switch (movimiento)
        {
            case Movimiento.Quieto:
                break;
            case Movimiento.Caminar:
                break;
        }
    }

    void EstablecerEjesVirtuales()
    {
        ejesVirtuales.x = Input.GetAxisRaw("Horizontal");
        ejesVirtuales.y = Input.GetAxisRaw("Vertical");
    }

    void ActualizarEstadoMovimiento()
    {
        if (ejesVirtuales.magnitude == 0f && movimiento == Movimiento.Caminar)
        {
            EstablecerMovimiento(Movimiento.Quieto);
        }
        else if (ejesVirtuales.magnitude != 0f && movimiento == Movimiento.Quieto)
        {
            EstablecerMovimiento(Movimiento.Caminar);
        }
    }

    void EstablecerDirMovimiento()
    {
        dirMovimiento = cam.right * ejesVirtuales.x + cam.forward * ejesVirtuales.y;
        dirMovimiento.y = 0f;
        dirMovimiento.Normalize();
    }

    void InteractuarConBloqueo()
    {
        bloqueo.SetActive(false);
        bloqueo = null;

        enZonaBloqueo = false;
    }

    void Saltar()
    {
        rb.velocity = Vector3.zero;
        
        float _fuerzaSalto = Mathf.Sqrt(1.5f * -2f * Physics.gravity.y);
        rb.AddForce(Vector3.up * _fuerzaSalto, ForceMode.VelocityChange);
    }

    void Agacharse()
    {
        estaAgachado = !estaAgachado;
        
        if (estaAgachado) velTraslacion = 1f;
        else velTraslacion = 3f;
    }

    public enum Movimiento
    {
        Quieto,
        Caminar
    }
}
