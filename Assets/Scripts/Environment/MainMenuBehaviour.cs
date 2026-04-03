using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;
    private void Awake()
    {
        gameSetting.PreviousScene = SceneManager.GetActiveScene().name;
    }
    public void OpenNewGame()
    {
        gameSetting.IsNewGame = true;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
        Debug.Log("New Game");
    }
    public void LoadGame()
    {
        gameSetting.IsNewGame = false;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
        Debug.Log("Load Game");
    }
    public void Options()
    {
        SceneManager.LoadScene("OptionMenu", LoadSceneMode.Single);
        Debug.Log("Game Options");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
