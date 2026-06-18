using UnityEngine;

public class LorePickup : PickupBase
{
    [SerializeField] private LoreData loreData;

    protected override void OnPickup(GameObject player)
    {
        LoreManager.Instance.UnlockLore(loreData.id);

        LorePopupUI.Instance.ShowLore(loreData);
    }
}
