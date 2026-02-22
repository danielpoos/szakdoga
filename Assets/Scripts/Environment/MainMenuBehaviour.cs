using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void OpenNewGame()
    {
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
        Debug.Log("New Game");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
        //set game variables
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
        Debug.Log("Load Game");
    }
}
