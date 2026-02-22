using UnityEngine;

public class FileMethods : MonoBehaviour
{
    private string fileName;
    private Player player;
    private GameSetting gs;

    public FileMethods(string fileName, Player player, GameSetting gs)
    {
        this.fileName = fileName;
        this.player = player;
        this.gs = gs;
    }

    public bool LoadGame()
    {
        return false;
    }
    public bool SaveGame()
    {
        return false;
    }
}
