using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PetData 
{
    //[DATOS A GUARDAR DE LA MASCOTA]
    //necesitamos el momento en el q lo hemos alimentado
    //o dado descanso por ultima vez para guardarlo
    public string ultimaVezAlimentado;
    public string ultimaVezDescansado;

    //ademas de la cantidad de los estados de la mascota en ese momento
    public int alimento;
    public int descanso;
    public int felicidad;

    public PetData(string ultimaVezAlimentado, string ultimaVezDescansado, int alimento, int descanso, int felicidad)
    {
        this.ultimaVezAlimentado = ultimaVezAlimentado;
        this.ultimaVezDescansado = ultimaVezDescansado;
        this.alimento = alimento;
        this.descanso = descanso;
        this.felicidad = felicidad;
    }

}
