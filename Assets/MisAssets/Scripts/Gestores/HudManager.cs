using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : MonoBehaviour
{
    public static HudManager instancia;

    public Image barraVida;
    public TextMeshProUGUI monedas;

    private void Awake()
    {
        instancia = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Actualizar_Monedas();
        Actualizar_Monedas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Actualizar_BarraVida()
    {
        float _vidaActual = PlayerDataManager.instancia.datosPlayer.vidaActual;
        float _vidaMax = PlayerDataManager.instancia.datosPlayer.vidaMax;

        float _vidaNormalizada = _vidaActual / _vidaMax;
        barraVida.fillAmount = _vidaNormalizada;

    }

    public void Actualizar_Monedas()
    {
        monedas.text = PlayerDataManager.instancia.datosPlayer.monedas.ToString();
    }
}
