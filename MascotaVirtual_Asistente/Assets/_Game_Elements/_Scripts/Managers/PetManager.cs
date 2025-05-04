using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public PetController mascota;
    public float timerMovimientoMascota;
    public float timerMovimientoInicial;

    public static PetManager instancia;

    //Array para los puntos de ruta
    public Transform[] waypoints;

    private void Awake()
    {
        timerMovimientoInicial = timerMovimientoMascota;

        //Singleton simple
        //con esto nos aseguramos de q solo hay un pet manager accesible en la escena
        //*de esta manera no puedes llamar al petmanager en otro awake
        if (instancia == null)
        {
            instancia = this;
        }
        else Debug.LogWarning("Hay mas de un PetManager en la escena");
    }

    void Update()
    {
        //si el timer es mayor a 0
        if (timerMovimientoMascota > 0)
        {
            //cada frame el timer se reducira el timer
            timerMovimientoMascota = timerMovimientoMascota - Time.deltaTime;
        }
        else //si el mayor
        {
            //nos movemos
            MoverAWaypointAleatorio();
            //reseteamos
            timerMovimientoMascota = timerMovimientoInicial;
        }
    }

    private void MoverAWaypointAleatorio()
    {
        //generamos un numero aleatorio (con el tamanio del array de ountos de ruta)
        int puntoAleatorio = Random.Range(0, waypoints.Length);

        //llamamos al scrip de la mascota (el objeto padre) y realizamos el metodo de movimiento
        //al q le pasamos la posicion del objeto del array
        mascota.MovimientoAleatorio(waypoints[puntoAleatorio].position);
    }
}
