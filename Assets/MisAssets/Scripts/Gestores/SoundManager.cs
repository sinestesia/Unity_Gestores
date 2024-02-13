using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 
/// DESCRIPCION: Gestor encargado de reproducir efectos de sonido.
/// 
/// </summary>

public class SoundManager : MonoBehaviour
{
    // --------------------------------------------------------------------------
    #region 1) DEFINICION VARIABLES
    public static SoundManager instancia;
    AudioSource audioSource;

    public AudioClip click; // almacena la pista de audio de "hacer click" en boton
    public AudioClip cancel; // almacena la pista de audio al "regresar" de un menu

    #endregion
    // --------------------------------------------------------------------------
    #region 2) FUNCIONES PREDETERMINADAS UNITY
    private void Awake()
    {
        instancia = this;
        audioSource = GetComponent<AudioSource>();        
    }
    #endregion
    // --------------------------------------------------------------------------
    #region 3) METODOS ORIGINALES
    /// <summary>
    /// DESCRIPCION: Reproduce la pista de audio asignada en la variable "click" a traves
    /// del Gestor de Sonido o AudioSource
    /// </summary>
    public void Reproducir_Click()
    {
        audioSource.PlayOneShot(click);
    }

    public void Reproducir_Cancel()
    {
        audioSource.PlayOneShot(cancel);
    }
    #endregion
    // --------------------------------------------------------------------------
}
