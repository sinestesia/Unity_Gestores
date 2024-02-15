using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SecuenciaSimon : MonoBehaviour
{
    public Estados estado;
    public Transform padreBotones;
    public Button pulsador;

    public Color[] colores;

    Image[] botonesImg;
    Button[] botonesButton; 

    public float contadorTiempo;

    [Header("--- Secuencia ---")]
    public int secuencia_elementoActual;
    public int secuencia_elementosMax;

    public int[] secuencia_correcta;
    public int[] secuencia_player;
    public int secuencia_player_ElementoActual;

    private void Awake()
    {
        InicializarArrays();
    }

    // Start is called before the first frame update
    void Start()
    {
        EstablecerEstado(Estados.pulsadorDisponibe);
    }

    // Update is called once per frame
    void Update()
    {
        if (estado == Estados.calculandoSecuencia)
        {
            if (contadorTiempo < 1f) contadorTiempo += Time.deltaTime;
            else EstablecerEstado(Estados.mostrandoSecuencia);
        }

        if (estado == Estados.mostrandoSecuencia)
        {
            if (contadorTiempo < 1f) contadorTiempo += Time.deltaTime;
            else
            {
                if (secuencia_elementoActual < secuencia_elementosMax)
                {
                    if (secuencia_correcta[secuencia_elementoActual] >= 0)
                    {
                        ColorearBoton(secuencia_correcta[secuencia_elementoActual], false);
                        Debug.Log ("Se desactiva el boton previo: " + secuencia_correcta[secuencia_elementoActual]);
                    }

                    secuencia_elementoActual++;
                    contadorTiempo = 0f;

                    if (secuencia_elementoActual < secuencia_elementosMax)
                    {
                        ColorearBoton(secuencia_correcta[secuencia_elementoActual], true);
                        Debug.Log("Se activa el boton: " + secuencia_correcta[secuencia_elementoActual]);
                    }
                }
                else
                {
                    EstablecerEstado(Estados.esperandoRespuesta);
                }
            }
        }
    }

    void InicializarArrays()
    {
        botonesImg = new Image[padreBotones.childCount];
        botonesButton = new Button[padreBotones.childCount];

        for (int i = 0; i < botonesImg.Length; i++)
        {
            botonesImg[i] = padreBotones.GetChild(i).GetComponent<Image>();
            botonesButton[i] = padreBotones.GetChild(i).GetComponent<Button>();
        }
    }

    public void EstablecerEstado(Estados nuevoEstado)
    {
        estado = nuevoEstado;
        Debug.Log("Estado actual: " + "<color=yellow>" + estado + "</color>");

        switch (estado)
        {
            case Estados.pulsadorDisponibe:

                contadorTiempo = 0f;
                secuencia_elementoActual = 0;
                secuencia_player_ElementoActual = 0;

                pulsador.interactable = true;
                EstablecerInteractividadBotones(false);
                break;
            case Estados.calculandoSecuencia:

                contadorTiempo = 0f;

                for (int i = 0; i < botonesButton.Length; i++)
                {
                    ColorearBoton(i, false);
                }

                pulsador.interactable = false;
                EstablecerInteractividadBotones(false);
                CalcularSecuencia();
                break;
            case Estados.mostrandoSecuencia:

                contadorTiempo = 0f;
                pulsador.interactable = false;
                EstablecerInteractividadBotones(false);
                ColorearBoton(secuencia_correcta[secuencia_elementoActual], true);
                break;
            case Estados.esperandoRespuesta:

                contadorTiempo = 0f;
                pulsador.interactable = false;

                for (int i = 0; i < botonesButton.Length; i++)
                {
                    ColorearBoton(i, true);
                }

                pulsador.interactable = true;
                EstablecerInteractividadBotones(true);
                break;
            case Estados.comprobandoRespuesta:

                contadorTiempo = 0f;
                 
                int _numAciertos = 0;

                for (int i = 0; i < secuencia_player.Length; i++)
                {
                    if (secuencia_player[i] == secuencia_correcta[i]) _numAciertos++;
                }

                if (_numAciertos == secuencia_elementosMax) Debug.Log("CORRECTO! MENUDA MEMORIA");
                else Debug.Log ("INCORRECTO! Numero de aciertos: " +  _numAciertos);

                EstablecerEstado(Estados.pulsadorDisponibe);
                break;
        }
    }

    public void IniciarSecuencia()
    {
        EstablecerEstado(Estados.calculandoSecuencia);
    }

    void CalcularSecuencia()
    {
        secuencia_correcta = new int[secuencia_elementosMax];
        secuencia_player = new int[secuencia_elementosMax];

        for (int i = 0; i < secuencia_correcta.Length; i++)
        {
            secuencia_correcta[i] = Random.Range(0, 4);
        }
    }

    public void SeleccionarBoton(int _index)
    {
        Debug.Log("Boton seleccionado: " + _index);

        if (secuencia_player_ElementoActual < secuencia_player.Length - 1)
        {
            secuencia_player[secuencia_player_ElementoActual] = _index;
            secuencia_player_ElementoActual++;
        }
        else
        {
            EstablecerEstado(Estados.comprobandoRespuesta);
        }
    }

    public void EstablecerInteractividadBotones(bool _estado)
    {
        for (int i = 0; i < padreBotones.childCount; i++)
        {
            botonesButton[i].interactable = _estado;
        }
    }

    public void ColorearBoton (int index, bool estaActivado)
    {
        if (estaActivado) botonesImg[index].color = colores[index];
        else botonesImg[index].color = Color.grey;
    }

    public enum Estados
    {
        pulsadorDisponibe,
        calculandoSecuencia,
        mostrandoSecuencia,
        esperandoRespuesta,
        comprobandoRespuesta
    }
}
