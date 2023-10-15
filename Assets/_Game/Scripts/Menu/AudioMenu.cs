using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public static AudioMenu singleton;

	public AudioSource[] sonidos;

	private void Awake()
	{
		if (singleton == null)
		{
            DontDestroyOnLoad(gameObject);
            singleton = this;
		}
		else
		{
            DestroyImmediate(gameObject);
		}
	}

	public void Destruir()
	{
		Destroy(gameObject);
	}

	public void ReproducirSonido(int cual)
	{
		if (cual >= 0 && cual < sonidos.Length)
		{
			sonidos[cual].Play();
		}
	}

}

