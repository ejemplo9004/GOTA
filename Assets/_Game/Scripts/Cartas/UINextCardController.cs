using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINextCardController : MonoBehaviour
{
    private Image nextCardImageContainer;

    private void Awake()
    {
        nextCardImageContainer = GetComponent<Image>();
    }

    public void SetNextCard(ScriptableCard card)
    {
        nextCardImageContainer.sprite = card.cardSprite;
    }
}
