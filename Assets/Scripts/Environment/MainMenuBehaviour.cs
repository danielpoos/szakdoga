using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;
    [SerializeField] private CanvasGroup canvasGroup;
    private bool isLeaderboardShown = false;
    private void Awake(){
        if (isLeaderboardShown) HideLeaderboard();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && isLeaderboardShown) HideLeaderboard();
    }
    public void OpenNewGame()
    {
        gameSetting.IsNewGame = true;
        gameSetting.IsLoadedGame = false;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
    }
    public void LoadGame()
    {
        gameSetting.IsNewGame = false;
        gameSetting.IsLoadedGame = true;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
    }
    private string TimerToString(float timer)
    {
        int hour = (int)(timer / 3600);
        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);
        TimeSpan ts = new(hour, min, sec);
        return $"{ts:c}";
    }
    public void ShowLeaderboard()
    {
        isLeaderboardShown = true;
        canvasGroup.interactable = false;
        GameObject textContainer = transform.Find("MainMenuCanvas").Find("LeaderboardObject").gameObject;
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
        GameObject textContainer = transform.Find("MainMenuCanvas").Find("LeaderboardObject").gameObject;
        if (textContainer == null) return;
        textContainer.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
