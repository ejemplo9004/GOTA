using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCombate : MonoBehaviour
{
    public static GestionCombate singleton;
    public ListasObjetivos listaUnidades;

	private void Awake()
	{
        singleton = this;
	}
	void Start()
    {
        
    }
    void Update()
    {
        
    }
}

[System.Serializable]
public class ListasObjetivos
{
    public List<Unidad> unidadesAliadas;
    public List<Unidad> unidadesEnemigas;
    public List<Torre>  torresAliadas;
    public List<Torre>  torresEnemigas;
    public List<Torre>  torresPrincipalesAliadas;
    public List<Torre> torresPrincipalesEnemigas;
    public void AñadirUnidad(Unidad u)
    {
        if (u.equipo == Equipo.aliado)
        {
            unidadesAliadas.Add(u);
        }
        else
        {
            unidadesEnemigas.Add(u);
        }
    }
    public void QuitarUnidad(Unidad u)
    {
        if (u.equipo == Equipo.aliado)
        {
            unidadesAliadas.Remove(u);
        }
        else
        {
            unidadesEnemigas.Remove(u);
        }
    }
    public void AñadirTorre(Torre t)
    {
		switch (t.tipoTorre)
		{
			case TipoTorre.generica:
                if (t.equipo == Equipo.aliado)
                {
                    torresAliadas.Add(t);
                }
                else
                {
                    torresEnemigas.Add(t);
                }
                break;
			case TipoTorre.principal:
                if (t.equipo == Equipo.aliado)
                {
                    torresPrincipalesAliadas.Add(t);
                    torresAliadas.Add(t);
                }
                else
                {
                    torresPrincipalesEnemigas.Add(t);
                    torresEnemigas.Add(t);
                }
                break;
			default:
				break;
		}
	}
    public void QuitarTorre(Torre t)
    {
		switch (t.tipoTorre)
		{
			case TipoTorre.generica:
                if (t.equipo == Equipo.aliado)
                {
                    torresAliadas.Remove(t);
                }
                else
                {
                    torresEnemigas.Remove(t);
                }
                break;
			case TipoTorre.principal:
                if (t.equipo == Equipo.aliado)
                {
                    torresPrincipalesAliadas.Remove(t);
                    torresAliadas.Remove(t);
                }
                else
                {
                    torresPrincipalesEnemigas.Remove(t);
                    torresEnemigas.Remove(t);
                }
                break;
			default:
				break;
		}
	}


}
