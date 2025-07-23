using Lean.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private List<GameObject> uiScreen;
    [SerializeField] private List<GameObject> uiPopup;
    [SerializeField] private List<GameObject> uiNotify;
    [SerializeField] private GameObject screenContainer;
    [SerializeField] private GameObject popupContainer;
    [SerializeField] private GameObject notifyContainer;
    [SerializeField] private RectTransform image;
    [SerializeField] private bool switchParent;
    [SerializeField] private bool switchCanvas;
    [SerializeField] private InputAction button;
    [SerializeField] private InputAction presser;
    [SerializeField] private Vector2 localPoint;

    [SerializeField] private GameObject itemPref;
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 originRect;
    [SerializeField] private GameObject ground;

    [SerializeField] private Type type;
    [SerializeField] private Component component;



    // Start is cal led once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject uiPrefab in uiScreen)
        {
            GameObject uiInstance = Instantiate(uiPrefab);
            uiInstance.transform.SetParent(screenContainer.transform, false);
        }
        foreach (GameObject uiPrefab in uiPopup)
        {
            GameObject uiInstance = Instantiate(uiPrefab);
            uiInstance.transform.SetParent(popupContainer.transform, false);
        }
        foreach (GameObject uiPrefab in uiNotify)
        {
            GameObject uiInstance = Instantiate(uiPrefab);
            uiInstance.transform.SetParent(notifyContainer.transform, false);
        }

        button.Enable();
        button.performed += ctx =>
        {
            switchParent = !switchParent;
            Debug.Log($"Switch Parent: {switchParent}");
        };
        presser.Enable();
        presser.performed += ctx =>
        {
            switchCanvas = !switchCanvas;
            Debug.Log($"Switch Canvas: {switchCanvas}");
        };

    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
             image,
             eventData.position,
             null,
             out localPoint
         );
        image.anchoredPosition += localPoint;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originRect = image.anchoredPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPoint.z = 0; // Ensure the z-coordinate is set to 0 for 2D placement
        GameObject obj = LeanPool.Spawn(itemPref, worldPoint, Quaternion.identity);
        LeanPool.Despawn(obj, 2f);
        image.anchoredPosition = originRect;


    }
}

