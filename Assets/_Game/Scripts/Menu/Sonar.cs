using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    public void ReproducirSonido(int cual)
	{
		if (AudioMenu.singleton != null)
		{
			AudioMenu.singleton.ReproducirSonido(cual);
		}
	}

	public void Destruir()
	{
		if (AudioMenu.singleton != null)
		{
			AudioMenu.singleton.Destruir();
		}
	}
}
