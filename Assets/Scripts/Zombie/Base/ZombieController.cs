using Lean.Pool;
using UnityEngine;
using DG.Tweening;
using System;

public class ZombieController : BaseZombieController
{
    [SerializeField] private ZombieType zombieType;
    [SerializeField] private ZombieMove zombieMove; public ZombieMove ZombieMove => zombieMove;
    [SerializeField] private ZombieHeal zombieHeal; public ZombieHeal ZombieHeal => zombieHeal;
    [SerializeField] private ZombieCollider zombieCollider;
    [SerializeField] private SpriteRenderer spriteRenderer; public SpriteRenderer SpriteRenderer => spriteRenderer;

    [SerializeField] private bool isDeath; public bool IsDeath => isDeath;

    private Action onSpawn; public Action OnSpawn => onSpawn;
        
    private Action onDeSpawn; public Action OnDeSpawn => onDeSpawn;

    private void Awake()
    {
        InitComponent();
    }
    private void Start()
    {
        onSpawn = () => Spawn();
        onDeSpawn = () => Despawn();
    }


    private void FixedUpdate()
    {
        if (GameManager.HasInstance && GameManager.Instance.IsGameOver)
        {
            return;
        }
        if (zombieMove != null)
        {
            if (isDeath) return;
            zombieMove.Move();
        }
    }

    public ZombieType GetZombieType()
    {
        return zombieType;
    }
    private void InitComponent()
    {
        zombieMove = GetComponent<ZombieMove>();
        zombieHeal = GetComponent<ZombieHeal>();
        zombieCollider = GetComponent<ZombieCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Spawn()
    {
        Debug.Log("zombie xuất hiện");
        isDeath = false;
        FadeColor(false, 0.5f);
        zombieHeal.SetHeal(3);
    }

    public void Despawn()
    {
        Debug.Log("zombie đã chết");
        isDeath = true;
        FadeColor(true, 0.5f);
    }

    public void FadeColor(bool isFade, float time)
    {
        if (isFade)
        {
            spriteRenderer.DOFade(0, time).SetEase(Ease.Linear);
        }
        else
        {
            spriteRenderer.DOFade(1, time).SetEase(Ease.Linear);
        }
    }
}
