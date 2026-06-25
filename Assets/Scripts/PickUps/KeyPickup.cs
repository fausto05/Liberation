using UnityEngine;

public class KeyPickup : PickupBase
{
    [SerializeField] private int keyID;

    protected override bool OnPickup(GameObject player)
    {
        Debug.Log($"Llave {keyID} recogida");

        GameEvents.OnKeyCollected?.Invoke(keyID);

        return true;
    }
}
