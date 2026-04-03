using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;

    [SerializeField] private Canvas createCanvas;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private GameObject scrollView;
    [SerializeField] private TMP_Text characterText;

    [SerializeField] private Canvas loadCanvas;
    [SerializeField] private TMP_Text fileNameText;

    private void Awake()
    {
        gameSetting.Timer = 0;
        gameSetting.PlayerName = "";
        gameSetting.Player = new Player();
        if (gameSetting.IsNewGame)
        {
            createCanvas.enabled = true;
            loadCanvas.enabled = false;
        }
        else
        {
            // load player from file dialog / element
            createCanvas.enabled = false;
            loadCanvas.enabled = true;
        }
    }
    void Start()
    {
        nameInput.onEndEdit.AddListener(OnNameInputEditEnded);
    }

    public void SelectCharacter(GameObject go)
    {
        // show hunter weapon
        TMP_Text hunterName = go.GetComponentInChildren<TMP_Text>();
        characterText.text = "Play as "+hunterName.text;
        switch (hunterName.text)
        {
            case "Bobby":
                gameSetting.Player.Hunter = ScriptableObject.CreateInstance<Bobby>();
                break;
            case "Castiel":
                gameSetting.Player.Hunter = ScriptableObject.CreateInstance<Castiel>();
                break;
            case "Dean":
                gameSetting.Player.Hunter = ScriptableObject.CreateInstance<Dean>();
                break;
            case "Jody":
                gameSetting.Player.Hunter = ScriptableObject.CreateInstance<Jody>();
                break;
            case "Rowena":
                gameSetting.Player.Hunter = ScriptableObject.CreateInstance<Rowena>();
                break;
            case "Sam":
                gameSetting.Player.Hunter = ScriptableObject.CreateInstance<Sam>();
                break;
            default:
                break;
        }
    }

    void OnNameInputEditEnded(string text)
    {
        gameSetting.PlayerName = text;
    }
    public void CreateNewGame()
    {
        bool cantCreate = gameSetting.PlayerName == "" || gameSetting.Player.Hunter == null;
        if (cantCreate)
        {
            // can create a popup or glow to show the unset variables
            return;
        }
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void LoadGame()
    {
        // set variables in loadgame function
        if (gameSetting.PlayerName == "")
        {
            return;
        }
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void BackToMainMenu()
    {
        gameSetting.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(gameSetting.PreviousScene, LoadSceneMode.Single);
    }
}
