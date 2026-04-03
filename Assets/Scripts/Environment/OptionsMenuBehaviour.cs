using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;
    [SerializeField] private Options options;
    private void Awake()
    {
        gameSetting.options = options;
    }
    public void BackToPreviousScene()
    {
        SceneManager.LoadScene(gameSetting.PreviousScene, LoadSceneMode.Single);
    }
}
