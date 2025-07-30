using Lean.Pool;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropPlant : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private TypePlant plantType;
    [SerializeField] private Vector3 originTranform;
    [SerializeField] private RectTransform plantRect;
    [SerializeField] private RectTransform placeHolder;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.HasInstance)
        {
            if (GameManager.Instance.Money <= 0 || GameManager.Instance.IsGameOver)
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
                CreatePlaceHolder(plantRect);
                plantType.SetRectParent(placeHolder);
                plantType.SetSiblingParent();
               
            }

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (plantRect != null)
        {
            if (plantRect.TryGetComponent(out RectTransform rectTransform))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                     rectTransform,
                     eventData.position,
                     eventData.pressEventCamera,
                     out Vector2 localPoint
                 );
                // Update the position of the plant based on the mouse position
                plantRect.anchoredPosition += localPoint;
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
                ResetPlant();
            }
            else
            {
                ResetPlant();
            }
        }
        else
        {
            ResetPlant();
            Debug.Log("Không có object nào được thả lên.");

        }
    }

    private void CreatePlaceHolder(RectTransform rectTransform)
    {
        placeHolder = LeanPool.Spawn(rectTransform);
     
    }
    private void ResetPlant()
    {
        plantRect.anchoredPosition = originTranform;
        LeanPool.Despawn(placeHolder);
        plantType.SetRectParent(plantRect);
    }    

}

