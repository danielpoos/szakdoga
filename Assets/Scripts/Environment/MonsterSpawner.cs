using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private List<MonsterBase> monsters;
    public void SpawnMonster()
    {
        monsters.Add(ChooseRandomMonster());
    }
    public MonsterBase ChooseRandomMonster()
    {
        MonsterBase randomMonster;
        int random = Convert.ToInt32(UnityEngine.Random.value * 6);
        switch (random)
        {
            case 0:
                randomMonster = new Angel();
                break;
            case 1:
                randomMonster = new Demon();
                break;
            case 2:
                randomMonster = new Leviathan();
                break;
            case 3:
                randomMonster = new Shapeshifter();
                break;
            case 4:
                randomMonster = new Vampire();
                break;
            case 5:
                randomMonster = new Werewolf();
                break;
            default:
                randomMonster = new Angel();
                break;
        }
        return randomMonster;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
