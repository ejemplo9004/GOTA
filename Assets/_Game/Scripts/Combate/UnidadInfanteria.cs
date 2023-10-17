using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnidadInfanteria : Unidad
{
	NavMeshAgent agente;
	public Animator animator;
	public float daño;
	public TipoDaño tipoDaño;
	public float inicioDaño;
	public float periodoDaño;


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
		switch (tipoDaño)
		{
			case TipoDaño.porSegundo:
				if (vidaObjetivo != null)
				{
					vidaObjetivo.CausarDaño(daño * Time.deltaTime);
				}
				break;
			case TipoDaño.porPeriodo:
				if (vidaObjetivo != null && Time.time > tiempoAtacar)
				{
					tiempoAtacar = Time.time + periodoDaño;
					vidaObjetivo.CausarDaño(daño);
				}
				break;
			case TipoDaño.manual:
				break;
			default:
				break;
		}
	}

	public void Dañar()
	{
		if (vidaObjetivo != null)
		{
			vidaObjetivo.CausarDaño(daño);
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
			tiempoAtacar = Time.time + inicioDaño;
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
		GestionCombate.singleton.SumarPuntos(equipo, vida.vidaMaxima);
		Destroy(gameObject, 6);
	}
}


public enum TipoDaño
{
	porSegundo,
	porPeriodo,
	manual
}
