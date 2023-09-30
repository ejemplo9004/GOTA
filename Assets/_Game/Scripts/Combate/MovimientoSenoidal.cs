using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSenoidal : MonoBehaviour
{
    public float frecuencia;
    public Vector3 amplitud;

    Vector3 posInicial;

    void Start()
    {
        posInicial = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = posInicial + amplitud * Mathf.Sin(Time.time * frecuencia);
    }
}
