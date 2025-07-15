using NUnit.Compatibility;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private TypePlant typePlant;
    [SerializeField] private GameObject plantPref;

    public void SetPlant(TypePlant typePlant)
    {
        this.typePlant = typePlant;
        InitPlant();
    }

    public void InitPlant()
    {
        switch (typePlant.GetPlantType())
        {
            case PlantType.PLANT_GUN:
                {
                    CreatePlant(typePlant.GetPlantType());
                }
                break;
            case PlantType.PLANT_POWER:
                {
                    CreatePlant(typePlant.GetPlantType());
                }
                break;

            default:
                Debug.LogWarning("Unknown plant type");
                break;
        }
    }

    public void CreatePlant(PlantType plantType)
    {
        if (PlantManager.HasInstance)
        {
            GameObject obj = PlantManager.Instance.GetPlantPrefabs(plantType);
            if (obj != null)
            {
                plantPref = Instantiate(obj, transform.position, Quaternion.identity);
                plantPref.transform.SetParent(transform);
                plantPref.transform.localPosition = Vector3.zero; // Reset position to the tile's position
                if(ListenerManager.HasInstance)
                {
                 
                    ListenerManager.Instance.BroadCast(ListenType.PLANT_CREATE, typePlant);
                }    
            }
            else
            {
                Debug.LogWarning($"Plant prefab not found for type: {plantType}");
            }

        }
    }


}
