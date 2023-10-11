using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoIdiomas : MonoBehaviour
{
    public Text         txtObjetivo;
    public Diccionario  diccionario;
    public string       llave;

    void Start()
    {
		if (txtObjetivo == null)
		{
            txtObjetivo = GetComponent<Text>();
		}
        if(txtObjetivo != null) txtObjetivo.text = diccionario.GetPalabra(llave);
    }
}
