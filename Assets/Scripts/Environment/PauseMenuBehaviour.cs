using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;

    [SerializeField] private CraftObject craftArea;
    [SerializeField] private GameObject newItem;

    [SerializeField] private GameObject saveGameBackground;
    [SerializeField] private GameObject areYouSureObject;
    [SerializeField] private GameObject saveGameMenu;
    [SerializeField] private TMP_InputField fileNameInput;
    [SerializeField] private TMP_Text errorText;
    private string saveFileName = "";
    private void Awake()
    {
        gameSetting.IsPaused = true;
        saveGameBackground.SetActive(false);
        areYouSureObject.SetActive(false);
        saveGameMenu.SetActive(false);
        errorText.text = "";
        craftArea.Inventory = gameSetting.Hunter.Inventory;
    }
    private void Start()
    {
        fileNameInput.onEndEdit.AddListener(OnNameInputEditEnded);
    }
    private void Update()
    {
        if (Input.anyKeyDown) OnKeyDown();
    }
    private bool CanBeUsedAsFileName(string name)
    {
        return !(name.Contains('\\') || name.Contains('/') || name.Contains(':') || name.Contains('*') || name.Contains('?')
            || name.Contains('<') || name.Contains('>') || name.Contains('|') || name.Contains('\"') || name.Contains(' '));
    }
    private void OnNameInputEditEnded(string text)
    {
        saveFileName = text;
        if (saveFileName == "") errorText.text = "File name required!";
        else if (!CanBeUsedAsFileName(saveFileName)) errorText.text = "There are forbidden characters in the filename!";
        else errorText.text = "";
    }
    private void OnKeyDown()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (saveGameMenu.activeInHierarchy || areYouSureObject.activeInHierarchy)
            {
                HideSaveGame();
            }
            else
            {
                BackToGame();
            }
        }
    }
    // stat info popup
    private bool HasEnoughMoney(int price)
    {
        return gameSetting.Hunter.HasEnoughMoney(price);
    }
    public void MergeItems()
    {
        if (HasEnoughMoney(100)) { }
        // check money
    }
    public void LevelUpWeapon()
    {
        // check money
    }
    public void DeleteItem()
    {
        // check money
    }
    public void BackToGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void LoadGame()
    {
        saveGameBackground.SetActive(true);
        areYouSureObject.SetActive(true);
        saveGameMenu.SetActive(false);
    }
    public void ConfirmLoadGame()
    {
        gameSetting.IsLoadedGame = true;
        SceneManager.LoadScene("CreateMenu", LoadSceneMode.Single);
    }
    public void CancelLoadGame()
    {
        HideSaveGame();
    }
    public void SaveGame()
    {
        saveGameBackground.SetActive(true);
        saveGameMenu.SetActive(true);
        areYouSureObject.SetActive(false);
    }
    public void ConfirmSaveGame()
    {
        if (gameSetting.SaveFileName == "")
        {
            if (saveFileName == ""){
                errorText.text = "File name required!";
                return;
            }
            else if (!CanBeUsedAsFileName(saveFileName)) { 
                errorText.text = "There are forbidden characters in the filename!";
                return;
            }
            if (Path.GetExtension(saveFileName) == "hex") gameSetting.SaveFileName = saveFileName;
            else gameSetting.SaveFileName = saveFileName + ".hex";
        }
        FileMethods.SaveGame(gameSetting);
        HideSaveGame();
    }
    private void HideSaveGame()
    {
        saveGameBackground.SetActive(false);
        saveGameMenu.SetActive(false);
        areYouSureObject.SetActive(false);
    }
}
