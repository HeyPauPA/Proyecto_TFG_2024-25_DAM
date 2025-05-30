using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    //VARIABLES 
    public Animator animatorMascota;
    private Vector3 destino;
    public float velocidad;


    private void Awake()
    {
        
    }
    private void Update()
    {
        //si la distancia entre la posicion de la mascota y el destino es mayor a 0.5
        if(Vector3.Distance(transform.position, destino) > 0.5f)
        {
            //nos vamos acercando a la velocidad indicada
            transform.position = Vector3.MoveTowards(transform.position,destino,velocidad*Time.deltaTime);
        }
    }

    //Metodo para el movimiento de la mascota
    public void MovimientoAleatorio(Vector3 destino)
    {
        this.destino = destino;
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
        animatorMascota.SetTrigger("Cansado");
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
    public void Feliz()
    {
        //Activamos animacion
        animatorMascota.SetTrigger("Feliz");
    }
}
