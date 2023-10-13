using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    private Slider m_Slider;

    private void OnEnable()
    {
        m_Slider = GetComponent<Slider>();
    }

    private void Update()
    {
        m_Slider.value = CardCombatController.Instance.energy / 10;

    }
}
