using UnityEngine;

public abstract class BasePlant : MonoBehaviour
{
    [SerializeField] protected PlantType plantType;
   
    public abstract PlantType GetPlantType();
}
