using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour
{
	public Equipo equipo;
	public Vida vida;
	public TipoTorre tipoTorre;
	private void Start()
	{
		GestionCombate.singleton.listaUnidades.AñadirTorre(this);
	}

	public void Morir()
	{
		GestionCombate.singleton.listaUnidades.QuitarTorre(this);
	}
}

public enum TipoTorre
{
	generica,
	principal
}