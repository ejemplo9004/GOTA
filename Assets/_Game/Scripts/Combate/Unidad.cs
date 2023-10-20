using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(Vida))]
public class Unidad : MonoBehaviour
{
	public Equipo equipo;
	public Distancia distancia;
	public Estado estado;
	[HideInInspector]
	public Vida vida;
	public Transform target;
	float distanciaObjetivo = -1;
	public Objetivos[] objetivos;
	public Vida vidaObjetivo;
	public Torre torreForzarAtaque;
	public TipoUnidad tipoUnidad = TipoUnidad.terrestre;

	private void Awake()
	{
		vida = GetComponent<Vida>();
		StartCoroutine(MiUpdate());
		StartCoroutine(CalcularDistancia());
		StartCoroutine(MiStart());
	}

	private IEnumerator MiStart()
	{
		yield return new WaitForEndOfFrame();
		GestionCombate.singleton.listaUnidades.AñadirUnidad(this);
	}

	private IEnumerator MiUpdate()
	{
		while (vida.vivo)
		{
			yield return new WaitForEndOfFrame();
			CheckEstado();
		}
		yield return new WaitForEndOfFrame();
		CheckEstado();
	}

	private void CheckEstado()
	{
		switch (estado)
		{
			case Estado.idle:
				EstadoIdle();
				break;
			case Estado.seguir:
				transform.LookAt(target, Vector3.up);
				EstadoSeguir();
				break;
			case Estado.atacar:
				EstadoAtacar();
				break;
			case Estado.muerto:
				EstadoMuerto();
				break;
			default:
				break;
		}
	}

	public virtual void CambiarEstado(Estado e)
	{
		switch (e)
		{
			case Estado.idle:
				break;
			case Estado.seguir:
				break;
			case Estado.atacar:
				transform.LookAt(target);
				break;
			case Estado.muerto:
				GestionCombate.singleton.listaUnidades.QuitarUnidad(this);
				break;
			default:
				break;
		}
		estado = e;
	}

	public virtual void EstadoIdle()
	{
		if (target != null)
		{
			CambiarEstado(Estado.seguir);
		}
	}
	public virtual void EstadoSeguir()
	{
		if (distanciaObjetivo < distancia.distanciaAtacar && distanciaObjetivo>-1 || (torreForzarAtaque != null && target == torreForzarAtaque.transform))
		{
			CambiarEstado(Estado.atacar);
		}
		else if (target == null)
		{
			CambiarEstado(Estado.idle);
		}
	}
	public virtual void EstadoAtacar()
	{
		if (distanciaObjetivo > distancia.distanciaAtacar + 0.4f )
		{
			if (torreForzarAtaque == null)
			{
				CambiarEstado(Estado.seguir);
			}
			else if (torreForzarAtaque != null && target != torreForzarAtaque.transform)
			{
				CambiarEstado(Estado.seguir);
			}
		}
		else if (target == null)
		{
			CambiarEstado(Estado.idle);
		}
		else
		{
			transform.LookAt(target);
		}
	}
	public virtual void EstadoMuerto()
	{

	}

	IEnumerator CalcularDistancia()
	{
		while (vida.vivo)
		{
			yield return new WaitForSeconds(0.2f);

			target = CalcularObjetivo();
			if (target != null)
			{

				distanciaObjetivo = Vector3.Distance(transform.position, target.position);
			}
			else
			{
				distanciaObjetivo = -1;
			}
		}
	}
	Transform CalcularObjetivo()
	{
		if (vida.vivo)
		{
			float distancia2 = distancia.distanciaSeguir * distancia.distanciaSeguir;
			for (int i = 0; i < objetivos.Length; i++)
			{
				if (objetivos[i] == Objetivos.unidad)
				{
					Unidad uObj = null;
					float dMin = 1000200;
					for (int j = 0; j < GetUnidadesOpuestas().Count; j++)
					{
						float d = (GetUnidadesOpuestas()[j].transform.position - transform.position).sqrMagnitude;
						if (d < distancia2 && d < dMin)
						{
							dMin = d;
							uObj = GetUnidadesOpuestas()[j];
						}
					}
					if (uObj != null)
					{
						torreForzarAtaque = null;
						vidaObjetivo = uObj.vida;
						return uObj.transform;
					}
				}
				if (objetivos[i] == Objetivos.torre)
				{
					Torre uObj = null;
					float dMin = 1200000;
					for (int j = 0; j < GetTorresOpuestas().Count; j++)
					{
						float d = (GetTorresOpuestas()[j].transform.position - transform.position).sqrMagnitude;
						if (d < distancia2 && d < dMin)
						{
							dMin = d;
							uObj = GetTorresOpuestas()[j];
						}
					}
					if (uObj != null)
					{
						torreForzarAtaque = null;
						vidaObjetivo = uObj.vida;
						return uObj.transform;
					}
				}
				if (objetivos[i] == Objetivos.torrePrincipal)
				{
					if (GetTorresPrincipalesOpuestas().Count > 0)
					{
						float dis = 1000000;
						int k = 0;
						for (int j = 0; j < GetTorresPrincipalesOpuestas().Count; j++)
						{
							if ((GetTorresPrincipalesOpuestas()[j].transform.position - transform.position).sqrMagnitude < dis)
							{
								dis = (GetTorresPrincipalesOpuestas()[j].transform.position - transform.position).sqrMagnitude;
								k = j;
							}
						}
						vidaObjetivo = GetTorresPrincipalesOpuestas()[k].vida;
						return GetTorresPrincipalesOpuestas()[k].transform;
					}
				}
			}
		}


		return null;
	}

	public List<Unidad> GetUnidadesOpuestas()
	{
		if (equipo == Equipo.aliado)
		{
			return GestionCombate.singleton.listaUnidades.unidadesEnemigas;
		}
		return GestionCombate.singleton.listaUnidades.unidadesAliadas;
	}

	public List<Torre> GetTorresOpuestas()
	{
		if (equipo == Equipo.aliado)
		{
			return GestionCombate.singleton.listaUnidades.torresEnemigas;
		}
		return GestionCombate.singleton.listaUnidades.torresAliadas;
	}
	public List<Torre> GetTorresPrincipalesOpuestas()
	{
		if (equipo == Equipo.aliado)
		{
			return GestionCombate.singleton.listaUnidades.torresPrincipalesEnemigas;
		}
		return GestionCombate.singleton.listaUnidades.torresPrincipalesAliadas;
	}
	public virtual void Morir()
	{
		CambiarEstado(Estado.muerto);
	}
#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
		Handles.color = Color.red;
		Handles.DrawWireDisc(transform.position + Vector3.up * 0.2f, Vector3.up, distancia.distanciaAtacar);
		Handles.color = Color.yellow;
		Handles.DrawWireDisc(transform.position + Vector3.up * 0.2f, Vector3.up, distancia.distanciaSeguir);
		Handles.color = Color.green;
		Handles.DrawWireDisc(transform.position + Vector3.up * 0.2f, Vector3.up, distancia.distanciaEscapar);	
	}
	private void OnDrawGizmos()
	{
		int icono = (int)estado;
		icono++;
		Gizmos.DrawIcon(transform.position + Vector3.up * 2.5f, "0" + icono + ".png");
	}
#endif
}

[System.Serializable]
public class Distancia
{
	public float distanciaAtacar;
	public float distanciaSeguir;
	[SerializeField]
	private float _distanciaEscapar;
	public float distanciaEscapar
	{
		get => Mathf.Max(distanciaSeguir, _distanciaEscapar);
		set => _distanciaEscapar = value;
	}
}

public enum Estado
{
    idle    = 0,
    seguir  = 1,
    atacar  = 2,
    muerto  = 3
}

public enum Equipo
{
	aliado = 0,
	enemigo = 1,
	ambos = 2
}

public enum Objetivos
{
	unidad = 0,
	torre = 1,
	torrePrincipal = 2
}

public enum TipoUnidad
{
	terrestre = 0,
	voladora = 1
}