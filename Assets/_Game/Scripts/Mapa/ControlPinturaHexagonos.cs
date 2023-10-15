using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPinturaHexagonos : MonoBehaviour
{
    public static ControlPinturaHexagonos singleton;

    public delegate void DMostrar();

    public DMostrar mostrar;
    public DMostrar ocultar;
	public Color colorBien = Color.green;
	public Color colorMal = Color.red;

	private void Awake()
	{
		singleton = this;
		colorBien.a = 0;
		colorMal.a = 0;
	}
	[ContextMenu("MostrarAliados")]
	public void TestMostrarAliado()
	{
		AsignarBando(Equipo.aliado);
	}
	[ContextMenu("MostrarEnemigos")]
	public void TestMostrarEnemigos()
	{
		AsignarBando(Equipo.enemigo);
	}
	[ContextMenu("MostrarAmbos")]
	public void TestMostrarAmbos()
	{
		AsignarBando(Equipo.ambos);
	}

	public void AsignarBando(Equipo e)
	{
		PintaHexagonos.equipo = e;
		mostrar();
	}

	[ContextMenu("Oculat")]
	public void OcultarMarcas()
	{
		ocultar();
	}
}
