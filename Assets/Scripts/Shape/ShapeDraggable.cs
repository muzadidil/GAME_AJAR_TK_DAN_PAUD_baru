using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShapeDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string shapeType;

    [SerializeField] private Image image;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private ShapeSorterGameManager gameManager;

    public void Setup(string type, Sprite sprite, ShapeSorterGameManager manager, Canvas parentCanvas)
    {
        shapeType = type;
        gameManager = manager;
        canvas = parentCanvas;

        if (image == null)
            image = GetComponent<Image>();

        if (image != null)
            image.sprite = sprite;

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        gameManager.HandleShapeReleased(this);
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = startPosition;
    }

    public void RecordStartPosition()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }
}