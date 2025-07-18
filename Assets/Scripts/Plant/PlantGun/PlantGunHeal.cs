using Lean.Pool;
using UnityEngine;

public class PlantGunHeal : BasePlantHeal
{

    public override void ReceiveHeal(int damage)
    {
        healAmount -= damage;
        if (healAmount <= 0)
        {
            healAmount = 0;
            LeanPool.Despawn(gameObject);
            Debug.Log("Plant fully healed and destroyed.");
        }
    }
}
