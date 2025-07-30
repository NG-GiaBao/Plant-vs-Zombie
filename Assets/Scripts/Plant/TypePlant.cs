using UnityEngine;

public class TypePlant : MonoBehaviour
{
    [SerializeField] private PlantType plantType;
    [SerializeField] private int cost;
    [SerializeField] private RectTransform rectParent;
    [SerializeField] private int indexChild;

    private void Start()
    {
        rectParent = GetComponentInParent<RectTransform>().parent as RectTransform;
        indexChild = transform.GetSiblingIndex();
    }

    public PlantType GetPlantType()
    {
        return plantType;
    }    
    public int GetCost()
    {
        return cost;
    }
    public void SetRectParent(RectTransform child)
    {
        child.SetParent(rectParent, false);
        child.SetSiblingIndex(indexChild);
    }    
    public void SetSiblingParent()
    {
        transform.SetParent(rectParent.parent,false);
    }    
}
