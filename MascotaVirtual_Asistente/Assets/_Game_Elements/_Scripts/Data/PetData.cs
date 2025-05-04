using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetData 
{
    //LOS DATOS A GUARDAR DE LA MASCOTA

    public DateTime ultimaVezAlimentado;
    public DateTime ultimaVezDescansado;
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
