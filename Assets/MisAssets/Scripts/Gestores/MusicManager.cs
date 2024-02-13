using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// DESCRIPCION: Gestiona el hilo musical del juego
/// 
/// </summary>

public class MusicManager : MonoBehaviour
{
    public static MusicManager instancia;
    
    AudioSource audioSource;

    public AudioClip musica_MenuInicio;
    public AudioClip musica_Gameplay;

    private void Awake()
    {
        instancia = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirMusica(int _num)
    {
        if (_num == 0)
        {
            audioSource.clip = musica_MenuInicio;
            audioSource.Play();
        }else if (_num == 1)
        {
            if (audioSource.clip != null)
            {
                if (audioSource.clip != musica_Gameplay)
                {
                    audioSource.clip = musica_Gameplay;
                    audioSource.Play();
                }
                else ReanudarMusica();
            }
           
        }
    }

    public void PausarMusica()
    {
        if (audioSource.isPlaying) audioSource.Pause();
    }

    public void ReanudarMusica()
    {
        if (!audioSource.isPlaying) audioSource.UnPause();
    }

    public void ReiniciarMusica()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.Play();
        }
    }
}
