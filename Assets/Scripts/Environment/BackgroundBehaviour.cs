using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundBehaviour : MonoBehaviour
{
    [SerializeField] private Canvas MainMenuCanvas;
    [SerializeField] private Button NewGameButton;
    [SerializeField] private Button LoadGameButton;
    [SerializeField] private Button OptionButton;
    [SerializeField] private Button QuitButton;

    [SerializeField] private Canvas CreateMenuCanvas;

    [SerializeField] private Canvas PauseMenuCanvas;

    [SerializeField] private Canvas UpgradeMenuCanvas;

    [SerializeField] private Canvas GameCanvas;

    public void CreateNewGame()
    {
        SceneManager.LoadScene("CreateMenu");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("CreateMenu");

    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
