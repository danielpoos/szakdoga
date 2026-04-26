using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text rankText;
    private bool isLeaderboardShown = false;
    private void Awake()
    {
        if (isLeaderboardShown) HideLeaderboard();
        resultText.text = $"Good job {gameSetting.PlayerName}!";
        int rankNum = PlacePlayer();
        rankText.text = $"Your scored {gameSetting.Hunter.Score} and you are now ranked at number {rankNum} on the leaderboard.";
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && isLeaderboardShown) HideLeaderboard();
    }
    private int PlacePlayer()
    {
        if (gameSetting.PlayerName == "" || gameSetting.Hunter.Score < 0) return -2;
        string placeLine = $"{gameSetting.Hunter.Score} {gameSetting.PlayerName} {gameSetting.Timer}{Environment.NewLine}";
        if (!File.Exists(gameSetting.LeaderboardFileName)) {
            File.WriteAllText(gameSetting.LeaderboardFileName, placeLine);
            return 1;
        }
        int place = 0;
        string[] leaderboardTextSplit = File.ReadAllLines(gameSetting.LeaderboardFileName);
        foreach (string line in leaderboardTextSplit)
        {
            int score = Convert.ToInt32(line.Split(' ')[0]);
            if (score > gameSetting.Hunter.Score) place += 1;
            else if (score == gameSetting.Hunter.Score && float.Parse(line.Split(' ')[2]) < gameSetting.Timer) place += 1;
            else break;
        }
        string newLeaderboardText = "";
        if (place >= leaderboardTextSplit.Length)
        {
            for (int i = 0; i < leaderboardTextSplit.Length; i++)
            {
                newLeaderboardText += leaderboardTextSplit[i] + Environment.NewLine;
            }
            newLeaderboardText += placeLine;
        }
        else
        {
            leaderboardTextSplit[place] = placeLine + leaderboardTextSplit[place];
            for (int i = 0; i < leaderboardTextSplit.Length; i++)
            {
                newLeaderboardText += leaderboardTextSplit[i] + Environment.NewLine;
            }
        }
        File.WriteAllText(gameSetting.LeaderboardFileName, newLeaderboardText);
        return place+1;
    }
    private string TimerToString(float timer)
    {
        int hour = (int)(timer / 3600);
        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);
        TimeSpan ts = new(hour, min, sec);
        return $"{ts:c}";
    }
    public void OpenNewGame()
    {
        gameSetting.IsNewGame = true;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
    }
    public void LoadGame()
    {
        gameSetting.IsLoadedGame = true;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
    }
    public void ShowLeaderboard()
    {
        isLeaderboardShown = true;
        canvasGroup.interactable = false;
        GameObject textContainer = transform.Find("EndSceneCanvas").Find("LeaderboardObject").gameObject;
        TMP_Text textObject = textContainer.transform.Find("Content").GetComponent<TMP_Text>();
        if (!File.Exists(gameSetting.LeaderboardFileName))
        {
            textObject.text = "No player data can be displayed";
            textContainer.SetActive(true);
            return;
        }
        string[] leaderboardText = File.ReadAllLines(gameSetting.LeaderboardFileName);
        string allText = "";
        for (int i = 0; i < leaderboardText.Length; i++)
        {
            if (i == 10) break;
            if (leaderboardText[i] == "") continue;
            string[] split = leaderboardText[i].Split(' ');
            allText += $"{split[0]}  {split[1]}  {TimerToString(float.Parse(split[2]))}{Environment.NewLine}";
        }
        textObject.text = allText;
        textContainer.SetActive(true);
    }
    public void HideLeaderboard()
    {
        isLeaderboardShown = false;
        canvasGroup.interactable = true;
        GameObject textContainer = transform.Find("EndSceneCanvas").Find("LeaderboardObject").gameObject;
        if (textContainer == null) return;
        textContainer.SetActive(false);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
