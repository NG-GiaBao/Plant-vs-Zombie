using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct DifficultSettingData
{
    public string dicultyName;
}

[CreateAssetMenu(fileName = "DifficultSetting", menuName = "Scriptable Objects/DifficultSetting")]
public class DifficultSetting : GameSetting<DifficultSetting>
{
   
}
