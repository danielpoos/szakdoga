using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner
{
    private List<MonsterBase> monsters = new();

    public List<MonsterBase> Monsters { get => monsters; set => monsters = value; }

    public MonsterBase SpawnMonster(Vector2 position)
    {
        MonsterBase m = ChooseRandomMonster(position);
        monsters.Add(m);
        return m;
    }
    private MonsterBase ChooseRandomMonster(Vector2 position)
    {
        int random = (int)(UnityEngine.Random.value * 6);
        MonsterBase randomMonster = random switch
        {
            0 => ScriptableObject.CreateInstance<Angel>(),
            1 => ScriptableObject.CreateInstance<Demon>(),
            2 => ScriptableObject.CreateInstance<Leviathan>(),
            3 => ScriptableObject.CreateInstance<Shapeshifter>(),
            4 => ScriptableObject.CreateInstance<Vampire>(),
            5 => ScriptableObject.CreateInstance<Werewolf>(),
            _ => ScriptableObject.CreateInstance<Demon>(),
        };
        randomMonster.position = position;
        return randomMonster;
    }
    public void UpdateTargetLocation(Vector2 position)
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].destination = position;
        }
    }
    public void ClearMonsters()
    {
        monsters.Clear();
    }
}
