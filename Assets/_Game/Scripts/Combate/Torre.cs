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
	public float daño=10; 

	public float distanciaForzarAtaque;
	public float distanciaAtacar;

	public ParticleSystem particulas;
	public float frecuenciaDaño;

	public GameObject objetoNormal;
	public GameObject objetoFracturado;
	public Rigidbody[] rbs;
	public GameObject particulasMuerte;

	List<Unidad> unidades;
	private void Start()
	{
		GestionCombate.singleton.listaUnidades.AñadirTorre(this);
		StartCoroutine(ListarUnidades());
		StartCoroutine(Atacar());
		rbs = GetComponentsInChildren<Rigidbody>();
		objetoFracturado.SetActive(false);
		vida.OnVidaPerdida += TorrePierdeVida;
	}

	public void TorrePierdeVida()
	{
		AlertType tipo = (equipo == Equipo.aliado) ? AlertType.PlayerTowerAttacked : AlertType.EnemyTowerAttacked;
		AlertEmition ae = new AlertEmition(tipo, transform.position);
		AlertSingleton.Instance.TriggerAlert.Invoke(ae);
	}

	public void Morir()
	{
		print(GestionCombate.singleton);
		print(GestionCombate.singleton.listaUnidades);

		GestionCombate.singleton.listaUnidades.QuitarTorre(this);
		objetoFracturado.SetActive(true);
		objetoNormal.SetActive(false);
		print("Entro 2");
		for (int i = 0; i < rbs.Length; i++)
		{
			rbs[i].AddTorque((new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100)) * 20));
		}
		print("Entro 3");
		particulasMuerte.SetActive(true);
		if (tipoTorre == TipoTorre.generica)
		{
			Destroy(gameObject, 5);
		}
		print("Entro 4");
        vida.OnVidaPerdida -= TorrePierdeVida;
    }

	IEnumerator ListarUnidades()
	{
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

	IEnumerator Atacar()
	{
		float d = distanciaAtacar * distanciaAtacar;
		yield return new WaitForSeconds(Random.Range(2,10));
		if (frecuenciaDaño > 0)
		{
			while (vida.vivo)
			{
				for (int i = 0; i < unidades.Count; i++)
				{
					if ((unidades[i].transform.position - transform.position).sqrMagnitude < d)
					{
						unidades[i].vida.CausarDaño(daño);
						particulas.Play();
					}
				}
				yield return new WaitForSeconds(frecuenciaDaño);
			}
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
		Handles.color = Color.red;
		Handles.DrawWireDisc(transform.position + Vector3.up * 0.2f, Vector3.up, distanciaForzarAtaque);
		Handles.color = Color.black;
		Handles.DrawWireDisc(transform.position + Vector3.up * 0.2f, Vector3.up, distanciaAtacar);
	}
#endif
}

public enum TipoTorre
{
	generica,
	principal
}