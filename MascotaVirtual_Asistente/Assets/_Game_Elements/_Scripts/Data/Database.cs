using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Database
{
    //[METODOS DE GUARDADO Y CARDA DE CUALQUIER TIPO DE DATO A JSON]

    //podemos guardar los datos de cualquier tipo (por eso el T)
    public void GuardarDatos<T>(string nombreGuardado, T datosGuardados)
    {
        string archivoGuardadoJson = JsonUtility.ToJson(datosGuardados);

        string rutaArchivo = Path.Combine(Application.persistentDataPath, nombreGuardado + ".json");

        File.WriteAllText(rutaArchivo, archivoGuardadoJson); 
    }

    public void CargarDatos<T>(string nombreGuardado, System.Action<T> callback)
    {
        //*la ruta del archivo la repito para poder poner el nombre de guardado q desee para cada archivo
        string rutaArchivo = Path.Combine(Application.persistentDataPath, nombreGuardado + ".json");

        if (File.Exists(rutaArchivo))
        {
            string jsonCargado = File.ReadAllText(rutaArchivo);
            callback(JsonUtility.FromJson<T>(jsonCargado));
        }
        else
        {
            Debug.Log("El archivo no existe");
        }
    }

}
