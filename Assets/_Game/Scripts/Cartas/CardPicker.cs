using UnityEngine;
using UnityEngine.EventSystems;

public class CardPicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform cardRectTransform;
    private Vector2 cardOrigin, offset;
    private bool isDraggin;
    private int heightLimit;
    private Vector2 bigCardPosition;
    private Camera _camera;
    private Vector3 pointerPosition;

    private void OnEnable()
    {
        cardRectTransform = transform.parent.GetComponent<RectTransform>();

        cardOrigin = cardRectTransform.anchoredPosition;
        heightLimit = Mathf.FloorToInt(Screen.height * CardUISingleton.Instance.screenPorcentajeLimitToPlayCard);
        bigCardPosition = new Vector2(Screen.width / 2, heightLimit + 200);

        _camera = Camera.main;
    }

    private void Update()
    {
        if (isDraggin)
        {
            OnDragCard();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDraggin = true;
        Vector3 mouse = Input.mousePosition;
        offset = cardRectTransform.anchoredPosition - new Vector2(mouse.x, mouse.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDraggin = false;
        cardRectTransform.anchoredPosition = cardOrigin;
        CardUISingleton.Instance.lineController.enabled = false;

        var card = GetComponent<UICard>();
        if (card != null)
        {
            ScriptableCard cardPlayed = card.InstanteateUnity(pointerPosition);
            if (cardPlayed != null)
            {
                CardCombatController.Instance.OnCardPlayed(cardPlayed);
            }
        }
    }

    public void OnDragCard()
    {
        Vector3 mouse = Input.mousePosition;
        if (mouse.y > heightLimit)
        {
            cardRectTransform.anchoredPosition = bigCardPosition;
            ShowArrow();
        }
        else
        {
            cardRectTransform.anchoredPosition = new Vector2(mouse.x, mouse.y) + offset;
        }

    }

    private void ShowArrow()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = CardUISingleton.Instance.worldMask;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, mask))
        {
            if (CardUISingleton.Instance.arrowInstance == null)
            {
                CardUISingleton.Instance.arrowInstance = Instantiate(CardUISingleton.Instance.arrowPrefab);
            }
            GameObject arrowInstance = CardUISingleton.Instance.arrowInstance;
            arrowInstance.SetActive(true);
            arrowInstance.transform.position = hit.point;
            CardUISingleton.Instance.lineController.enabled = true;
            try
            {
                CardUISingleton.Instance.lineController.SetEndPoint(hit.point);
            }catch(System.Exception e)
            {
                Debug.LogException(e);
                Debug.Log(e.StackTrace);
            }
                pointerPosition = hit.point;

        }
    }
}
