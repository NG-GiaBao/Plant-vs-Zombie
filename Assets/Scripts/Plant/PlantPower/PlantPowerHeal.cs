using Lean.Pool;
using System.Collections;
using UnityEngine;

public class PlantPowerHeal : BasePlantHeal
{
    [SerializeField] private bool isAttacked;
    [SerializeField] private float escapleTime;
    [SerializeField] private PlantPower plantPower;
    private void Awake()
    {
        plantPower = GetComponent<PlantPower>();
    }

    private void Update()
    {
        if (!isAttacked) return;
        StartTime();
    }
    public override void ReceiveHeal(int damage)
    {
        if (!isAttacked)
        {
            healAmount -= damage;
            isAttacked = true;
            if (healAmount <= 0)
            {
                healAmount = 0;
                LeanPool.Despawn(gameObject);
                if(plantPower != null)
                {
                    plantPower.OnDeSpawn?.Invoke();
                }
                Debug.Log("Plant fully healed and destroyed.");
            }
        }
        
    }
    private void StartTime()
    {
        escapleTime += Time.deltaTime;
        if (escapleTime >= 0.5f)
        {
            isAttacked = false;
            escapleTime = 0f;
        }
    }
}
