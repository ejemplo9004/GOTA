using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    public ScriptableCard card;
    public bool hasCard;
    private bool wasColored;
    private Image image;
    private Material disableColor;

    public void FillUICard(ScriptableCard card)
    {
        this.card = card;
        hasCard = true;
        image = GetComponent<Image>();
        image.sprite = card.cardSprite;
        disableColor = CardCombatController.Instance.disabledMaterial;
        wasColored = card.cost < CardCombatController.Instance.energy;
        image.material = (wasColored)? null : disableColor;
    }

    public ScriptableCard EmptyUICard()
    {
        ScriptableCard card = this.card;
        this.card = null;
        hasCard = false;
        image.sprite = null;
        return card;
    }

    public ScriptableCard InstanteateUnity(Vector3 pos)
    {
        if(!hasCard) return null;
        if(card.cost > CardCombatController.Instance.energy) return null;
        GameObject unit = Instantiate(card.prefab, pos, Quaternion.identity);
        CardCombatController.Instance.energy -= card.cost;
        return EmptyUICard(); 
    }

    private void Update()
    {
        if(card != null)
        {
            bool coloredCard = card.cost < CardCombatController.Instance.energy;
            if (coloredCard != wasColored)
            {
                if (wasColored)
                {
                    image.material = disableColor;
                }
                else
                {
                    image.material = null;
                }
                wasColored = coloredCard;
            }
        }
    }

   
}
