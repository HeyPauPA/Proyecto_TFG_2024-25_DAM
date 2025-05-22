using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonNuevaTarea : MonoBehaviour
{
    //METODO PARA CREAR LA NUEVA TAREA DESDE EL BOTON DE OK DEL POUP
    public void OnClick()
    {
        TaskManager.instancia.NuevaTarea();
    }
}
