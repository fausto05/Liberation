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

        Debug.Log($"MissionIndex: {data.missionIndex}");
        Debug.Log($"GameCompleted: {data.gameCompleted}");

        if (data.gameCompleted)
        {
            Debug.Log("Juego ya completado");
            return;
        }

        currentMissionIndex = data.missionIndex;

        if (currentMissionIndex < missions.Count)
        {
            Debug.Log($"Iniciando misión {currentMissionIndex}");

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

            Debug.Log("Juego completado");

            return;
        }

        SaveSystem.Save(data);

        missions[currentMissionIndex].StartMission();
    }
}
