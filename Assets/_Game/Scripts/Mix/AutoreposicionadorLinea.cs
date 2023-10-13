using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoreposicionadorLinea : MonoBehaviour
{
    public Transform puntoInicial;
    public Transform camara;
    public Vector3 offset;
    void Start()
    {
        puntoInicial.position = camara.position + offset;
    }
}
