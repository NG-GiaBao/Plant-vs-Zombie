using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenSelectPlant : BaseScreen
{
    //[SerializeField] private Vector3 offSet;
    [SerializeField] private GameObject plantSelectionPanel;
    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private List<GameObject> plantObj;


    private const string PLANT_PATH_FOLDER = "Prefabs/ImagePlant";


    private void Start()
    {
        InitPlant();
        RegisterEvent();
        if(GameManager.HasInstance)
        {
            moneyTxt.text = $"{GameManager.Instance.Money}";
        }
       
        //if (TryGetComponent<RectTransform>(out var rectTransform))
        //{
        //    rectTransform.anchoredPosition = offSet;
        //}
    }
    private void OnDestroy()
    {
        UnRegisterEvent();
    }
    private void RegisterEvent()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_MONEY, OnUpdateMoney);
        }
    }
    private void UnRegisterEvent()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_MONEY, OnUpdateMoney);
        }
    }

    private void InitPlant()
    {
        GameObject[] plantPrefabs = Resources.LoadAll<GameObject>(PLANT_PATH_FOLDER);
        foreach (GameObject plantPrefab in plantPrefabs)
        {
            GameObject plant = Instantiate(plantPrefab, plantSelectionPanel.transform);
            plantObj.Add(plant);
        }
    }
    private void OnUpdateMoney(object data)
    {
        if (data is int money)
        {
            moneyTxt.text = $"{money}";
        }
    }
}
