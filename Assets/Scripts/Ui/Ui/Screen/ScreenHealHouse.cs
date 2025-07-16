using TMPro;
using UnityEngine;

public class ScreenHealHouse : BaseScreen
{
    [SerializeField] private TextMeshProUGUI healText;
    [SerializeField] private int healAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.SEND_HEAL_HOUSE, OnHealHouse);
        }
    }
    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.SEND_HEAL_HOUSE, OnHealHouse);
        }
    }

   
    private void OnHealHouse(object data)
    {
        if (data is int heal)
        {
            healAmount = heal;
            healText.text = $"Heal: {healAmount}";
        }
    }
}
