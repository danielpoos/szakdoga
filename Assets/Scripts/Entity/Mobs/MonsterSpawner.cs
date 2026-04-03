using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner
{
    private List<MonsterBase> monsters = new();
    public void SpawnMonster()
    {
        monsters.Add(ChooseRandomMonster());
    }
    public MonsterBase ChooseRandomMonster()
    {
        int random = (int)UnityEngine.Random.value * 6;
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
        return randomMonster;
    }
}
