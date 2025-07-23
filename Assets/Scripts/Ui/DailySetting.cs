using UnityEngine;
using Lean.Pool;
using TMPro;

public class DailySetting : MonoBehaviour
{
    [SerializeField] private GameObject slotPref;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private RectTransform rect;
    [SerializeField] private int slotCount;


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
