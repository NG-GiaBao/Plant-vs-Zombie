using Lean.Pool;
using UnityEngine;

public class PlantGunHeal : BasePlantHeal
{
    [SerializeField] private bool isAttacked;
    [SerializeField] private float escapleTime;
    [SerializeField] private PlantGun plantGun;

    private void Awake()
    {
        plantGun = GetComponent<PlantGun>();
    }
    public override void ReceiveHeal(int damage)
    {
        StartTime();
        if (!isAttacked)
        {
            healAmount -= damage;
            isAttacked = true;
            if (healAmount <= 0)
            {
                healAmount = 0;
                plantGun.OnDeSpawn?.Invoke();
                LeanPool.Despawn(gameObject);
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
