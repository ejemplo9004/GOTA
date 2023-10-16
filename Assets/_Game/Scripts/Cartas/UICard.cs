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
    private CardLoadEffect loadEffect;
    [SerializeField] private Text costText;

    public void FillUICard(ScriptableCard card)
    {
        this.card = card;
        hasCard = true;
        image = GetComponent<Image>();
        image.sprite = card.cardSprite;
        wasColored = card.cost < CardCombatController.Instance.energy;
        loadEffect = GetComponentInChildren<CardLoadEffect>();
        costText.text = card.cost.ToString();

        int spriteSheetNumber = int.Parse(card.cardSprite.name.Substring(11, 2));
        if (spriteSheetNumber == 21)
        {
            disableColor = CardCombatController.Instance.disabledMaterial21;
        }
        else
        {
            disableColor = CardCombatController.Instance.disabledMaterial22;
        }
        image.material = (wasColored) ? null : disableColor;
    }

    public ScriptableCard EmptyUICard()
    {
        ScriptableCard card = this.card;
        this.card = null;
        hasCard = false;
        image.sprite = null;
        return card;
    }

    public ScriptableCard InstanteateUnit(Vector3 pos, Hexagon hexagon)
    {
        if (!hasCard) return null;
        if (card.cost > CardCombatController.Instance.energy) return null;
        if (hexagon == null) return null;
        if (hexagon.Team == Equipo.enemigo && !card.cartaEspecial) return null;
        if(!hexagon.Walkable && GetUnitInPrefab(card.prefab).tipoUnidad != TipoUnidad.voladora) return null;

        GameObject unit = Instantiate(card.prefab,
            new Vector3(hexagon.transform.position.x, pos.y, hexagon.transform.position.z),
            Quaternion.identity);
        Equipador e = unit.GetComponent<Equipador>();
        if (e != null) e.Inicializar(Equipo.aliado);

        CardCombatController.Instance.energy -= card.cost;
        return EmptyUICard();
    }

    private void Update()
    {
        if (card != null)
        {
            float energy = CardCombatController.Instance.energy;
            bool coloredCard = card.cost < energy;
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
                loadEffect.SetImageFill(0);
                wasColored = coloredCard;
            }
            if (!wasColored)
            {
                loadEffect.SetImageFill(
                    (card.cost - energy) / card.cost);
            }

        }
    }

    private UnidadInfanteria GetUnitInPrefab(GameObject unit)
    {
        UnidadInfanteria u = unit.GetComponent<UnidadInfanteria>();
        if (u != null) return u;
        u = GetComponentInChildren<UnidadInfanteria>();
        return u;
    }
}
