using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    #region 1) DEFINICION VARIABLES
    public static PauseManager instancia;

    public GameObject menu_Hall;
    public GameObject menu_ConfirmarReiniciar;
    public GameObject menu_ConfirmarSalir;
    #endregion

    #region 2) FUNCIONES PREDET. UNITY
    void Awake()
    {
        instancia = this;        
    }

    // Start is called before the first frame update
    void Start()
    {
        OcultarTodosPaneles();
    }

    #endregion

    #region 3) METODOS ORIGINALES
    public void MostrarPanel_Hall()
    {
        menu_Hall.SetActive(true);
        menu_ConfirmarReiniciar.SetActive(false);
        menu_ConfirmarSalir.SetActive(false);
    }

    public void MostrarPanel_ConfirmarReiniciar()
    {
        menu_Hall.SetActive(false);
        menu_ConfirmarReiniciar.SetActive(true);
        menu_ConfirmarSalir.SetActive(false);
    }

    public void MostrarPanel_ConfirmarSalir()
    {
        menu_Hall.SetActive(false);
        menu_ConfirmarReiniciar.SetActive(false);
        menu_ConfirmarSalir.SetActive(true);
    }

    public void OcultarTodosPaneles()
    {
        menu_Hall.SetActive(false);
        menu_ConfirmarReiniciar.SetActive(false);
        menu_ConfirmarSalir.SetActive(false);
    }

    public void BotonHall_Reanudar()
    {
        OcultarTodosPaneles();
        GameManager.instancia.EstablecerEstado(EstadosJuego.Jugando);
    }

    public void BotonHall_Reiniciar()
    {
        MostrarPanel_ConfirmarReiniciar();
    }

    public void BotonHall_Salir()
    {
        MostrarPanel_ConfirmarSalir();
    }

    public void Boton_ConfirmarReiniciar_Confirmar()
    {
        MusicManager.instancia.ReiniciarMusica();
        GameManager.instancia.InformacionTransicion(1, EstadosJuego.Jugando);
        GameManager.instancia.EstablecerEstado(EstadosJuego.Cargando);
    }

    public void Boton_ConfirmarReiniciar_Cancelar()
    {
        MostrarPanel_Hall();
    }

    public void Boton_ConfirmarSalir_Confirmar()
    {
        GameManager.instancia.InformacionTransicion(0, EstadosJuego.MenuInicio);
        GameManager.instancia.EstablecerEstado(EstadosJuego.Cargando);
    }

    public void Boton_ConfirmarSalir_Cancelar()
    {
        MostrarPanel_Hall();
    }

    #endregion
}
