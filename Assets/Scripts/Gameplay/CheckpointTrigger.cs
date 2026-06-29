using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private int checkpointID;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        SaveData data = SaveSystem.Load();

        if (data.checkpointID >= checkpointID)
            return;

        data.checkpointID = checkpointID;

        SaveSystem.Save(data);

        Debug.Log($"Checkpoint {checkpointID} guardado");
    }
}
