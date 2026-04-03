using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;
    private void Awake()
    {
    }
    public void OpenNewGame()
    {
        gameSetting.IsNewGame = true;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
    }
    public void LoadGame()
    {
        gameSetting.IsNewGame = false;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
    }
    public void ShowLeaderboard()
    {
        //
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
