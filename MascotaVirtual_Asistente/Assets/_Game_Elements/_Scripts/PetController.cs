using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Animator animatorMascota;

    private void Awake()
    {
        
    }

    //Metodos para las animaciones de la mascota
    //MALAS
    public void Hambriento()
    {
        //Activamos animacion
        animatorMascota.SetTrigger("Hambriento");
    }
    public void Cansando()
    {
        //Activamos animacion
        animatorMascota.SetTrigger("Cansando");
    }
    public void Triste() //falta de hambre y sueño
    {
        //Activamos animacion
        animatorMascota.SetTrigger("Triste");
    }
    //BUENAS
    public void Comer()
    {
        //Activamos animacion
        animatorMascota.SetTrigger("Comer");
    }
    public void Dormir() 
    {
        //Activamos animacion
        animatorMascota.SetTrigger("Dormir");
    }
}
