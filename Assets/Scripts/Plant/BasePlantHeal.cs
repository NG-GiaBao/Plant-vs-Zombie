using UnityEngine;

public abstract class BasePlantHeal : MonoBehaviour
{
    [SerializeField] protected int healAmount;
    public abstract void ReceiveHeal(int damage);
}
