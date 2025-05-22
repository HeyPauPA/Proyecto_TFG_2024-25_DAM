using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CREO UN TIPO DE DATOS ESPECIAL PARA GUARDAR CADA TAREA
[Serializable]
public class TasksData 
{
    //[DATOS A GUARDAR DE CADA TAREA]
    //necesitamos la fecha
    public string fecha;

    //y las tareas de esa fecha concreta
    public List<string> tareas = new List<string>();

    //public TasksData(string fecha, List<string> tareas)
    //{
    //    this.fecha = fecha;
    //    this.tareas = tareas;
    //}
}
//CREO UN TIPO DE LISTAS QUE SERAN AQUELLAS QUE PUEDAN GUARDAR LOS DATOS DE LAS TAREAS
[System.Serializable]
public class ListaDeTareas
{
    public List<TasksData> tareasPorDia = new List<TasksData>();
}
