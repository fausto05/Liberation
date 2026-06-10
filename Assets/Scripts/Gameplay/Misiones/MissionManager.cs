using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    [SerializeField] private List<MissionBase> missions;

    private int currentMissionIndex;

    public MissionBase CurrentMission =>
        currentMissionIndex < missions.Count
        ? missions[currentMissionIndex]
        : null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        SaveData data = SaveSystem.Load();

        

        if (data.gameCompleted)
        {
            
            return;
        }

        currentMissionIndex = data.missionIndex;

        

        if (currentMissionIndex < missions.Count)
        {
            

            missions[currentMissionIndex].StartMission();
        }
    }

    public void StartNextMission()
    {
        currentMissionIndex++;

        SaveData data = SaveSystem.Load();

        data.missionIndex = currentMissionIndex;

        if (currentMissionIndex >= missions.Count)
        {
            data.gameCompleted = true;

            SaveSystem.Save(data);

            

            return;
        }

        SaveSystem.Save(data);

        
        missions[currentMissionIndex].StartMission();
    }
}
