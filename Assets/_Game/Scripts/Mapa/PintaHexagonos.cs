using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PintaHexagonos : MonoBehaviour
{
    public SpriteRenderer spColor;
    public Color colorBien = Color.green;
    public Color colorMal = Color.red;
    Hexagon h;

    public static Equipo equipo;
    Color c;
    public bool visible;
    void Start()
    {
        h = GetComponentInParent<Hexagon>();
        colorBien.a = 0;
        colorMal.a = 0;
        spColor.color = colorBien;
    }

    void Update()
    {
        
    }

    [ContextMenu("Marcar")]
    public void Marcar()
	{
		if (h != null)
		{
            return;
		}
		if (h.Team == equipo || equipo == Equipo.ambos)
		{
            c = colorBien;
		}
		else
		{
            c = colorMal;
		}
        visible = true;
        StartCoroutine(Pintar());
	}
    [ContextMenu("Desmarcar")]
    public void Desmarcar()
	{
        visible = false;
	}

    IEnumerator Pintar()
	{
        spColor.color = c;
		for (int i = 0; i < 20; i++)
		{
            c.a = i / 20f;
            spColor.color = c;
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitUntil(() => !visible);

        for (int i = 0; i <= 20; i++)
        {
            c.a = (i-20) / 20f;
            spColor.color = c;
            yield return new WaitForSeconds(0.04f);
        }
    }
}
