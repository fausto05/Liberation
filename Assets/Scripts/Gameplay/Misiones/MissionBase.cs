using UnityEngine;

public abstract class MissionBase : MonoBehaviour
{
    public abstract string MissionName { get; }

    public abstract string GetProgressText();

    public abstract void StartMission();
    public abstract void CompleteMission();
}
