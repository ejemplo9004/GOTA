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
        heightLimit = Mathf.FloorToInt(Screen.height * CardControllerSingleton.Instance.screenPorcentajeLimitToPlayCard);
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
        CardControllerSingleton.Instance.lineController.enabled = false;

        var card = GetComponent<UICard>();
        if (card != null)
        {
            card.InstanteateUnity(pointerPosition);
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
        LayerMask mask = CardControllerSingleton.Instance.worldMask
            ;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, mask))
        {
            GameObject arrowInstance = CardControllerSingleton.Instance.arrowInstance;
            if (arrowInstance == null)
            {
                CardControllerSingleton.Instance.arrowInstance = Instantiate(CardControllerSingleton.Instance.arrowPrefab);
            }
            arrowInstance.SetActive(true);
            arrowInstance.transform.position = hit.point;
            CardControllerSingleton.Instance.lineController.enabled = true;
            CardControllerSingleton.Instance.lineController.SetEndPoint(hit.point);
            pointerPosition = hit.point;
        }
    }
}
