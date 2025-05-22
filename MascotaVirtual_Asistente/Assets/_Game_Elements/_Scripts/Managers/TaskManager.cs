using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using SimpleCalendar;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instancia;

    //ELEMENTOS DEL UI
    public TextMeshProUGUI fechaSeleccionada;
    public Transform zonaTareas;
    public GameObject prefabTarea;
    public InputField inputNuevaTarea;

    private string diaSeleccionadoString;

    //lista de tareas (de la clase creada en el taskdata)
    private ListaDeTareas listaDeTareasTotales = new ListaDeTareas();

    public void Awake()
    {
        //Singleton simple
        //con esto nos aseguramos de q solo hay un pet manager accesible en la escena
        //*de esta manera no puedes llamar al petmanager en otro awake
        if (instancia == null)
        {
            instancia = this;
        }
        else Debug.LogWarning("Hay mas de un TaskManager en la escena");
    }

    private void Start()
    {
        //TODO Cargar datos del json
    }

    //---------- METODOS DE MOSTRAR TAREAS ----------

    /// <summary>
    /// Metodo qpara mostrar el dia seleccionado como titulo del area de tareas
    /// </summary>
    public void mostrarFechaSeleccionada(DateTime diaSeleccionado)
    {
        //guardamos dato de la fecha para tenerla en todo momento
        diaSeleccionadoString = diaSeleccionado.ToShortDateString();

        //necesito q sea shortdatestring para q solo me de la fecha
        if (diaSeleccionado != null)
        {
            fechaSeleccionada.text = ("[ " + diaSeleccionadoString + " ]");

            //llamamos al metodo de mostrar las tareas
            mostrarTareasParaDiaSeleccionado(diaSeleccionado);
        }
        else
        {
            fechaSeleccionada.text = ("[ ERROR ELIGIENDO LA FECHA CORRECTA]");
        } 
    }

    /// <summary>
    /// Metodo que muestra las tareas del dia elegido
    /// </summary>
    public void mostrarTareasParaDiaSeleccionado(DateTime diaSeleccionado)
    {
        //SI HAY TAREAS YA MOSTRADAS LAS BORRAMOS
        for (int i = 0; i < zonaTareas.childCount; i++)
        {
            Destroy(zonaTareas.GetChild(i).gameObject);
        }

        //BUSCAMOS LAS TAREAS DEL DIA CORRESPONDIENTE
        TasksData tareasDelDia = BuscarTareasPorFecha(diaSeleccionado.ToShortDateString());

        //Y LAS MOSTRAMOS (SI TIENE)
        if (tareasDelDia != null)
        {
            foreach (string tarea in tareasDelDia.tareas)
            {
                GameObject nuevaTarea = Instantiate(prefabTarea, zonaTareas);
                nuevaTarea.GetComponent<TextMeshProUGUI>().text = tarea;
            }
        }
    }

    public void NuevaTarea(DateTime diaSeleccionado)
    {
        //si no hay texto en el imput
        if (string.IsNullOrWhiteSpace(inputNuevaTarea.text))
        {
            Debug.Log("No estoy haciendo nah");
            return;//no hacemos nada
        }

        //buscamos las tareas de ese dia
        TasksData tareasDelDia = BuscarTareasPorFecha(diaSeleccionadoString);

        //Si no hay lista de tareas de ese dia, la crea
        if (tareasDelDia == null)
        {
            Debug.Log("Estoy dentro del tareas del dia");
            tareasDelDia = new TasksData
            {
                fecha = diaSeleccionadoString,
                tareas = new List<string>() 
            };
            listaDeTareasTotales.tareasPorDia.Add(tareasDelDia);
            Debug.Log("He aniadido una tarea");
        }

        tareasDelDia.tareas.Add(inputNuevaTarea.text); //mete el texto del imput como tarea

        inputNuevaTarea.text = "";
        //GuardarEnJson();
        mostrarTareasParaDiaSeleccionado(diaSeleccionado);//recargamos las tareas de ese dia
    }


    //---------- METODOS DE BUSQUEDA DE TAREAS ----------
    /// <summary>
    /// Metodo para buscar las tareas de dentro de la lista, por una fecha determinada
    /// </summary>
    private TasksData BuscarTareasPorFecha(string fecha)
    {
        //Filtramos de dentro de la lista de todas las tareas
        foreach (TasksData tarea in listaDeTareasTotales.tareasPorDia)
        {
            Debug.Log(tarea.fecha);
            //aquellas que coincidan con la fecha seleccionada
            if (tarea.fecha == fecha)
            {
                return tarea;
            }
                
        }
        return null;
    }

}

