using UnityEngine;

[CreateAssetMenu(menuName = "Lore/Lore Data")]
public class LoreData : ScriptableObject
{
    public int id;

    public string title;

    [TextArea(5, 10)]
    public string description;

    public Sprite icon;
}
