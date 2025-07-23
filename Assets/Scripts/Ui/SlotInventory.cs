using Lean.Pool;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SlotInventory : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private RectTransform panel;
    [SerializeField] private GameObject slotInventoryPref;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitSlot();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitSlot()
    {
        // Xóa tất cả các slot (kể cả inactive) khỏi panel
        for (int i = panel.childCount - 1; i >= 0; i--)
        {
            var child = panel.GetChild(i).gameObject;
            LeanPool.Despawn(child);
        }

        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                GameObject obj = LeanPool.Spawn(slotInventoryPref, panel);
                obj.name = $"SlotInven {i},{j}";
            }    
        } 
            
    }    
}
