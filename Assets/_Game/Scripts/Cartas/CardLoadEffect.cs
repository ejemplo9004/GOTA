using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLoadEffect : MonoBehaviour
{
    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void SetImageFill(float fill)
    {
        image.fillAmount = fill;
    }
}
