using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateMenuBehaviour : MonoBehaviour
{
    public void CreateNewGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        //create gamesettings
        //set variables
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("LoadGame", LoadSceneMode.Single);
        //enable to set variables to load a game
        Debug.Log("Load Game");
    }
}
