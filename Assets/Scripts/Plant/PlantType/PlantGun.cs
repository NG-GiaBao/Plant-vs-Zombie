using UnityEngine;

public class PlantGun : BasePlant
{
    [SerializeField] private PlantGunAttack gunAttack;
    [SerializeField] private bool isDeath;

    protected override void Awake()
    {
        base.Awake();
        gunAttack = GetComponent<PlantGunAttack>();
    }
    private void Start()
    {
        onSpawn = () => Spawn();
        onDeSpawn = () => DeSpawn();
    }
    private void Update()
    {
        if (GameManager.HasInstance )
        {
            if (GameManager.Instance.IsGameOver || isDeath) return;
        }
        if (gunAttack != null)
        {
            gunAttack.InitRaycast();
        }
    }
    protected override void Spawn()
    {
        base.Spawn();
    }
    protected override void DeSpawn()
    {
        base.DeSpawn();
        isDeath = true;
    }
}
