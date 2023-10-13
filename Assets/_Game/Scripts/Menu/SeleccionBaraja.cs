using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionBaraja : MonoBehaviour
{
    public ScriptableDeck deck;
    public GameObject baseImagen;
    public Transform tPadreImagenes;

    void Start()
    {
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
