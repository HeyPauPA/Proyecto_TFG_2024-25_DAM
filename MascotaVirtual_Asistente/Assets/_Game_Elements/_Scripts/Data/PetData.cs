using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetData 
{
    //LOS DATOS A GUARDAR DE LA MASCOTA

    //necesitamos el momento en el q lo hemos alimentado
    //o dado descanso por ultima vez para guardarlo
    public DateTime ultimaVezAlimentado;
    public DateTime ultimaVezDescansado;

    //ademas de la cantidad de los estados de la mascota en ese momento
    public int alimento;
    public int descanso;
    public int felicidad;

    public PetData(DateTime ultimaVezAlimentado, DateTime ultimaVezDescansado, int alimento, int descanso, int felicidad)
    {
        this.ultimaVezAlimentado = ultimaVezAlimentado;
        this.ultimaVezDescansado = ultimaVezDescansado;
        this.alimento = alimento;
        this.descanso = descanso;
        this.felicidad = felicidad;
    }
}
