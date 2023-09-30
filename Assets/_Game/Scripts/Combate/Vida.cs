using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vida : MonoBehaviour
{
    public float vidaMaxima;
    public float vidaActual;
	public UnityEvent eventoMuerte;
	public bool vivo = true;

	private void Start()
	{
		vidaActual = vidaMaxima;
	}

	public void CausarDaño(float cuanto)
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
	}

	public void Morir()
	{
		eventoMuerte.Invoke();
		vidaActual = 0;
		vivo = false;
	}
}
