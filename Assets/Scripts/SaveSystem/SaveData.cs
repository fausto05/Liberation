using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int missionIndex = 0;

    public bool gameCompleted = false;

    public List<int> unlockedLore = new();
}
