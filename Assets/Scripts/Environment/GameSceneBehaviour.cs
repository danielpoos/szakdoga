using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneBehaviour : MonoBehaviour
{
    private float timer = 0f;
    private bool isNewGame = true;
    private string saveFileName = "";
    // save to gamesetting
    protected float Timer { get => timer; set => timer = value; }
    public void Pause()
    {
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Single);
        Debug.Log("Game paused");
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
