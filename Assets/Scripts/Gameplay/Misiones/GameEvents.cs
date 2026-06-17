using UnityEngine;
using System;

public static class GameEvents 
{
    public static Action OnPlayerLeftRoom;
    public static Action OnEnemyKilled;
    public static Action OnKeyCollected;
    public static Action OnPanelDestroyed;

    public static Action<MissionBase> OnMissionStarted;
    public static Action<MissionBase> OnMissionUpdated;
}
