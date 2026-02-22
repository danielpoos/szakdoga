using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenuBehaviour : MonoBehaviour
{
    public void MergeItems()
    {

    }
    public void BackToGame()
    {
        //if any item can be upgraded ask are you sure
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
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
