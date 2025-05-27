using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    //variables
    public Toggle checkerNotificaciones;
    void Start()
    {
        //CANAL 1 PARA NOTIFICACIONES
        var channel = new AndroidNotificationChannel()
        {
            Id = "notificaciones_tareas",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Notificaciones de tareas",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        //CANAL 2 PARA NOTIFICACIONES DE CONSEJOS
        var channelHelps = new AndroidNotificationChannel()
        {
            Id = "notificaciones_ayudas",
            Name = "ChannelHelps",
            Importance = Importance.High,
            Description = "Notificaciones de ayudas",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channelHelps);

        //Activamos las notificaciones de ayuda solo si no estan activas ya
        if (checkerNotificaciones.isOn == true && AndroidNotificationCenter.CheckScheduledNotificationStatus(1) != NotificationStatus.Scheduled)
        {
            mandarNotificacionAyudas("¡Mantente sano!", "recuerda tomar agua");
        }
    }

    //----- METODOS DE MANDO DE NOTIFICACIONES -----
    /// <summary>
    /// METODO para mandar la notificacion de una tarea en el dia en el que estas, con el titulo de esta
    /// </summary>
    public void mandarNotificacionTarea(string tituloNotificacion, string textoNotificacion)
    {
        var notification = new AndroidNotification();
        notification.Title = tituloNotificacion;
        notification.Text = textoNotificacion;
        notification.FireTime= System.DateTime.Now.AddSeconds(1);

        AndroidNotificationCenter.SendNotification(notification, "notificaciones_tareas");
    }
    /// <summary>
    /// METODO para mandar la notificacion de una ayuda al usuario cada 2h desde q se abre por primera vez la aplicacion
    /// </summary>
    public void mandarNotificacionAyudas(string tituloNotificacion, string textoNotificacion)
    {
        var notification = new AndroidNotification();
        notification.Title = tituloNotificacion;
        notification.Text = textoNotificacion;
        notification.FireTime = DateTime.Now.AddHours(2); // Primera vez en 2h
        notification.RepeatInterval = TimeSpan.FromHours(2); // Luego cada 2h

        AndroidNotificationCenter.SendNotification(notification, "notificaciones_ayudas");       
    }
}
