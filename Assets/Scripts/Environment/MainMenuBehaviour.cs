using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;
    private bool isLeaderboardShown = false;
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
    public void ShowLeaderboard()
    {
        GameObject textContainer = transform.Find("MainMenuCanvas").Find("LeaderboardObject").gameObject;
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
        GameObject textContainer = transform.Find("MainMenuCanvas").Find("LeaderboardObject").gameObject;
        if (textContainer == null) return;
        textContainer.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
