using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Torreta : MonoBehaviour
{
    public EstadosTorreta estadoTorreta;
    public Transform posicionJugador;
    public Transform transformTorreta;
    public Transform origenBalas;
    public Vector3 vectorDireccion;
    public Quaternion rotacionFinal;
    public float velocidadRotacion;
    public Rigidbody balaOriginal;
    public float cadenciaDisparo;
    float temporizador = 0;

    private Quaternion rotacionInicial;
    private void Awake()
    {
        rotacionInicial = transformTorreta.rotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotacionActual = transformTorreta.rotation;
        vectorDireccion = posicionJugador.position - transformTorreta.position;
        vectorDireccion.y = 0;
        vectorDireccion.Normalize();
        rotacionFinal = Quaternion.LookRotation(vectorDireccion);

        //Si torreta está reposicionando y llega a la posición origen se pone en reposo
        if (estadoTorreta == EstadosTorreta.Reposicionando) {
            if (rotacionActual == rotacionInicial)
            {
                CambiarEstadoTorreta(EstadosTorreta.Reposo);
            }
            else {
                transformTorreta.rotation = Quaternion.RotateTowards(rotacionActual, rotacionInicial, velocidadRotacion * Time.deltaTime);
            }
        }

        if (estadoTorreta == EstadosTorreta.Detectado) {
            transformTorreta.rotation = Quaternion.RotateTowards(rotacionActual, rotacionFinal, velocidadRotacion * Time.deltaTime);
            if (rotacionActual == rotacionFinal) {
                CambiarEstadoTorreta(EstadosTorreta.Disparando);
            }

        }

        if (estadoTorreta == EstadosTorreta.Disparando) {
            //Disparar 
            temporizador += Time.deltaTime;
            if (temporizador > cadenciaDisparo) {

                Rigidbody clonBala = Instantiate(balaOriginal, origenBalas.position, rotacionFinal);
                
                Destroy(clonBala.gameObject, 8f);
                clonBala.AddForce(clonBala.transform.forward * 10f, ForceMode.VelocityChange);
                temporizador = 0;
            }
     
            if (rotacionActual != rotacionFinal)
            {
                CambiarEstadoTorreta(EstadosTorreta.Detectado);
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (estadoTorreta == EstadosTorreta.Reposo || estadoTorreta == EstadosTorreta.Reposicionando) {
                CambiarEstadoTorreta(EstadosTorreta.Detectado);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (estadoTorreta == EstadosTorreta.Detectado || estadoTorreta == EstadosTorreta.Disparando)
            {
                CambiarEstadoTorreta(EstadosTorreta.Reposicionando);
            }
        }
    }

    private void CambiarEstadoTorreta(EstadosTorreta nuevoEstadoTorreta) { 
        estadoTorreta = nuevoEstadoTorreta;
    }

}

public enum EstadosTorreta {
    Reposo,
    Detectado,
    Disparando,
    Reposicionando
}