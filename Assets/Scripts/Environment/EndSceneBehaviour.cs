using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text rankText;
    private bool isLeaderboardShown = false;
    private void Awake()
    {
        resultText.text = $"Good job {gameSetting.PlayerName}! You did well!";
        int rankNum = PlacePlayer();
        rankText.text = $"Your scored {gameSetting.Player.Score} and you are now ranked at number {rankNum} on the leaderboard.";
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && isLeaderboardShown)
        {
            HideLeaderboard();
        }
    }
    private int PlacePlayer()
    {
        if (gameSetting.Player == null) return -2;
        if (!File.Exists(gameSetting.LeaderboardFileName)) File.Create(gameSetting.LeaderboardFileName);
        string[] leaderboardTextSplit = File.ReadAllText(gameSetting.LeaderboardFileName).Split(Environment.NewLine);
        string placeLine = $"{gameSetting.Player.Score} {gameSetting.PlayerName}{Environment.NewLine}";
        int place = 0;
        foreach (string line in leaderboardTextSplit)
        {
            int score = Convert.ToInt32(line.Split(' ')[0]);
            if (score > gameSetting.Player.Score) place += 1;
            else break;
        }
        string newLeaderboardText = "";
        for (int i = 0; i < leaderboardTextSplit.Length; i++)
        {
            if (i == place) newLeaderboardText += placeLine;
            newLeaderboardText += leaderboardTextSplit[i] + Environment.NewLine;
        }
        return place+1;
    }
    public void OpenNewGame()
    {
        gameSetting.IsNewGame = true;
        // reinit vars
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
    }
    public void LoadGame()
    {
        gameSetting.IsLoadedGame = true;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
    }
    public void ShowLeaderboard()
    {
        GameObject textContainer = transform.Find("EndSceneCanvas").Find("LeaderboardObject").gameObject;
        TMP_Text textObject = textContainer.transform.Find("Content").GetComponent<TMP_Text>();
        isLeaderboardShown = true;
        if (!File.Exists(Environment.CurrentDirectory + gameSetting.LeaderboardFileName))
        {
            textObject.text = "No player data can be displayed";
            textContainer.SetActive(true);
            return;
        }
        string leaderboardText = File.ReadAllText(gameSetting.LeaderboardFileName);
        textObject.text = leaderboardText;
        textContainer.SetActive(true);
    }
    public void HideLeaderboard()
    {
        isLeaderboardShown = false;
        GameObject textContainer = transform.Find("EndSceneCanvas").Find("LeaderboardObject").gameObject;
        if (textContainer == null) return;
        textContainer.SetActive(false);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
