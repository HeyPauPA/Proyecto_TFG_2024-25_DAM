using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    private Database database;
    public NeedsController needsController;

    private void Awake()
    {
        database = new Database();

        //Singleton simple
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("Hay mas de un DBManager en la escena");
    }

    private void Start()
    {
        PetData datosMascota = CargarMascota();
        if (datosMascota != null)
        {
            needsController.Inicializar(
                datosMascota.alimento,
                datosMascota.descanso,
                datosMascota.felicidad,
                10, 10, 10,
                DateTime.Parse(datosMascota.ultimaVezAlimentado),
                DateTime.Parse(datosMascota.ultimaVezDescansado)
                );
        }

        Debug.Log(DateTime.Parse(datosMascota.ultimaVezAlimentado).Hour);
        
    }

    private void Update()
    {
        //Si ha pasado una hora en el juego
        if (TimingManager.timerHoraJuego < 0) 
        {
            //guardamos los datos de la mascota
            PetData datosMascota = new PetData
                ( 
                    needsController.ultimaVezAlimentado.ToString(),
                    needsController.ultimaVezDescansado.ToString(),
                    needsController.alimento,
                    needsController.descanso,
                    needsController.felicidad
                );
            GuardarMascota(datosMascota);
        }
    }

    //METODO PARA GUARDAR LOS DATOS DE LA MASCOTA
    public void GuardarMascota(PetData datosMascota)
    {
        //llamamos al metodo del database
        database.GuardarDatos("informacionMascota", datosMascota);
    }

    //METODO PARA CARGAR LOS DATOS DE LA MASCOTA
    public PetData CargarMascota()
    {
        //creamos una mascota a devolver (vacia)
        PetData datosMascotaADevolver = null;

        //cargamos los datos de la BBDD
        database.CargarDatos<PetData>("informacionMascota", (datosMascota) =>
        {
            datosMascotaADevolver = datosMascota;//Y los guardamos en el dato
        });
        //devolvemos el dato lleno
        return datosMascotaADevolver;
    }
}
