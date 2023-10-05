using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Torre : MonoBehaviour
{
	public Equipo equipo;
	public Vida vida;
	public TipoTorre tipoTorre;

	public float distanciaForzarAtaque;

	private void Start()
	{
		GestionCombate.singleton.listaUnidades.AñadirTorre(this);
		StartCoroutine(ListarUnidades());
	}

	public void Morir()
	{
		GestionCombate.singleton.listaUnidades.QuitarTorre(this);
	}

	IEnumerator ListarUnidades()
	{
		List<Unidad> unidades;
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			if (equipo == Equipo.aliado)
			{
				unidades = GestionCombate.singleton.listaUnidades.unidadesEnemigas;
			}
			else
			{
				unidades = GestionCombate.singleton.listaUnidades.unidadesAliadas;
			}

			for (int i = 0; i < unidades.Count; i++)
			{
				if ((unidades[i].transform.position - transform.position).sqrMagnitude < (distanciaForzarAtaque*distanciaForzarAtaque))
				{
					unidades[i].torreForzarAtaque = this;
				}
			}
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
		Handles.color = Color.red;
		Handles.DrawWireDisc(transform.position + Vector3.up * 0.2f, Vector3.up, distanciaForzarAtaque);
	}
#endif
}

public enum TipoTorre
{
	generica,
	principal
}