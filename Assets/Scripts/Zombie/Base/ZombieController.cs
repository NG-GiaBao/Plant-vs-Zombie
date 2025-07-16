using UnityEngine;

public class ZombieController : BaseZombieController
{
    [SerializeField] private ZombieType zombieType;
    [SerializeField] private ZombieMove zombieMove;
    [SerializeField] private ZombieHeal zombieHeal;
    [SerializeField] private ZombieCollider zombieCollider;

    private void Awake()
    {
        InitComponent();
    }
    
   
    private void FixedUpdate()
    {
        if(GameManager.HasInstance && GameManager.Instance.IsGameOver)
        {
            return;
        }
        if (zombieMove != null)
        {
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
    }
    
  

}
