using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] private GameObject loaderObject;
    [SerializeField] private GameObject loadSavedContent;
    [SerializeField] private GameObject savedGameObject;

    private string selectedFileName = "";

    private void Awake()
    {
        gameSetting.Timer = 0;
        gameSetting.PlayerName = "";
        if (gameSetting.IsNewGame)
        {
            createCanvas.gameObject.SetActive(true);
            createCanvas.enabled = true;
            loadCanvas.gameObject.SetActive(false);
            loadCanvas.enabled = false;
        }
        else if (gameSetting.IsLoadedGame)
        {
            loadCanvas.gameObject.SetActive(true);
            loadCanvas.enabled = true;
            createCanvas.gameObject.SetActive(false);
            createCanvas.enabled = false;
            loaderObject.SetActive(false);
            savedGameObject.SetActive(false);
        }
    }
    private void Start()
    {
        nameInput.onEndEdit.AddListener(OnNameInputEditEnded);
    }
    private void OnNameInputEditEnded(string text)
    {
        gameSetting.PlayerName = text;
    }
    public void SelectCharacter(GameObject go)
    {
        // show hunter weapon
        TMP_Text hunterName = go.GetComponentInChildren<TMP_Text>();
        characterText.text = "Play as "+hunterName.text;
        switch (hunterName.text)
        {
            case "Bobby": gameSetting.Hunter = ScriptableObject.CreateInstance<Bobby>(); break;
            case "Castiel": gameSetting.Hunter = ScriptableObject.CreateInstance<Castiel>(); break;
            case "Dean": gameSetting.Hunter = ScriptableObject.CreateInstance<Dean>(); break;
            case "Jody": gameSetting.Hunter = ScriptableObject.CreateInstance<Jody>(); break;
            case "Rowena": gameSetting.Hunter = ScriptableObject.CreateInstance<Rowena>(); break;
            case "Sam": gameSetting.Hunter = ScriptableObject.CreateInstance<Sam>(); break;
            default: break;
        }
    }
    public void ShowSavedGames()
    {
        loaderObject.SetActive(true);
        string[] arr = Directory.GetFiles(Application.persistentDataPath, "*.hex").Select(Path.GetFileName).ToArray();
        foreach (Transform child in loadSavedContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < arr.Length; i++)
        {
            GameObject loadable = Instantiate(savedGameObject, loadSavedContent.transform);
            loadable.name = $"DataTextHolder{i}";
            loadable.SetActive(true);
            TMP_Text data = loadable.GetComponentInChildren<TMP_Text>();
            data.text = arr[i];
        }
        TMP_Text[] textObjects = loaderObject.GetComponentsInChildren<TMP_Text>();
        if (arr.Length == 0)
        {
            textObjects[^1].text = "No saved game found";
        }
        else
        {
            textObjects[^1].text = "Saved games found";
        }
    }
    public void HideSavedGames()
    {
        loaderObject.SetActive(false);
        if (selectedFileName != "")
        {
            fileNameText.text = selectedFileName;
        }
        SetGameData();
    }
    private void SetGameData()
    {
        gameSetting = FileMethods.LoadGame(selectedFileName, gameSetting);
        try {
            gameSetting.SaveFileName = selectedFileName;
        } catch (Exception) { }
        try
        {
            gameSetting.Player.Hunter = gameSetting.Hunter;
        } catch (Exception) { }
    }
    public void GetSavedGameData()
    {
        selectedFileName = GameObject.Find(EventSystem.current.currentSelectedGameObject.name).GetComponentInChildren<TMP_Text>().text;
    }
    public void CreateNewGame()
    {
        bool cantCreate = gameSetting.PlayerName == "" || gameSetting.Hunter == null;
        if (cantCreate)
        {
            // can create a popup or glow to show the unset variables
            return;
        }
        gameSetting.Hunter.Difficulty = gameSetting.Difficulty;
        gameSetting.Hunter.SetWeapon();
        gameSetting.RoundNum = 0;
        gameSetting.IsNewGame = true;
        gameSetting.IsLoadedGame = false;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void LoadGame()
    {
        Debug.Log(gameSetting.SaveFileName +" "+ selectedFileName +" "+ gameSetting.PlayerName);
        if (selectedFileName == "" || gameSetting.PlayerName == "")
        {
            return;
        }
        gameSetting.IsNewGame = false;
        gameSetting.IsLoadedGame = true;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
