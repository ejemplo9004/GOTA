using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipador : MonoBehaviour
{
    public Torre[] torres;
    public Unidad[] unidades;
    public ExploMagia[] explos;

	[ContextMenu("Aliar")]
	public void TestEquiparAliados()
	{
		Inicializar(Equipo.aliado);
	}
	[ContextMenu("Enemistar")]
	public void TestEquiparEnemigos()
	{
		Inicializar(Equipo.enemigo);
	}

	public void Inicializar(Equipo e)
	{
        torres = GetComponents<Torre>();
		for (int i = 0; i < torres.Length; i++)
		{
			torres[i].equipo = e;
			print("Equipando a " + gameObject.name + " como " + e);
		}
		unidades = GetComponents<Unidad>();
		for (int i = 0; i < unidades.Length; i++)
		{
			unidades[i].equipo = e;
		}
		unidades = GetComponentsInChildren<Unidad>();
		for (int i = 0; i < unidades.Length; i++)
		{
			unidades[i].equipo = e;
		}

		explos = GetComponents<ExploMagia>();
		for (int i = 0; i < explos.Length; i++)
		{
			if (explos[i].objetivos == Equipo.aliado)
			{
				explos[i].objetivos = Equipo.enemigo;
			}
			else if(explos[i].objetivos == Equipo.enemigo)
			{
				explos[i].objetivos = Equipo.aliado;
			}
		}

		UnityEngine.UI.Image im = GetComponentInChildren<UnityEngine.UI.Image>();
		if (im != null)
		{
			im.color = e == Equipo.enemigo ? Color.red : Color.green;
		}
	}
}
