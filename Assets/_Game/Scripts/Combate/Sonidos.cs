using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonidos : MonoBehaviour
{
    public AudioSource audioSource;

	private void Start()
	{
		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
	}

	public void ReproducirSonido()
	{
		if (audioSource != null)
		{
			audioSource.Play();
		}
	}
}
