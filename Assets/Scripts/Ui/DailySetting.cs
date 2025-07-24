using UnityEngine;
using Lean.Pool;
using TMPro;
using UnityEngine.UI;

public class DailySetting : MonoBehaviour
{
    [SerializeField] private GameObject slotPref;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private RectTransform rect;
    [SerializeField] private int slotCount;
    [SerializeField] private GridLayoutGroup grid;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rows = Mathf.RoundToInt(rect.rect.width / 100 - 1);
        //columns = Mathf.RoundToInt(rect.rect.height / 100 - 1);
        InitSlot();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitSlot()
    {
        RectTransform rectTransform = grid.GetComponent<RectTransform>();
        float cellWidth = (rectTransform.rect.width - grid.padding.left - grid.padding.right - grid.spacing.x * (columns - 1)) / columns;
        float cellHeight = (rectTransform.rect.height - grid.padding.top - grid.padding.bottom - grid.spacing.y * (rows - 1)) / rows;
        grid.cellSize = new Vector2(cellWidth, cellHeight);
        grid.constraintCount = columns;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject slot = LeanPool.Spawn(slotPref, rect);
                slot.name = $"Slot_{i}_{j}";
                slotCount++;
                TextMeshProUGUI text = slot.GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                {
                    text.text = $"{slotCount}";
                }
                else
                {
                    Debug.LogWarning($"TextMeshProUGUI component not found in {slot.name}. Please ensure the prefab has a TextMeshProUGUI component.");
                }
            }
        }
    }

}
