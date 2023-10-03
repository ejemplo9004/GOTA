using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    public ScriptableCard card;

    private void Start()
    {
        GetComponent<Image>().sprite = card.cardSprite;
    }

    public bool InstanteateUnity(Vector3 pos)
    {
        GameObject unit = Instantiate(card.prefab, pos, Quaternion.identity);
        return unit != null;
    }
}
