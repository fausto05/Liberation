using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    [SerializeField] private List<MissionBase> missions;

    private int currentMissionIndex;

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
        if (missions.Count > 0)
        {
            missions[0].StartMission();
        }
    }

    public void StartNextMission()
    {
        currentMissionIndex++;

        if (currentMissionIndex >= missions.Count)
        {
            Debug.Log("No hay mas misiones");
            return;
        }

        missions[currentMissionIndex].StartMission();
    }
}
