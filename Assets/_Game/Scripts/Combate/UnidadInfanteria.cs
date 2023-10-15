using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnidadInfanteria : Unidad
{
    NavMeshAgent agente;
	public Animator animator;
	public float da�o;
	public TipoDa�o tipoDa�o;
	public float inicioDa�o;
	public float periodoDa�o;


	private float tiempoAtacar;

	Vector3 lPos;
	float distanciaMovido;
	private void Start()
	{
		agente = GetComponent<NavMeshAgent>();
		if (animator == null)
		{
			animator = GetComponentInChildren<Animator>();
		}
		StartCoroutine(CambiosPos());
	}

	IEnumerator CambiosPos()
	{
		while (vida.vivo)
		{
			lPos = transform.position;
			yield return new WaitForSeconds(0.8f);
			distanciaMovido = (lPos - transform.position).sqrMagnitude;
			lPos = transform.position;
			if(AlertSingleton.Instance != null)
			{
				bool side = transform.position.z > AlertSingleton.Instance.ZLimite;
				if (equipo == Equipo.aliado && side)
				{
					AlertEmition ae = new AlertEmition(AlertType.UnitInPlayerTerritory, transform.position);
					AlertSingleton.Instance.TriggerAlert(ae);
				}else if(equipo == Equipo.enemigo && !side)
				{
                    AlertEmition ae = new AlertEmition(AlertType.UnitInEnemyTerritory, transform.position);
                    AlertSingleton.Instance.TriggerAlert(ae);
                }
			}
		}
	}

	public override void EstadoAtacar()
	{
		base.EstadoAtacar();
		agente.SetDestination(transform.position);
		switch (tipoDa�o)
		{
			case TipoDa�o.porSegundo:
				if (vidaObjetivo != null)
				{
					vidaObjetivo.CausarDa�o(da�o * Time.deltaTime);
				}
				break;
			case TipoDa�o.porPeriodo:
				if (vidaObjetivo != null && Time.time > tiempoAtacar)
				{
					tiempoAtacar = Time.time + periodoDa�o;
					vidaObjetivo.CausarDa�o(da�o);
				}
				break;
			case TipoDa�o.manual:
				break;
			default:
				break;
		}
	}

	public void Da�ar()
	{
		if (vidaObjetivo != null)
		{
			vidaObjetivo.CausarDa�o(da�o);
		}
	}

	public override void EstadoIdle()
	{
		base.EstadoIdle();
		if (target != null)
		{
			CambiarEstado(Estado.seguir);
		}
		else
		{
			agente.SetDestination(transform.position);
		}
	}

	public override void EstadoMuerto()
	{
		base.EstadoMuerto();
		agente.enabled = false;
	}

	public override void EstadoSeguir()
	{
		base.EstadoSeguir();
		if (target != null)
		{
			agente.SetDestination(target.position);
		}
	}

	public override void CambiarEstado(Estado e)
	{
		base.CambiarEstado(e);
		if (e == Estado.atacar)
		{
			tiempoAtacar = Time.time + inicioDa�o;
		}
		if(animator != null)
		{
			animator.SetInteger("estado", (int)e);
			//if (distanciaMovido < 0.2f && estado == Estado.seguir)
			//{
			//	animator.SetInteger("estado", 0);
			//}
			//else
			//{
			//	animator.SetInteger("estado", (int)e);
			//}
		}
	}

	public override void Morir()
	{
		base.Morir();
		//print("Muerto"  + gameObject.name);
		Destroy(gameObject, 5);
	}
}


public enum TipoDa�o
{
	porSegundo,
	porPeriodo,
	manual
}