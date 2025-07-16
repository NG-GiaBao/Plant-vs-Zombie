using UnityEngine;

public class PlantPower : BasePlant
{
    [SerializeField] private BasePlantHeal plantHeal;
    [SerializeField] private PlantPowarMoney powarMoney;

    private void Awake()
    {
        plantHeal = GetComponent<BasePlantHeal>();
        powarMoney = GetComponent<PlantPowarMoney>();
    }

    private void Update()
    {
        if(GameManager.Instance.IsGameOver)
        {
            StopAllCoroutines();
            return;
        }
        powarMoney.CallCouroutineMoney();
    }
    public override PlantType GetPlantType()
    {
       return plantType;
    }

   
}
