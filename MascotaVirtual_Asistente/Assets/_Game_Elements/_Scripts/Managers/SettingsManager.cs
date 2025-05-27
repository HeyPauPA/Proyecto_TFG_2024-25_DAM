using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    //VOLUMEN
    public Slider sliderVolumen;
    public float valorSliderSonido;
    public Image imagenSolidoApagado;
    public Image imagenConSonido;

    //BRILLO
    public Slider sliderBrillo;
    public float valorSliderBrillo;
    public Image panelBrillo;
    void Start()
    {
        //La primera vez que abramos la aplicacion los valores de los sliders
        //seran los guardados en el playerporefs por defecto 
        sliderVolumen.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        sliderBrillo.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        
        AudioListener.volume = sliderVolumen.value;
        revisarSiSonidoApagado();

        //el valor de alpha sera el del slider
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderBrillo.value);
    }

    //------ METODOS PARA EL SONIDO ------
    /// <summary>
    /// METODO PARA CAMBIAR EL VOLUMEN DEL SONIDO SEGUN EL VALOR DEL SLIDER DE ESTE Y GUARDARLO EN EL PLAYERPREFS
    /// </summary>
    public void CambioValorSliderSonido(float valor)
    {
        valorSliderSonido = valor;

        // ahora guardamos en el playerprefs el valor puesto por el usuario
        PlayerPrefs.SetFloat("volumenAudio", valorSliderSonido);
       
        //El volumen del audio corresponde con el del slider
        AudioListener.volume = sliderVolumen.value; 
        revisarSiSonidoApagado();
    }
    /// <summary>
    /// METODO PARA CAMBIAR LAS IMAGENES SI EL SONIDO ESTA ACTIVO O DESACTIVO
    /// </summary>
    private void revisarSiSonidoApagado()
    {
        if(valorSliderSonido == 0)
        {
            imagenSolidoApagado.enabled = true;
            imagenConSonido.enabled = false;
        }
        else
        {
            imagenSolidoApagado.enabled = false;
            imagenConSonido.enabled = true;
        }
    }

    //------ METODOS PARA EL BRILLO ------
    /// <summary>
    /// METODO PARA CAMBIAR EL BRILLO DE LA APP SEGUN EL VALOR DEL SLIDER DE ESTE Y GUARDARLO EN EL PLAYERPREFS
    /// </summary>
    public void cambioValorSliderBrillo(float valor)
    {
        valorSliderBrillo = valor;

        // ahora guardamos en el playerprefs el valor puesto por el usuario
        PlayerPrefs.SetFloat("brillo", valorSliderBrillo);

        //El volumen del audio corresponde con el del slider
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderBrillo.value);
    }
}
