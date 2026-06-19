using UnityEngine;

public class LorePickup : PickupBase
{
    [SerializeField] private LoreData loreData;

    protected override bool OnPickup(GameObject player)
    {
        LoreManager.Instance.UnlockLore(loreData.id);

        LorePopupUI.Instance.ShowLore(loreData);

        return true;
    }
}
