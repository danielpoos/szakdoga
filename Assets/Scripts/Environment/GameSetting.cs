using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "Scriptable Objects/GameSetting")]
public class GameSetting : ScriptableObject
{
    private string playerName = "";
    private string saveFileName = "supernatural.hex"; // dynamic_naming / name savefile
    private string leaderboardFileName = "leaderboard.hexdata";
    private int difficulty;
    private int roundNum = 0;
    private bool isNewGame = true;
    private bool isLoadedGame = false;
    private float timer = 0f;
    private Player player;
    private HunterBase hunter;
    private Vector2 playerPosition = new(0,0);
    private Vector2 backgroundPosition = new(0,0);
    private MonsterSpawner spawner = new();
    private List<Item> itemsOnGround = new();
    public string PlayerName { get => playerName; set => playerName = value; }
    public bool IsNewGame { get => isNewGame; set => isNewGame = value; }
    public bool IsLoadedGame { get => isLoadedGame; set => isLoadedGame = value; }
    public float Timer { get => timer; set => timer = value; }
    public string SaveFileName { get => saveFileName; set => saveFileName = value; }
    public string LeaderboardFileName { get => leaderboardFileName; }
    public int RoundNum { get => roundNum; set => roundNum = value; }
    public int DiffInt { get => difficulty; set => difficulty = value; }
    public Difficulty Difficulty { get => (Difficulty)difficulty; }
    public Vector2 PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public Vector2 BackgroundPosition { get => backgroundPosition; set => backgroundPosition = value; }
    public Player Player { get => player; set => player = value; }
    public HunterBase Hunter { get => hunter; set => hunter = value; }
    public MonsterSpawner Spawner { get => spawner; set => spawner = value; }
    public List<Item> ItemsOnGround { get => itemsOnGround; set => itemsOnGround = value; }
}
public enum Difficulty { Easy = 0, Normal = 1, Hard = 2 };
