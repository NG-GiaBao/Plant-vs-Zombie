using UnityEngine;


//[System.Serializable]
[CreateAssetMenu(fileName =" New GameSetting",menuName =" Game Setting SO")]
public abstract class GameSetting<T> : ScriptableObject
{
    public T newSetting;
    public string settingName;
}
