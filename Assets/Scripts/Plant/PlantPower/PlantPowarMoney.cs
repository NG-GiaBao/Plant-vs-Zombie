using System.Collections;
using UnityEngine;

public class PlantPowarMoney : MonoBehaviour
{
    [SerializeField] private int RateMoney = 5; // Số tiền nhận được mỗi giây
    [SerializeField] private float timeIncreaseMoney = 3f; // Thời gian giữa các lần tăng tiền
    [SerializeField] private float elapsedTime;


    private enum PlantPower
    {
        Start,
        Spawning,
        End,
    }

    private void OnIncreaseMoney()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.PLANT_CREATE_MONEY, RateMoney);
        }
    }

    public void IncreaseMoney()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeIncreaseMoney)
        {
            OnIncreaseMoney();
            elapsedTime = 0;
        }

    }



}
