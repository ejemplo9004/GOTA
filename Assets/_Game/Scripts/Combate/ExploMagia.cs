using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploMagia : MonoBehaviour
{
    public float radio;
    public Equipo objetivos;
    public float tiempoVida;
	public float da�o;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempoVida);
		switch (objetivos)
		{
			case Equipo.aliado:
				CausarDa�o(GestionCombate.singleton.listaUnidades.unidadesEnemigas);
				break;
			case Equipo.enemigo:
				CausarDa�o(GestionCombate.singleton.listaUnidades.unidadesAliadas);
				break;
			case Equipo.ambos:
				CausarDa�o(GestionCombate.singleton.listaUnidades.unidadesEnemigas);
				CausarDa�o(GestionCombate.singleton.listaUnidades.unidadesAliadas);
				break;
			default:
				break;
		}

		void CausarDa�o(List<Unidad> u)
		{
			float r = radio * radio;
			for (int i = 0; i < u.Count; i++)
			{
				if ((transform.position - u[i].transform.position).sqrMagnitude < r)
				{
					u[i].vida.CausarDa�o(da�o);
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
