using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Profe_Enemigo_Torreta : MonoBehaviour
{
    #region 1) DEFINICION VARIABLES
    public Estados estado;
    public float velRotGradual;
    public Transform origen;
    public Light luz;
    public Rigidbody balaOriginal;

    Vector3 dirFrontalInicial;
    float contadorTiempo;
    Transform cannon;
    Transform objetivo;
    SphereCollider colEsfera;
    #endregion

    #region 2) FUNCIONES PREDET. UNITY
    private void Awake()
    {

        cannon = transform.GetChild(1);
        dirFrontalInicial = cannon.forward;

        objetivo = GameObject.FindWithTag("Player").transform;
        colEsfera = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (estado == Estados.ApuntandoPlayer)
        {
            Vector3 _dirHaciaObjetivo = objetivo.position - cannon.position;
            _dirHaciaObjetivo.y = 0f;
            _dirHaciaObjetivo.Normalize();

            float _angulos = Vector3.Angle(cannon.forward, _dirHaciaObjetivo);

            if (_angulos > 0.05f) RotacionGradual_HaciaPlayer();
            else EstablecerEstado(Estados.Disparando);
        }

        if (estado == Estados.RegresandorReposo)
        {
            float _angulos = Vector3.Angle (cannon.forward, dirFrontalInicial);

            if (_angulos > 0.05f) RotacionGradual_HaciaRotInicial();
            else EstablecerEstado(Estados.Reposo);
        }

        if (estado == Estados.Disparando)
        {
            if (contadorTiempo < 0.25f) contadorTiempo += Time.deltaTime;
            else
            {
                Disparar();
                contadorTiempo = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastCannon();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && estado != Estados.ApuntandoPlayer) EstablecerEstado(Estados.ApuntandoPlayer);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && estado != Estados.RegresandorReposo) EstablecerEstado(Estados.RegresandorReposo);
    }
    #endregion

    #region 3) METODOS ORIGINALES
    void RotacionGradual_HaciaPlayer()
    {
        // PASO 1) Calculo la direccion que hay entre el cannon de la torreta y el player
        Vector3 _dirHaciaObjetivo = objetivo.position - cannon.position;

        // Omito la diferencia que hay entre las alturas de los dos objetos
        _dirHaciaObjetivo.y = 0f;

        // Y dejo establecida la longitud del vector en base 1
        _dirHaciaObjetivo.Normalize();

        // Dibujo un rayo en color magenta con la direccion frontal actual del cannon
        Debug.DrawRay(cannon.position, cannon.forward, Color.magenta);

        // Dibujo un rayo en color amarillo con la direccion frontal a la que el cannon acabará rotando
        Debug.DrawRay(cannon.position, _dirHaciaObjetivo, Color.yellow);

        // PASO 2) Defino y almaceno temporalmente las informaciones de rotacion necesarias
        Quaternion _rotActual = cannon.rotation;

        // Defino la variable "Quaternion" donde se calcula info de rotacion para que el cannon acabe rotando
        // orientando su eje z (flecha azul) hacia el player
        Quaternion _rotFinal = Quaternion.LookRotation(_dirHaciaObjetivo);
        
        // Se almacena la informacion de rotacion gradual que deberemos aplicar al cannon
        // sabiendo la rotacion actual y final
        Quaternion _rotGradual = Quaternion.RotateTowards(_rotActual, _rotFinal, velRotGradual * Time.deltaTime);

        // PASO 3) Le aplico al cannon la rotacion gradual almacenada en la variable "_rotGradual"
        cannon.rotation = _rotGradual;
    }

    void RotacionGradual_HaciaRotInicial()
    {
        // PASO 1) Calculo la direccion que hay entre el cannon de la torreta y el player
        Vector3 _dirHaciaObjetivo = dirFrontalInicial;

        // Omito la diferencia que hay entre las alturas de los dos objetos
        _dirHaciaObjetivo.y = 0f;

        // Y dejo establecida la longitud del vector en base 1
        _dirHaciaObjetivo.Normalize();

        // Dibujo un rayo en color magenta con la direccion frontal actual del cannon
        Debug.DrawRay(cannon.position, cannon.forward, Color.magenta);

        // Dibujo un rayo en color amarillo con la direccion frontal a la que el cannon acabará rotando
        Debug.DrawRay(cannon.position, _dirHaciaObjetivo, Color.yellow);

        // PASO 2) Defino y almaceno temporalmente las informaciones de rotacion necesarias
        Quaternion _rotActual = cannon.rotation;

        // Defino la variable "Quaternion" donde se calcula info de rotacion para que el cannon acabe rotando
        // orientando su eje z (flecha azul) hacia el player
        Quaternion _rotFinal = Quaternion.LookRotation(_dirHaciaObjetivo);

        // Se almacena la informacion de rotacion gradual que deberemos aplicar al cannon
        // sabiendo la rotacion actual y final
        Quaternion _rotGradual = Quaternion.RotateTowards(_rotActual, _rotFinal, velRotGradual * 0.125f * Time.deltaTime);

        // PASO 3) Le aplico al cannon la rotacion gradual almacenada en la variable "_rotGradual"
        cannon.rotation = _rotGradual;
    }


    void RaycastCannon()
    {
        Ray ray = new Ray(cannon.position, cannon.forward);
        RaycastHit hit;

        bool resultado = Physics.Raycast(ray, out hit, colEsfera.radius);

        if (resultado)
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);

            if (hit.collider.CompareTag("Player") && estado != Estados.ApuntandoPlayer) EstablecerEstado(Estados.ApuntandoPlayer);
            else if (estado == Estados.Disparando) EstablecerEstado(Estados.RegresandorReposo);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * colEsfera.radius, Color.blue);

            if (estado == Estados.Disparando) EstablecerEstado(Estados.RegresandorReposo);
        }
    }

    void EstablecerEstado (Estados nuevoEstado)
    {
        estado = nuevoEstado;

        switch (estado)
        {
            case Estados.Reposo:

                luz.color = Color.white;
                cannon.rotation = Quaternion.LookRotation(dirFrontalInicial);
                break;
            case Estados.ApuntandoPlayer:

                luz.color = Color.yellow;
                break;
            case Estados.Disparando:

                luz.color = Color.red;
                break;
            case Estados.RegresandorReposo:

                luz.color = Color.white;
                break;
        }
    }

    void Disparar()
    {
        Rigidbody clonBala = Instantiate(balaOriginal, origen.position, origen.rotation);
        clonBala.AddForce(clonBala.transform.forward * 5f, ForceMode.VelocityChange);
        Destroy(clonBala.gameObject, 2f);
    }
    #endregion

    public enum Estados
    {
        Reposo,
        ApuntandoPlayer,
        Disparando,
        RegresandorReposo
    }
}
