using UnityEngine;

[System.Serializable]
public struct AudioSettingData
{
    public float volumn;
}

[CreateAssetMenu(fileName = "AudioSetting", menuName = "Scriptable Objects/AudioSetting")]
public class AudioSetting : GameSetting<AudioSettingData>
{
    
}
