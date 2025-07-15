using UnityEngine;

public class TypePlant : MonoBehaviour
{
    [SerializeField] private PlantType plantType;
   

    public PlantType GetPlantType()
    {
        return plantType;
    }    
}
