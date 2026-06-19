using UnityEngine;

public class KeyPickup : PickupBase
{
    protected override bool OnPickup(GameObject player)
    {
        Debug.Log("Llave recogida");

        GameEvents.OnKeyCollected?.Invoke();

        return true;
    }
}
