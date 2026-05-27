using UnityEngine;

public class KeyPickup : PickupBase
{
    protected override void OnPickup(GameObject player)
    {
        Debug.Log("Llave recogida");

        GameEvents.OnKeyCollected?.Invoke();
    }
}
