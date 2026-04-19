using System.IO;
using System.Text;
using UnityEngine;

public class FileMethods
{
    public static GameSetting LoadGame(string fileName, GameSetting gameSetting)
    {
        string saveFile = Path.Combine(Application.persistentDataPath, gameSetting.SaveFileName);
        if (!File.Exists(saveFile)) return gameSetting;
        string[] lines = File.ReadAllLines(saveFile);
        gameSetting.SaveFileName = fileName;
        gameSetting.PlayerName = lines[0];
        gameSetting.DiffInt = int.Parse(lines[1]);
        gameSetting.RoundNum = int.Parse(lines[2]);
        gameSetting.Timer = float.Parse(lines[3]);
        gameSetting.PlayerPosition = new Vector2(float.Parse(lines[4].Split(',')[0]), float.Parse(lines[4].Split(',')[1]));
        gameSetting.BackgroundPosition = new Vector2(float.Parse(lines[5].Split(',')[0]), float.Parse(lines[5].Split(',')[1]));
        gameSetting.Player.Money = int.Parse(lines[6]);
        gameSetting.Player.Score = int.Parse(lines[7]);
        gameSetting.Hunter.Weapon.ItemType = (ItemType)int.Parse(lines[8]);
        gameSetting.Hunter.Weapon.Level = int.Parse(lines[9]);
        gameSetting.Player.Inventory = new();
        for (int i = 11; i < lines.Length - 1; i++)
        {
            string[] sl = lines[i].Split(',');
            Item item = new((ItemType)int.Parse(sl[0]), int.Parse(sl[1]), int.Parse(sl[2]));
            gameSetting.Player.Inventory.AddItem(item);
        }
        return gameSetting; 
    }
    public static bool SaveGame(GameSetting gameSetting)
    {
        bool isSaved = false;
        string saveFile = Path.Combine(Application.persistentDataPath, gameSetting.SaveFileName);
        if (File.Exists(saveFile)) return isSaved;
        //JsonUtility.ToJson();
        StringBuilder sb = new();
        sb.AppendLine(gameSetting.PlayerName);
        sb.AppendLine(gameSetting.DiffInt.ToString());
        sb.AppendLine(gameSetting.RoundNum.ToString());
        sb.AppendLine(gameSetting.Timer.ToString());
        sb.AppendLine(gameSetting.PlayerPosition.x + ","+gameSetting.PlayerPosition.y);
        sb.AppendLine(gameSetting.BackgroundPosition.x + ","+gameSetting.BackgroundPosition.y);
        sb.AppendLine(gameSetting.Player.Money.ToString());
        sb.AppendLine(gameSetting.Player.Score.ToString());
        sb.AppendLine(((int)gameSetting.Hunter.Weapon.ItemType).ToString());
        sb.AppendLine(gameSetting.Hunter.Weapon.Level.ToString());
        foreach (Item item in gameSetting.Player.Inventory.GetItems())
        {
            sb.AppendLine(((int)item.ItemType).ToString()+","+item.Quality.ToString()+","+item.Quantity.ToString());
        }
        File.WriteAllText(saveFile, sb.ToString());
        return isSaved;
    }
}
