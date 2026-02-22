using UnityEngine;

public class ProjectileBase : ScriptableObject
{
    private int damage = 0;
    private int range = 0;
    private Vector3 position;
    private Quaternion rotation;

    protected int Damage { get => damage; set => damage = value; }
    protected int Range { get => range; set => range = value; }
    public Vector3 Position { get => position; set => position = value; }
    public Quaternion Rotation { get => rotation; set => rotation = value; }
}
