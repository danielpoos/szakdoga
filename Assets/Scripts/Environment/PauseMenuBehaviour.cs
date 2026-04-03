using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;
    private void Awake()
    {
        gameSetting.IsGamePaused = true;
        // stop timer
    }
    public void MergeItems()
    {

    }
    public void LevelUpWeapon()
    {

    }
    public void BackToGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void GameOptions()
    {
        gameSetting.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("OptionMenu", LoadSceneMode.Single);
    }
    public void LoadGame()
    {
        //get game variables
        Debug.Log("Load Game");
    }
    public void SaveGame()
    {
        //set game variables
        Debug.Log("Save Game");
    }
}
