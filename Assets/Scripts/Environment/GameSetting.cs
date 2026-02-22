using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "Scriptable Objects/GameSetting")]
public class GameSetting : ScriptableObject
{
    [SerializeField] private float timer = 0f;
    [SerializeField] private bool isNewGame = true;
    [SerializeField] private string saveFileName = "supernatural.hex";
    [SerializeField] private Difficulty difficulty;
    //[SerializeField] private Options options;

    public bool LoadGame()
    {
        return false;
    }
    public bool SaveGame()
    {
        return false;
    }
}

public enum Difficulty { Easy = 1, Normal = 2, Hard = 3 };
