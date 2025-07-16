using System.Collections;
using UnityEngine;

public class PlantPowarMoney : MonoBehaviour
{
    [SerializeField] private int RateMoney = 5; // Số tiền nhận được mỗi giây
    [SerializeField] private float timeIncreaseMoney = 3f; // Thời gian giữa các lần tăng tiền
    [SerializeField] private float elapsedTime;
    [SerializeField] private bool isIncreaseMoney = false;

    public void CallCouroutineMoney()
    {
        StartCoroutine(TimeIncreaseMoney());
    }

    private IEnumerator TimeIncreaseMoney()
    {
        while (!isIncreaseMoney)
        {
            IncreaseMoney();
            isIncreaseMoney = true;
            yield return new WaitForSeconds(timeIncreaseMoney);

            isIncreaseMoney = false;
        }
    }
    private void IncreaseMoney()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.PLANT_CREATE_MONEY, RateMoney);
        }
    }



}
