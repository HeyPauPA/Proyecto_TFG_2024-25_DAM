using UnityEngine;
using UnityEngine.UI;

public class DelayBotonNecesidades : MonoBehaviour
{
    //VARIABLES
    public Button botonNecesidad;
    public float tiempoDelay = 3f;
    private float contador = 0f;
    private bool botonActivado = true;

    void Start()
    {
        botonNecesidad.onClick.AddListener(OnClick);
    }

    void Update()
    {
        //si el boton esta desactivado empieza la cuenta atras
        if (botonActivado == false)
        {
            //reducimos el tiempo del contador
            contador -= Time.deltaTime;

            //cuando acaba lo reactivamos
            if (contador <= 0f)
            {
                botonActivado = true;
                botonNecesidad.interactable = true;
            }
        }
    }

    public void OnClick()
    {
        if (botonActivado)
        {
            //Se desactiva el boton 
            botonActivado = false;
            botonNecesidad.interactable = false;

            //empieza la cuenta atras
            contador = tiempoDelay;
        }
    }
}
