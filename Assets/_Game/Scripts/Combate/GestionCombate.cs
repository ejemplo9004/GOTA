using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionCombate : MonoBehaviour
{
    public static GestionCombate singleton;
    public ListasObjetivos listaUnidades;

    public float    puntosAliados;
    public float    puntosEnemigos;
    public Slider   slScore;
    public Text     txtTiempo;

    public int tiempo = 300;

    public bool inGame = true;
    public GameObject[] desactivarEnGameOver;

    private void Awake()
	{
        singleton = this;
		if (AudioMenu.singleton != null)
		{
            AudioMenu.singleton.Destruir();
		}
	}
	private void Start()
	{
        StartCoroutine(Cronometro());
	}
	public void SumarPuntos(Equipo e, float pts)
	{
		switch (e)
		{
			case Equipo.aliado:
                puntosEnemigos += (pts/10f);
				break;
			case Equipo.enemigo:
                puntosAliados += (pts/10f);
                break;
			case Equipo.ambos:
				break;
			default:
				break;
		}
		if (slScore!= null)
		{
            
			if (!enAccion)
			{
                StartCoroutine(SumarPuntosVisual());
			}
		}
	}
    bool enAccion;
    IEnumerator SumarPuntosVisual()
	{
        enAccion = true;
		while (Mathf.Abs(slScore.value - puntosAliados)>0.01f)
		{
            slScore.maxValue = Mathf.Lerp(slScore.maxValue, puntosAliados + puntosEnemigos, 0.3f);
            slScore.value = Mathf.Lerp(slScore.value, puntosAliados + 0f, 0.2f);
            yield return new WaitForEndOfFrame();
        }
        enAccion = false;
	}

    IEnumerator Cronometro()
	{
        tiempo++;
		while (tiempo > 0 && inGame)
		{
            tiempo--;
            int minutos = Mathf.FloorToInt(tiempo / 60f);
            int segundos = Mathf.FloorToInt(tiempo % 60);
            txtTiempo.text = minutos.ToString("00") + ":" + segundos.ToString("00");
            yield return new WaitForSeconds(1);
		}
		if (inGame && (slScore.value / slScore.maxValue < 0.6f && slScore.value/slScore.maxValue > 0.4f))
        {
            txtTiempo.color = Color.red;
            tiempo = 121;
            while (tiempo > 0 && inGame)
            {
                tiempo--;
                int minutos = Mathf.FloorToInt(tiempo / 60f);
                int segundos = Mathf.FloorToInt(tiempo % 60);
                txtTiempo.text = minutos.ToString("00") + ":" + segundos.ToString("00");
                yield return new WaitForSeconds(1);
            }
        }
		if (inGame)
		{
			if (slScore.value / slScore.maxValue > 0.5f)
			{
                while (listaUnidades.torresPrincipalesEnemigas.Count>0)
                {
                    listaUnidades.torresPrincipalesEnemigas[0].vida.CausarDaño(5000);
                    yield return new WaitForSeconds(0.5f);
                }
            }
			else
            {
                while (listaUnidades.torresPrincipalesAliadas.Count > 0)
                {
                    listaUnidades.torresPrincipalesAliadas[0].vida.CausarDaño(5000);
                    yield return new WaitForSeconds(0.5f);
                }
            }
		}
	}

    void Victoria()
	{
        print("victoaia");
		for (int i = 0; i < listaUnidades.torresPrincipalesEnemigas.Count; i++)
        {
            listaUnidades.torresPrincipalesEnemigas[listaUnidades.torresPrincipalesEnemigas.Count - 0 - i].vida.CausarDaño(5000);
        }
	}

    void Derrota()
    {
        print("Derrota");
        for (int i = 0; i < listaUnidades.torresPrincipalesAliadas.Count; i++)
        {
            listaUnidades.torresPrincipalesAliadas[i].vida.CausarDaño(5000);
        }
    }
    public void GameOver()
	{
        inGame = false;
		for (int i = 0; i < desactivarEnGameOver.Length; i++)
		{
            desactivarEnGameOver[i].SetActive(false);
		}
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
    public static Action onTowerDestroy;
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
                }
                else
                {
                    torresPrincipalesEnemigas.Add(t);
                }
                break;
			default:
				break;
		}
	}
    public void QuitarTorre(Torre t)
    {
        //Debug.Log("Está intentando quitar una torre llamada " + t + "/" + t.name);
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
                }
                else
                {
                    torresPrincipalesEnemigas.Remove(t);
                }
                onTowerDestroy.Invoke();
                break;
			default:
				break;
		}
	}


}
