using UnityEngine;

public class PlayerPrefsPersistence : ILoader, ISaver
{
    private const string DurationKey = "duration";

    public float LoadData()
    {
        return PlayerPrefs.GetFloat(DurationKey, 0);
    }

    public void SaveData(float duration)
    {
        PlayerPrefs.SetFloat(DurationKey, duration);
        PlayerPrefs.Save();
    }
}
