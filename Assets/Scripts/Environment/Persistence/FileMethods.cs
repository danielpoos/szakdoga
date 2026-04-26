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
        string[] split;
        gameSetting.SaveFileName = fileName;
        gameSetting.PlayerName = lines[0];
        split = lines[1].Split(',');
        gameSetting.DiffInt = int.Parse(split[0]);
        gameSetting.RoundNum = int.Parse(split[1]);
        split = lines[2].Split(',');
        gameSetting.Timer = float.Parse(split[0]);
        gameSetting.PlayerPosition = new Vector2(float.Parse(split[1]), float.Parse(split[2]));
        split = lines[3].Split(',');
        gameSetting.Hunter = (ItemType)int.Parse(split[7]) switch
        {
            ItemType.AngelBlessing => ScriptableObject.CreateInstance<Castiel>(),
            ItemType.Book => ScriptableObject.CreateInstance<Rowena>(),
            ItemType.Flamethrower => ScriptableObject.CreateInstance<Bobby>(),
            ItemType.Machete => ScriptableObject.CreateInstance<Sam>(),
            ItemType.Pistol => ScriptableObject.CreateInstance<Dean>(),
            ItemType.Shotgun => ScriptableObject.CreateInstance<Jody>(),
            _ => ScriptableObject.CreateInstance<Dean>(),
        };
        gameSetting.Hunter.Level = int.Parse(split[0]);
        gameSetting.Hunter.XP = int.Parse(split[1]);
        gameSetting.Hunter.MaxHP = int.Parse(split[2]);
        gameSetting.Hunter.HP = int.Parse(split[3]);
        gameSetting.Hunter.Attack = int.Parse(split[4]);
        gameSetting.Hunter.Money = int.Parse(split[5]);
        gameSetting.Hunter.Score = int.Parse(split[6]);
        gameSetting.Hunter.Weapon.ItemType = (ItemType)int.Parse(split[7]);
        gameSetting.Hunter.Weapon.Level = int.Parse(split[8]);
        gameSetting.Hunter.Difficulty = gameSetting.Difficulty;
        gameSetting.Hunter.rotation = bool.Parse(lines[4]);
        gameSetting.Hunter.Inventory = new();
        for (int i = 5; i < lines.Length; i++)
        {
            string[] sl = lines[i].Split(',');
            Item item = new((ItemType)int.Parse(sl[0]), int.Parse(sl[1]), int.Parse(sl[2]));
            gameSetting.Hunter.Inventory.AddItem(item);
        }
        return gameSetting; 
    }
    public static bool SaveGame(GameSetting gameSetting)
    {
        bool isSaved = false;
        string saveFile = Path.Combine(Application.persistentDataPath, gameSetting.SaveFileName);
        if (File.Exists(saveFile)) return isSaved;
        StringBuilder sb = new();
        sb.AppendLine(gameSetting.PlayerName);
        sb.AppendLine(gameSetting.DiffInt.ToString()+ ","+gameSetting.RoundNum.ToString());
        sb.AppendLine(gameSetting.Timer.ToString() + "," + gameSetting.PlayerPosition.x + "," + gameSetting.PlayerPosition.y);
        sb.AppendLine(gameSetting.Hunter.Level.ToString() + "," + gameSetting.Hunter.XP.ToString() + "," + gameSetting.Hunter.MaxHP.ToString() 
            + "," + gameSetting.Hunter.HP.ToString() + "," + gameSetting.Hunter.Attack.ToString() + "," + gameSetting.Hunter.Money.ToString() 
            + "," + gameSetting.Hunter.Score.ToString() + "," + ((int)gameSetting.Hunter.Weapon.ItemType).ToString() + "," + gameSetting.Hunter.Weapon.Level.ToString());
        sb.AppendLine(gameSetting.Hunter.rotation.ToString());
        foreach (Item item in gameSetting.Hunter.Inventory.GetItems())
        {
            sb.AppendLine(((int)item.ItemType).ToString()+","+item.Quality.ToString()+","+item.Quantity.ToString());
        }
        File.WriteAllText(saveFile, sb.ToString());
        return isSaved;
    }
}
