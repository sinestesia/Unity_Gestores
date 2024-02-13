using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // libreria donde esta definida la clase SceneManager.

/// <summary>
/// 
/// DESCRIPCION: Gestor de gestores. Define y gestiona todos los posibles
/// estados, fases o etapas por las que pasará mi videojuego en algún momento
/// de la simulación. 
/// 
/// Tambien es el encargado de dar directrices o hacer llamadas a otros
/// gestores complementarios.
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    // --------------------------------------------------------------------------
    #region 1) DEFINICION DE VARIABLES
    public static GameManager instancia;
    public EstadosJuego estadoActual;

    [Header ("VARIABLES USADAS EN TRANSICIONES")]
    // se almacena el valor de la escena que se carga al terminar la primera mitad del fundido a negro
    public int escenaCargadaTrasTransicion;
    // se almacena el estado actual que tendra el GameManager al terminar la segunda mitad del fundido a negro
    public EstadosJuego estadoJuegoTrasTransicion; 
    #endregion
    // --------------------------------------------------------------------------
    #region 2) FUNCIONES PREDETERMINADAS UNITY
    void Awake()
    {
        Debug.Log("Se ejecuta el Awake()");
        #region USO DEL PATRON SINGLETON
        // Sirve para asegurarnos de que en el momento en el que nuestro GameManager
        // se "crea" navegará conmigo entre escenas
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);

            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Se ejecuta el Start()");
        EstablecerEstadoInicial();
    }

    // Update is called once per frame
    void Update()
    {
        NavegacionEntreEscenas();
        EstablecerEstadoPausado();
    }
    #endregion
    // --------------------------------------------------------------------------
    #region 3) METODOS ORIGINALES CREADOS POR MI
    /// <summary>
    /// DESCRIPCION: Asigna un estado nuevo y ejecuta el conjunto de instrucciones asociadas
    /// al estado escogido
    /// </summary>
    /// <param name="nuevoEstado">Nuevo estado que va a quedar asignado en el GameManager</param>
    public void EstablecerEstado(EstadosJuego nuevoEstado)
    {
        estadoActual = nuevoEstado;
        Debug.Log("Estado GameManager : <color=green> " + estadoActual.ToString() + "</color>");

        switch (estadoActual)
        {
            case EstadosJuego.MenuInicio:

                Time.timeScale = 1f;
                MusicManager.instancia.ReproducirMusica(0);
                break;
            case EstadosJuego.Cargando:

                Time.timeScale = 1f;
                TransitionManager.instancia.Transicion_SeMuestraEmpieza();

                if (estadoJuegoTrasTransicion == EstadosJuego.Jugando)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else if (estadoJuegoTrasTransicion == EstadosJuego.MenuInicio)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                break;
            // ----------------------------------------------------------------------
            case EstadosJuego.Jugando:

                Time.timeScale = 1f; // Se escala la velocidad de la simul a 1f (valo predet.)

                Cursor.lockState = CursorLockMode.Locked; // El cursor se bloquea
                Cursor.visible = false; // El cursor se oculta

                MusicManager.instancia.ReproducirMusica(1);
                PauseManager.instancia.OcultarTodosPaneles();
                break;
            // ----------------------------------------------------------------------
            case EstadosJuego.JuegoPausado:

                Time.timeScale = 0f; // Se escala la velocidad de la simul a 0f

                Cursor.lockState = CursorLockMode.None; // El cursor se desbloquea
                Cursor.visible = true; // El cursor se muestra

                MusicManager.instancia.PausarMusica();
                PauseManager.instancia.MostrarPanel_Hall();
                break;
            // ----------------------------------------------------------------------
            case EstadosJuego.FinJuego:
                
                break;
            case EstadosJuego.NivelCompletado:
                
                break;
            case EstadosJuego.Creditos:
                
                break;
        }
    }

    /// <summary>
    /// DESCRIPCION: Asigna el estado correcto del GameManager segun la escena
    /// desde la que se ejecuta el script como componente
    /// </summary>
    void EstablecerEstadoInicial()
    {
        int _escena = SceneManager.GetActiveScene().buildIndex;

        if (_escena == 0) EstablecerEstado(EstadosJuego.MenuInicio);
        if (_escena == 1) EstablecerEstado(EstadosJuego.Jugando);
    }

    /// <summary>
    /// 
    /// DESCRIPCION: Analiza el estado actual de "jugando" o "juegoPausado"
    /// y asigna el estado contrario
    /// 
    /// </summary>
    void EstablecerEstadoPausado()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // "si estoy jugando..."
            if (estadoActual == EstadosJuego.Jugando)
            {
                // ...pausamos el juego"
                EstablecerEstado(EstadosJuego.JuegoPausado);
            }

            // ".. en caso de que no este jugando, si esta el juego pausado..."
            else if (estadoActual == EstadosJuego.JuegoPausado)
            {
                /// ... volvemos a estar en estado jugando"
                EstablecerEstado(EstadosJuego.Jugando);
            }
        }
    }

    /// <summary>
    /// DESCRIPCION: Navega entre las escenas "MenuInicial" y "Gameplay" del proyecto
    /// y actualiza la variable "estadoActual" al presionar la tecla P
    /// </summary>
    void NavegacionEntreEscenas()
    {
        // si presiono la tecla ENTER del teclado...
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (estadoActual == EstadosJuego.MenuInicio)
            {
                SceneManager.LoadScene(1);
                EstablecerEstado(EstadosJuego.Jugando);
            }
            else if (estadoActual == EstadosJuego.JuegoPausado)
            {
                InformacionTransicion(0, EstadosJuego.MenuInicio);
                EstablecerEstado(EstadosJuego.Cargando);
            }
        }
    }

    public void Cargando_Jugar()
    {
        SceneManager.LoadScene(1);
        EstablecerEstado(EstadosJuego.Jugando);
    }

    public void Cargando_VolverMenuInicio()
    {
        SceneManager.LoadScene(0);
        EstablecerEstado(EstadosJuego.MenuInicio);
    }

    public void InformacionTransicion(int _escena, EstadosJuego _estado)
    {
        escenaCargadaTrasTransicion = _escena;
        estadoJuegoTrasTransicion = _estado;
    }

    public void CargarEscenaDuranteTransicion()
    {
        SceneManager.LoadScene(escenaCargadaTrasTransicion);
    }

    public void EstablecerEstadoDuranteTransicion()
    {
        EstablecerEstado(estadoJuegoTrasTransicion);

    }
    #endregion
    // --------------------------------------------------------------------------
}
// ------------------------------------------------------------------------------

public enum EstadosJuego
{
    MenuInicio,
    Cargando,
    Jugando,
    JuegoPausado,
    FinJuego,
    NivelCompletado,
    Creditos
}