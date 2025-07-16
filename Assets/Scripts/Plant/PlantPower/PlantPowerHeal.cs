using System.Collections;
using UnityEngine;

public class PlantPowerHeal : BasePlantHeal
{
    [SerializeField] private bool isAttacked;
    public override void ReceiveHeal(int damage)
    {
        if(!isAttacked)
        {
            healAmount -= damage;
            isAttacked= true;
            if (healAmount <= 0)
            {
                StopAllCoroutines();
                healAmount = 0;
                Destroy(gameObject);
                Debug.Log("Plant fully healed and destroyed.");
            }
            StartCoroutine(DelayAttacked());
        }
    }
    IEnumerator DelayAttacked()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacked = false;
    }
}
