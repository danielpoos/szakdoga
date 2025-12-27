using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    //[SerializeField] protected string name;
    [SerializeField] protected int level = 0;
    [SerializeField] protected int xp = 0;
    [SerializeField] protected float timer = 0f;
    [SerializeField] public Vector3 dest;
    [SerializeField] public Quaternion rotation;
    protected int Level { get => level; set => level = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
