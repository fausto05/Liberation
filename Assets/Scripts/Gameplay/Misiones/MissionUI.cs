using TMPro;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text missionNameText;
    [SerializeField] private TMP_Text missionProgressText;

    private void OnEnable()
    {
        GameEvents.OnMissionStarted += UpdateMissionUI;
        GameEvents.OnMissionUpdated += UpdateMissionUI;
    }

    private void OnDisable()
    {
        GameEvents.OnMissionStarted -= UpdateMissionUI;
        GameEvents.OnMissionUpdated -= UpdateMissionUI;
    }

    private void Start()
    {
        if (MissionManager.Instance.CurrentMission != null)
        {
            UpdateMissionUI(MissionManager.Instance.CurrentMission);
        }
    }

    private void UpdateMissionUI(MissionBase mission)
    {
        missionNameText.text = mission.MissionName;
        missionProgressText.text = mission.GetProgressText();
    }
}
