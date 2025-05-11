using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetUIController : MonoBehaviour
{
    //variables de las barras de la mascota
    public Image barraComida;
    public Image barraDescanso;
    public Image barraFelicidad;

    public static PetUIController instance;

    private void Awake()
    {
        //Singleton simple
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("Hay mas de un Manager del UI de la mascota en la escena");
    }

    public void CambiarImagenBarras(int alimento, int descanso, int felicidad)
    {
        barraComida.fillAmount = (float) alimento / 100;
        barraDescanso.fillAmount = (float)descanso / 100;
        barraFelicidad.fillAmount = (float)felicidad / 100;
    }
}
