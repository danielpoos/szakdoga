using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    public void BackToGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void GameOptions()
    {
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
