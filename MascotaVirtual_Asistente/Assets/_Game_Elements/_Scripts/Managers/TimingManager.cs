using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public static float timerHoraJuego; //hora dentro del juego
    public float duracionHora;//la duracion de esta hora (para pruebas)

    private void Update()
    {
        if (timerHoraJuego <= 0)
        {
            timerHoraJuego = duracionHora;
        }
        else
        {
            timerHoraJuego = timerHoraJuego - Time.deltaTime;
        }
    }
}
