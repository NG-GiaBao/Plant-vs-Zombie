using System;
using UnityEngine;

public  class BasePlant : MonoBehaviour
{
    [SerializeField] protected PlantType plantType;
    [SerializeField] protected BasePlantHeal basePlantHeal;
    protected Action onSpawn; public Action OnSpawn => onSpawn;
    protected Action onDeSpawn; public Action OnDeSpawn => onDeSpawn;

    protected virtual void Awake()
    {
        basePlantHeal = GetComponent<BasePlantHeal>();
    }

    public virtual PlantType GetPlantType()
    {
        return plantType;
    }
    protected virtual void Spawn()
    {

    }
    protected virtual void DeSpawn()
    {

    }
}
