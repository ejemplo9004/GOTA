using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CartasEnBaraja : MonoBehaviour
{
    public Barajas[] barajas;

    public ScriptableDeck deck;
    public Image imPrevia;
    public Text txtDescripcion;
    public Text txtNombre;
    public Slider slVida;
    public Slider slVelocidad;
    public Slider slDa�o;

    public float referenciaVida;
    public float referenciaVelocidad;
    public float referenciaDa�o;

    int iActual;

    void Start()
    {
        string m = PlayerPrefs.GetString("mazo", "DEMONIOS");
		for (int i = 0; i < barajas.Length; i++)
		{
			if (barajas[i].nombre == m)
			{
                deck = barajas[i].deck;
			}
		}
        MostrarCarta(0);
    }

    public void MostrarCarta(int cual)
	{
        txtDescripcion.text = deck.diccionario.GetPalabra(deck.cards[cual].cardName.ToUpper());
        imPrevia.sprite = deck.cards[cual].cardSprite;
        txtNombre.text = deck.cards[cual].cardName.ToUpper();
        iActual = cual;
        Vida v = deck.cards[cual].prefab.GetComponent<Vida>();
		if (v == null)
		{
            v = deck.cards[cual].prefab.GetComponentInChildren<Vida>();
        }
		if (v != null)
		{
            slVida.value = v.vidaMaxima / referenciaVida;
		}
		else
		{
            slVida.value = 0;
		}

        NavMeshAgent a = deck.cards[cual].prefab.GetComponent<NavMeshAgent>();
        if (a == null)
        {
            a = deck.cards[cual].prefab.GetComponentInChildren<NavMeshAgent>();
        }
        if (a != null)
        {
            slVelocidad.value = a.speed / referenciaVelocidad;
        }
        else
        {
            slVelocidad.value = 0;
        }

        UnidadInfanteria u = deck.cards[cual].prefab.GetComponent<UnidadInfanteria>();
        if (u == null)
        {
            u = deck.cards[cual].prefab.GetComponentInChildren<UnidadInfanteria>();
        }
        if (u != null)
        {
            slDa�o.value = u.da�o / referenciaDa�o;
        }
        else
        {
            Torre t = deck.cards[cual].prefab.GetComponent<Torre>();
			if (t == null)
			{
                slDa�o.value = 0;
			}
			else
			{
                slDa�o.value = t.da�o / referenciaDa�o;
            }
        }
    }

    public void Siguiente()
	{
        iActual = Mathf.Clamp(iActual + 1, 0, deck.cards.Length-1);
        MostrarCarta(iActual);
    }
    public void Anterior()
    {
        iActual = Mathf.Clamp(iActual - 1, 0, deck.cards.Length-1);
        MostrarCarta(iActual);
    }
}

[System.Serializable]
public class Barajas
{
    public string nombre;
    public ScriptableDeck deck;
}