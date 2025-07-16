using UnityEngine;

public class PlantGun : BasePlant
{
    [SerializeField] private PlantGunAttack gunAttack;
    [SerializeField] private PlantGunHeal gunHeal;
    public override PlantType GetPlantType()
    {
      return plantType;
    }
    private void Awake()
    {
        InitComponent();
    }
    private void InitComponent()
    {
        if (gunAttack == null)
        {
            gunAttack = GetComponent<PlantGunAttack>();
        }
        if (gunHeal == null)
        {
            gunHeal = GetComponent<PlantGunHeal>();
        }
    }    

}
