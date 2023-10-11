using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Diccionario))]
public class DiccionarioEditor : Editor
{
	public override void OnInspectorGUI()
	{
		//d.idiomaSeleccionado = (MenuIdioma)PlayerPrefs.GetInt("idioma");
		base.OnInspectorGUI();
		if (GUILayout.Button("Cambiar Idioma"))
		{
			Diccionario d = (Diccionario)target;
			d.GuardarIdioma();
		}
		if (GUILayout.Button("Eliminar Preferencia de Idioma"))
		{
			PlayerPrefs.DeleteKey("idioma");
		}
	}
}

