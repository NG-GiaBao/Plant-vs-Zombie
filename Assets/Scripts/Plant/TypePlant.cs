using UnityEngine;

public class TypePlant : MonoBehaviour
{
    [SerializeField] private PlantType plantType;
    [SerializeField] private int cost;


    public PlantType GetPlantType()
    {
        return plantType;
    }    
    public int GetCost()
    {
        return cost;
    }
}
