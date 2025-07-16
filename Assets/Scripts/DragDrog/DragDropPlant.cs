using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropPlant : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private TypePlant plantType;
    [SerializeField] private Vector3 originTranform;
    [SerializeField] private RectTransform plantRect;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(GameManager.HasInstance)
        {
            if(GameManager.Instance.Money <= 0 || GameManager.Instance.IsGameOver)
            {
                Debug.LogWarning("không đủ tiền");
                eventData.pointerDrag = null; // Prevent dragging if not enough money
                return; // Prevent dragging if not enough money
            }
        }    
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent(out CanvasGroup canvasGroup))
            {
                canvasGroup.blocksRaycasts = false; // Prevents other UI elements from receiving events while dragging
            }

            plantType = eventData.pointerDrag.GetComponentInChildren<TypePlant>();
            if (plantType != null)
            {
                plantRect = plantType.GetComponent<RectTransform>();

                originTranform = plantRect.anchoredPosition; // Store the original position of the plant
            }

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (plantRect != null)
        {
            if (plantRect.TryGetComponent(out RectTransform rectTransform))
            {
                rectTransform.anchoredPosition += eventData.delta; // Move the plant with the mouse pointer
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        // Chuyển vị trí chuột từ màn hình -> thế giới
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Tile tile))
            {
                tile.SetPlant(plantType);
                plantRect.anchoredPosition = originTranform;
            }
            else
            {
                plantRect.anchoredPosition = originTranform;
            }
        }
        else
        {
            plantRect.anchoredPosition = originTranform;
            Debug.Log("Không có object nào được thả lên.");
        }
    }

}

