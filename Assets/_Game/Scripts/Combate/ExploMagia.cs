using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploMagia : MonoBehaviour
{
    public float radio;
    public Equipo objetivos;
    public float tiempoVida;
	public float daño;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempoVida);
		switch (objetivos)
		{
			case Equipo.aliado:
				CausarDaño(GestionCombate.singleton.listaUnidades.unidadesEnemigas);
				break;
			case Equipo.enemigo:
				CausarDaño(GestionCombate.singleton.listaUnidades.unidadesAliadas);
				break;
			case Equipo.ambos:
				CausarDaño(GestionCombate.singleton.listaUnidades.unidadesEnemigas);
				CausarDaño(GestionCombate.singleton.listaUnidades.unidadesAliadas);
				break;
			default:
				break;
		}

		void CausarDaño(List<Unidad> u)
		{
			float r = radio * radio;
			for (int i = 0; i < u.Count; i++)
			{
				if ((transform.position - u[i].transform.position).sqrMagnitude < r)
				{
					u[i].vida.CausarDaño(daño);
				}
			}
		}

		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radio);
	}

}
