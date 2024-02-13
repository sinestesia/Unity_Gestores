using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// DESCRIPCION: Gestor encargado de dotar de funcionalidad y definir variables
/// para la interfaz de usuario en el Menu de Inicio de mi videojuego
/// 
/// ¡IMPORTANTE! No va a navegar entre escenas, SOLO va a permanecer en la escena "0"
/// 
/// </summary>

public class MainMenuManager : MonoBehaviour
{
    #region 1) DEFINICION VARIABLES
    public static MainMenuManager instancia;

    public GameObject panel_hall;
    public GameObject panel_confirmarSalida;
    #endregion

    #region 2) FUNCIONES PREDET UNITY
    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        panel_hall.SetActive(true);
        panel_confirmarSalida.SetActive(false);
    }
    #endregion

    #region 3) METODOS ORIGINALES
    #region METODOS DE LA PANTALLA "1_Hall"
    public void Boton_Jugar()
    {
        Debug.Log("Se hace click en JUGAR");

        SoundManager.instancia.Reproducir_Click();
        GameManager.instancia.InformacionTransicion(1, EstadosJuego.Jugando);
        GameManager.instancia.EstablecerEstado(EstadosJuego.Cargando);
    }

    public void Boton_Salir()
    {
        Debug.Log("Se hace click en Salir");

        SoundManager.instancia.Reproducir_Click();
        panel_hall.SetActive(false);
        panel_confirmarSalida.SetActive(true);
    }
    #endregion

    #region METODOS DE LA PANTALLA "2_ConfirmarSalida"
    public void Boton_SalirConfirmar()
    {
        // Sale de la version exportada
        Application.Quit();
    }

    public void Boton_SalirCancelar()
    {
        SoundManager.instancia.Reproducir_Cancel();
        panel_hall.SetActive(true);
        panel_confirmarSalida.SetActive(false);
    }
    #endregion
    #endregion
}
