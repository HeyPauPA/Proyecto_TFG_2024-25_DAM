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
    public TMP_InputField inputNuevaTarea;

    private DateTime diaSeleccionado;
    private string diaSeleccionadoString;

    //lista de tareas (de la clase creada en el taskdata)
    private ListaDeTareas listaDeTareasTotales = new ListaDeTareas();

    //--database
    private Database db = new Database();
    private string nombreArchivo = "datos_tareasGuardadas";

    //NOTIFICACIONES
    //dato del dia en el que estamos
    DateTime diaActual = DateTime.Now;
    //conexion con el notification manager
    public NotificationManager ntMg = new NotificationManager() ;
    private bool notificacionEnviadasHoy = false;

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
        //Cargar datos del json
        db.CargarDatos<ListaDeTareas>(nombreArchivo, (datosCargados) =>
        {
            listaDeTareasTotales = datosCargados;
        });

    }
    private void Update()
    {
        //Si no ha mandado la notificacion hoy
        if(notificacionEnviadasHoy != true)
        {
            //Si el dia en el que nos encontramos, coincide con el dia en el que hay alguna tarea, manda notificacion     
            TasksData tareasdiariasParaNotificacion = BuscarTareasPorFecha(diaActual.ToShortDateString());

            if (tareasdiariasParaNotificacion != null) 
            {
                foreach (string tarea in tareasdiariasParaNotificacion.tareas)
                {
                    ntMg.mandarNotificacionTarea("Tarea diaria:", tarea);
                    Debug.Log(tarea);
                }
                notificacionEnviadasHoy = true;
            }
        }
        
    }

    //---------- METODOS DE MOSTRAR TAREAS ----------

    /// <summary>
    /// Metodo para mostrar el dia seleccionado como titulo del area de tareas
    /// </summary>
    public void mostrarFechaSeleccionada(DateTime nuevoDiaSeleccionado)
    {
        //guardamos dato de la fecha para tenerla en todo momento (tanto en tipo datetime como en string)
        //necesito q sea shortdatestring para q solo me de la fecha
        diaSeleccionado = nuevoDiaSeleccionado;
        diaSeleccionadoString = nuevoDiaSeleccionado.ToShortDateString();

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
            Debug.Log("tareas encontradas");
            foreach (string tarea in tareasDelDia.tareas)
            {
                GameObject nuevaTarea = Instantiate(prefabTarea, zonaTareas);
                nuevaTarea.GetComponentInChildren<TextMeshProUGUI>().text = tarea;
            }
        }
    }

    //---------- METODO DE BUSQUEDA DE TAREAS ----------
    /// <summary>
    /// Metodo para buscar las tareas de dentro de la lista, por una fecha determinada
    /// </summary>
    private TasksData BuscarTareasPorFecha(string fecha)
    {
        //Filtramos de dentro de la lista de todas las tareas
        foreach (TasksData tarea in listaDeTareasTotales.tareasPorDia)
        {
            //aquellas que coincidan con la fecha seleccionada
            if (tarea.fecha == fecha)
            {
                return tarea;
            }
        }
        return null;
    }

    //---------- METODO DE NUEVA TAREA ----------
    /// <summary>
    /// Metodo para crear nuevas tareas en el dia elegido
    /// </summary>
    public void NuevaTarea()
    {
        //si no hay texto en el imput
        if (string.IsNullOrWhiteSpace(inputNuevaTarea.text))
        {
            return;//no hacemos nada
        }
        else 
        {
            //buscamos las tareas de ese dia
            TasksData tareasDelDia = BuscarTareasPorFecha(diaSeleccionado.ToShortDateString());

            //Si no hay lista de tareas de ese dia, la crea
            if (tareasDelDia == null)
            {
                Debug.Log("Estoy dentro del tareas del dia y como no habia creo nuevas");
                tareasDelDia = new TasksData
                {
                    fecha = diaSeleccionado.ToShortDateString(),
                    tareas = new List<string>() 
                };
                listaDeTareasTotales.tareasPorDia.Add(tareasDelDia);
                Debug.Log("He aniadido una tarea");
            }

            tareasDelDia.tareas.Add(inputNuevaTarea.text); //mete el texto del imput como tarea

            inputNuevaTarea.text = "";

            //GUARDAMOS LOS DATOS EN JSON
            db.GuardarDatos(nombreArchivo, listaDeTareasTotales);

            mostrarTareasParaDiaSeleccionado(diaSeleccionado);
        }

    }
}

