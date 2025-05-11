using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    //[CONTROLADOR DE LAS NECESIDADES DE LA MASCOTA]
    //NECESIDADES van de 0-100
    public int alimento;
    public int descanso;
    public int felicidad;

    //REDUCCION DE NECESIDADES
    public int reduccionAlimento;
    public int reduccionDescanso;
    public int reduccionFelicidad;

    //MINIMO PARA EMPEZAR A REDUCIR FELICIDAD
    public int mindesc_ali;

    //datos de ultimas veces realizado alguna accion
    public DateTime ultimaVezAlimentado;
    public DateTime ultimaVezDescansado;

    //Contructor
    public void Inicializar(int alimento, int descanso, int felicidad, int reduccionAlimento, int reduccionDescanso, int reduccionFelicidad)
    {
        this.alimento = alimento;
        this.descanso = descanso;
        this.felicidad = felicidad;
        this.reduccionAlimento = reduccionAlimento;
        this.reduccionDescanso = reduccionDescanso;
        this.reduccionFelicidad = reduccionFelicidad;
        //para las ultimas veces
        ultimaVezAlimentado = DateTime.Now;
        ultimaVezDescansado = DateTime.Now;

        //actualizacion de barras
        PetUIController.instance.CambiarImagenBarras(alimento, descanso, felicidad);
    }
    //contructor segundo
    public void Inicializar(int alimento, int descanso, int felicidad, int reduccionAlimento, 
        int reduccionDescanso, int reduccionFelicidad, DateTime ultimaVezAlimentado, DateTime ultimaVezDescansado)
    {
        this.alimento = alimento;
        this.descanso = descanso;
        this.felicidad = felicidad;
        this.reduccionAlimento = reduccionAlimento;
        this.reduccionDescanso = reduccionDescanso;
        this.reduccionFelicidad = reduccionFelicidad;

        //para las ultimas veces
        this.ultimaVezAlimentado = ultimaVezAlimentado;
        this.ultimaVezDescansado = ultimaVezDescansado;

        //actualizacion de barras
        PetUIController.instance.CambiarImagenBarras(alimento, descanso, felicidad);
    }

    private void Awake()
    {
        Inicializar(100, 100, 100, 10, 10, 10);
    }
    private void Update()
    {
        //si pasa una hora del juego
        if(TimingManager.timerHoraJuego < 0)
        {
            //las necesidades (menos la felicidad) se reducen
            CambioNivelAlimento(-reduccionAlimento);
            CambioNivelDescanso(-reduccionDescanso);

            //en caso de q el alimento y el descanso esten elevados al pasar la hora
            if (alimento > 75 && descanso > 75)
            {
                //la felicidad empieza a elevar
                CambioNivelFelicidad(+10);
            } 
        }
        //actualizacion de barras
        PetUIController.instance.CambiarImagenBarras(alimento, descanso, felicidad);
    }

    //---------- METODOS PARA LAS NECESIDADES ----------
    public void CambioNivelAlimento(int cantidad)
    {
        alimento += cantidad;

        //cuando aumentamos guardamos el dato del momento de alimentacion
        if (cantidad > 0) 
        {
            ultimaVezAlimentado = DateTime.Now;
        }
       
        //si el alimento esta a menos de un
        //25 por ciento empieza a bajar la felicidad
        if (alimento < mindesc_ali)
        {
            CambioNivelFelicidad(-reduccionFelicidad);
        }

        else if (alimento > 100) //para q no nos pasemos de nivel [POR ARRIBA] 
        {
            alimento = 100;
        }
        else if (alimento < 0) //para q no nos pasemos de nivel[POR ABAJO]
        {
            alimento = 0;
        }
    }
    public void CambioNivelDescanso(int cantidad)
    {
        descanso += cantidad;
        
        //cuando aumentamos guardamos el dato del momento del descanso
        if (cantidad > 0) 
        {
            ultimaVezDescansado = DateTime.Now;
        }

        //si el descanso esta a menos de un
        //25 por ciento empieza a bajar la felicidad
        if (descanso < mindesc_ali)
        {
            CambioNivelFelicidad(-reduccionFelicidad);
        }

        else if (descanso > 100) //para q no nos pasemos de nivel [POR ARRIBA] 
        {
            descanso = 100;
        }
        else if (descanso < 0) //para q no nos pasemos de nivel[POR ABAJO]
        {
            descanso = 0;
        }
    }
    public void CambioNivelFelicidad(int cantidad)
    {
        felicidad += cantidad;

        if (felicidad <= 0)
        {
            felicidad = 0; // no podemos reducir mas q 0
        }
        else if (felicidad > 100) //para q no nos pasemos de nivel [POR ARRIBA] 
        {
            felicidad = 100;
        }
        else if (felicidad < 0) //para q no nos pasemos de nivel[POR ABAJO]
        {
            felicidad = 0;
        }

    }

}
