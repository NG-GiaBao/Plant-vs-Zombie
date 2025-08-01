using UnityEngine;
using Lean.Pool;

public class ZombieHeal : BaseZombieHeal
{
    [SerializeField] private float healAmount;
    [SerializeField] private ZombieController zombieController;

    private void Awake()
    {
        zombieController = GetComponent<ZombieController>();
    }
    public void ReceiveDamage(int damage)
    {
        healAmount -= damage;
        if (healAmount < 0)
        {
            healAmount = 0;
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.ZOMBIE_DEAD, zombieController.GetZombieType());
            }
            zombieController.OnDeSpawn?.Invoke();
            LeanPool.Despawn(gameObject, 0.5f);

        }
    }
    public void SetHeal(int heal)
    {
        healAmount = heal;
    }
}
