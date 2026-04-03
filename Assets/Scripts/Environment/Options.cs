using UnityEngine;

[CreateAssetMenu(fileName = "Options", menuName = "Scriptable Objects/Options")]
public class Options : ScriptableObject
{
    public Shortcuts shortcut;
    private void Awake()
    {
        LoadShortCuts();
    }
    private void LoadShortCuts()
    {
        shortcut.Pause = KeyCode.Escape;
        shortcut.Options = KeyCode.O;
        shortcut.Save = KeyCode.K;
        shortcut.Load = KeyCode.L;
        shortcut.Interact = KeyCode.F;
        shortcut.Buymenu = KeyCode.B;
        shortcut.Nextweapon = KeyCode.E;
        shortcut.Prevweapon = KeyCode.Q;
        shortcut.Craft = KeyCode.C;
        shortcut.Inventory = KeyCode.I;
    }
    public struct Shortcuts
    {
        public KeyCode Pause { get; set; }
        public KeyCode Options { get; set; }
        public KeyCode Save { get; set; }
        public KeyCode Load { get; set; }
        public KeyCode Interact { get; set; } //???
        public KeyCode Buymenu { get; set; }
        public KeyCode Nextweapon { get; set; }
        public KeyCode Prevweapon { get; set; }
        public KeyCode Craft { get; set; }
        public KeyCode Inventory { get; set; }
    }

}
