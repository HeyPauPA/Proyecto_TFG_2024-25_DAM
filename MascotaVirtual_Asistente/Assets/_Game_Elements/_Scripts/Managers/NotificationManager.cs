using System;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public Toggle checkerNotificaciones; // Asignar desde el inspector

    private const string canalTareas = "notificaciones_tareas";
    private const string canalAyudas = "notificaciones_ayudas";

    void Start()
    {
        ////CANAL 1 PARA NOTIFICACIONES DE TAREAS
        var channelTareas = new AndroidNotificationChannel()
        {
            Id = canalTareas,
            Name = "Canal de Tareas",
            Importance = Importance.High,
            Description = "Notificaciones de tareas diarias",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channelTareas);

        //CANAL 2 PARA NOTIFICACIONES DE CONSEJOS
        var channelAyudas = new AndroidNotificationChannel()
        {
            Id = canalAyudas,
            Name = "Canal de Ayudas",
            Importance = Importance.High,
            Description = "Notificaciones de consejos de salud",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channelAyudas);

        //RECOJEMOS EL ESTADO DEL CHECKER DEL PLAYERPREFS
        bool notificacionesActivas = PlayerPrefs.GetInt("notificacionesActivadas", 1) == 1;
        checkerNotificaciones.isOn = notificacionesActivas;

        // Si están activadas notificaciones y no se han programado aún
        int savedId = PlayerPrefs.GetInt("idNotificacionAyuda", -1);
        if (notificacionesActivas && AndroidNotificationCenter.CheckScheduledNotificationStatus(savedId) 
            != NotificationStatus.Scheduled)
        {
            ProgramarNotificacionAyudas();
        }
    }


    //--- METODO DEL CHECKER ---
    /// <summary>
    ///METODO PARA GUARDAR EL DATO DEL CHECKER (EN EL PLAYERPREFS)
    /// </summary>
    public void CambiarEstadoNotificaciones()
    {
        //Si el checker esta on activo el programador de las noticicaiones
        if (checkerNotificaciones.isOn)
        {
            ProgramarNotificacionAyudas();
            PlayerPrefs.SetInt("notificacionesActivadas", 1);
        }
        else //si no las cancelo
        {
            AndroidNotificationCenter.CancelAllScheduledNotifications();
            PlayerPrefs.SetInt("notificacionesActivadas", 0);
        }
    }

    // ----- METODOS DE MANDO DE NOTIFICACIONES -----
    /// <summary>
    /// METODO para mandar la notificacion de una tarea en el dia en el que estas, con el titulo de esta
    /// </summary>
    public void mandarNotificacionTarea(string titulo, string texto)
    {
        var notificacion = new AndroidNotification
        {
            Title = titulo,
            Text = texto,
            FireTime = DateTime.Now.AddSeconds(1)
        };

        AndroidNotificationCenter.SendNotification(notificacion, canalTareas);
    }

    /// <summary>
    /// METODO para mandar la notificacion de una ayuda al usuario cada 2h desde q se abre por primera vez la aplicacion
    /// </summary>
    private void ProgramarNotificacionAyudas()
    {
        var notificacion = new AndroidNotification
        {
            Title = "¡Mantente sano!",
            Text = "Recuerda tomar agua",
            FireTime = DateTime.Now.AddSeconds(10),
            RepeatInterval = TimeSpan.FromSeconds(30)
            
        };
        Debug.Log("Noti salud");
        int id = AndroidNotificationCenter.SendNotification(notificacion, canalAyudas);
        PlayerPrefs.SetInt("idNotificacionAyuda", id);
    }


    
}
