using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    public ScriptableCard card;
    public bool hasCard;

    public void FillUICard(ScriptableCard card)
    {
        this.card = card;
        hasCard = true;
        GetComponent<Image>().sprite = card.cardSprite;
    }

    public ScriptableCard EmptyUICard()
    {
        ScriptableCard card = this.card;
        this.card = null;
        hasCard = false;
        GetComponent<Image>().sprite = null;
        return card;
    }

    public ScriptableCard InstanteateUnity(Vector3 pos)
    {
        if(!hasCard) return null;
        GameObject unit = Instantiate(card.prefab, pos, Quaternion.identity);
        return EmptyUICard(); 
    }

   
}
