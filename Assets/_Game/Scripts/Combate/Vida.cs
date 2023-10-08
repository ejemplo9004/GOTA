using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Vida : MonoBehaviour
{
    public float vidaMaxima;
    public float vidaActual;
	public UnityEvent eventoMuerte;
	public bool vivo = true;
	public Image imVida;

	private void Start()
	{
		if (imVida == null)
		{
			imVida = GetComponentInChildren<Image>();
		}
		vidaActual = vidaMaxima;
	}

	public void CausarDaņo(float cuanto)
	{
		if (!vivo)
		{
			return;
		}
		vidaActual -= cuanto;
		if (vidaActual<=0)
		{
			Morir();
		}
		ActualizarInterfaz();
	}

	public void ActualizarInterfaz()
	{
		if (imVida != null)
		{
			imVida.fillAmount = vidaActual / vidaMaxima;
		}
	}

	public void Morir()
	{
		eventoMuerte.Invoke();
		vidaActual = 0;
		vivo = false;
	}
}
