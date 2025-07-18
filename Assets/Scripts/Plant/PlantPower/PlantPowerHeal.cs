using Lean.Pool;
using System.Collections;
using UnityEngine;

public class PlantPowerHeal : BasePlantHeal
{
    [SerializeField] private bool isAttacked;
    [SerializeField] private float escapleTime;

    private void Update()
    {
        escapleTime += Time.deltaTime;
    }
    public override void ReceiveHeal(int damage)
    {
        StartTime();
        if (!isAttacked)
        {
            healAmount -= damage;
            isAttacked= true;
            if (healAmount <= 0)
            {
                healAmount = 0;
                LeanPool.Despawn(gameObject);
                Debug.Log("Plant fully healed and destroyed.");
              
            }
        }
    }
    private void StartTime()
    {
            if (escapleTime >= 0.5f)
            {
                isAttacked = false;
                escapleTime = 0f;
            }
    }    

    //IEnumerator DelayAttacked()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    isAttacked = false;
    //}
}
