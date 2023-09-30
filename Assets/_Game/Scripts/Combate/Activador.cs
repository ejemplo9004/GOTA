using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activador : MonoBehaviour
{
    public UnityEvent evento1;
    public UnityEvent evento2;
    public UnityEvent evento3;
    public UnityEvent evento4;

    public void ActivarEvento1()
    {
        evento1.Invoke();

    }
    public void ActivarEvento2()
    {
        evento2.Invoke();

    }
    public void ActivarEvento3()
    {
        evento3.Invoke();

    }
    public void ActivarEvento4()
    {
        evento4.Invoke();

    }
}
