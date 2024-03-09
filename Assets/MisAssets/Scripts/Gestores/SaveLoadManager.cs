using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
///
/// DESCRIPCION:
///
/// </summary>

public class SaveLoadManager : MonoBehaviour
{

    // -----------------------------------------------------------------
    #region 1) Definicion de Variables
    public static SaveLoadManager instancia;

    string ruta;
    #endregion
// -----------------------------------------------------------------
#region 2) Funciones Predeterminadas de Unity 
void Awake (){
        
        instancia = this;
        ruta = Application.persistentDataPath;
        Debug.Log("Ruta del archivo guardado: " + ruta);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) GuardarPartida();
        if (Input.GetKeyDown(KeyCode.H)) CargarPartida();
    }

    void GuardarPartida()
    {
        // Actualiza datos del player
        PlayerDataManager.instancia.datosPlayer.posicion = Controlador_TerceraPersona.instancia.transform.position;
        PlayerDataManager.instancia.datosPlayer.rotacion = Controlador_TerceraPersona.instancia.transform.rotation;

        // Actualiza datos de la camara del Player
        Vector2 _valorCam = Controlador_TerceraPersona.instancia.AccederValoresCamaraVirtualActiva();
        PlayerDataManager.instancia.datosPlayer.rotCamH = _valorCam.x;
        PlayerDataManager.instancia.datosPlayer.rotCamV = _valorCam.y;

        DatosPlayer _datosPlayer = PlayerDataManager.instancia.datosPlayer;
        string _contenido = JsonUtility.ToJson(_datosPlayer);

        File.WriteAllText(ruta + "/Save01.json", _contenido);
    }

    void CargarPartida()
    {
        string rutaConArchivo = ruta + "/Save01.json";

        bool archivoExistente = File.Exists(rutaConArchivo);
        Debug.Log("Resultado archivo existente: " + archivoExistente);

        // Does the file exist?
        if (archivoExistente)
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(rutaConArchivo);
            // Work with JSON
            Debug.Log("Contenido de archivo encontrado:\n " + fileContents);

            DatosPlayer _datosPlayerCargados = JsonUtility.FromJson<DatosPlayer>(fileContents);
            PlayerDataManager.instancia.datosPlayer = _datosPlayerCargados;
        }
    }
#endregion
// -----------------------------------------------------------------
#region 3) Metodos Originales

#endregion
// -----------------------------------------------------------------

}
