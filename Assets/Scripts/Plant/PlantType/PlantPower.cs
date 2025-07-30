using System;
using UnityEngine;

public class PlantPower : BasePlant
{
    [SerializeField] private PlantPowarMoney powarMoney;
    [SerializeField] private bool isDeath;


    protected override void Awake()
    {
        base.Awake();
        powarMoney = GetComponent<PlantPowarMoney>();
    }
    private void Start()
    {
        onSpawn = () => Spawn();
        onDeSpawn = () => DeSpawn();
    }


    private void Update()
    {
        if(GameManager.Instance.IsGameOver || isDeath)
        {
            return;
        }
        powarMoney.IncreaseMoney();
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
