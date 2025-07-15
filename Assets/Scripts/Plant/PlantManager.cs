using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantManager : BaseManager<PlantManager>   
{
    private Dictionary<PlantType, GameObject> plantDict = new();

    private const string PLANT_PREFAB_PATH = "Prefabs/Plant";


    private void Start()
    {
        GetPlantPrefabs();
    }

    private void GetPlantPrefabs()
    {
        GameObject[] plantPrefabs = Resources.LoadAll<GameObject>(PLANT_PREFAB_PATH);
        foreach(GameObject prefab in plantPrefabs)
        {
            PlantType plantType = prefab.GetComponent<BasePlant>().GetPlantType();
            if (!plantDict.ContainsKey(plantType))
            {
                plantDict.Add(plantType, prefab);
            }
            else
            {
                Debug.LogWarning($"Duplicate plant type found: {plantType}");
            }
            Debug.Log($"Loaded plant prefab: {plantType} , {prefab.name}");
        }
    }

    public GameObject GetPlantPrefabs(PlantType plantType)
    {
        if (plantDict.TryGetValue(plantType, out GameObject plantPrefab))
        {
            return plantPrefab;
        }
        else
        {
            Debug.LogWarning($"Plant prefab not found for type: {plantType}");
            return null;
        }
    
    }
}
