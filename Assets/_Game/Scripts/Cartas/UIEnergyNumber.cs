using UnityEngine;
using UnityEngine.UI;

public class UIEnergyNumber : MonoBehaviour
{
    private Text Text;

    private void OnEnable()
    {
        Text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        Text.text = Mathf.FloorToInt(CardCombatController.Instance.energy).ToString();
    }
}
