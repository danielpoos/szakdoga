using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "Scriptable Objects/GameSetting")]
public class GameSetting : ScriptableObject
{
    private string playerName = "";
    private Player player;
    private Vector2 playerPosition = new(0,0);
    private int difficulty;
    private bool isNewGame = true;
    private float timer = 0f;
    private bool isGamePaused = false;
    private string saveFileName = "supernatural.hex";
    private string previousScene;
    private MonsterSpawner spawner;
    private int roundNum = 0;
    public Options options;
    public string PlayerName { get => playerName; set => playerName = value; }
    public Player Player { get => player; set => player = value; }
    public Vector2 PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public int DiffInt { get => difficulty; set => difficulty = value; }
    public Difficulty Difficulty { get => (Difficulty)difficulty; }
    public bool IsNewGame { get => isNewGame; set => isNewGame = value; }
    public float Timer { get => timer; set => timer = value; }
    public bool IsGamePaused { get => isGamePaused; set => isGamePaused = value; }
    public string SaveFileName { get => saveFileName; set => saveFileName = value; }
    public MonsterSpawner Spawner { get => spawner; set => spawner = value; }
    public string PreviousScene { get => previousScene; set => previousScene = value; }
    public int RoundNum { get => roundNum; set => roundNum = value; }
}
public enum Difficulty { Easy = 0, Normal = 1, Hard = 2 };
