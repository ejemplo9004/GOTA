using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionBaraja : MonoBehaviour
{
    public ScriptableDeck deck;
    public GameObject baseImagen;
    public Transform tPadreImagenes;
    public NombreObjeto[] objetos;


    void Start()
    {
		if (objetos.Length > 0)
		{
            string ba = PlayerPrefs.GetString("mazo", "MUISCAS");
			for (int i = 0; i < objetos.Length; i++)
			{
				if (ba.Equals(objetos[i].nombre))
				{
					gameObject.SetActive(false);
					objetos[i].objeto.SetActive(true);
				}
			}
		}
		for (int i = 0; i < deck.cards.Length; i++)
		{
            GameObject g = Instantiate(baseImagen, tPadreImagenes);
            g.GetComponent<Image>().sprite = deck.cards[i].cardSprite;
		}
    }

    public void CambiarMazoActivo(string mazo)
	{
        PlayerPrefs.SetString("mazo", mazo);
	}
}

[System.Serializable]
public class NombreObjeto
{
    public string nombre;
    public GameObject objeto;
}
