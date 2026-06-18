using System;
using System.Collections.Generic;
using UnityEngine;

public class LoreManager : MonoBehaviour
{
    public static LoreManager Instance;

    [SerializeField] private LoreData[] allLore;

    private HashSet<int> unlockedLore = new();

    private void Awake()
    {
        Instance = this;

        SaveData data = SaveSystem.Load();

        unlockedLore = new HashSet<int>(data.unlockedLore);
    }

    public bool IsUnlocked(int id)
    {
        return unlockedLore.Contains(id);
    }

    public void UnlockLore(int id)
    {
        if (unlockedLore.Contains(id))
            return;

        unlockedLore.Add(id);

        SaveData data = SaveSystem.Load();

        data.unlockedLore = new List<int>(unlockedLore);

        SaveSystem.Save(data);
    }

    public LoreData GetLore(int id)
    {
        foreach (var lore in allLore)
        {
            if (lore.id == id)
                return lore;
        }

        return null;
    }
}