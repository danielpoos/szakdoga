using UnityEngine;

public class CharacterAssets : MonoBehaviour
{
    public static CharacterAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    // hunter sprites
    public Sprite Bobby;
    public Sprite Castiel;
    public Sprite Dean;
    public Sprite Jody;
    public Sprite Rowena;
    public Sprite Sam;
    // monster sprites
    public Sprite Angel;
    public Sprite Demon;
    public Sprite Leviathan;
    public Sprite Shapeshifter;
    public Sprite Vampire;
    public Sprite Werewolf;
}