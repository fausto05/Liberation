using UnityEngine;
using UnityEngine.UI;

public class SpecialButtonUI : MonoBehaviour
{
    [SerializeField] private Image buttonImage;

    [SerializeField]
    private Color emptyColor =
        new Color(0.5f, 0.5f, 0.5f);

    [SerializeField]
    private Color fullColor =
        new Color(1f, 0.5f, 0f);

    public void UpdateCharge(float progress)
    {
        buttonImage.color =
            Color.Lerp(emptyColor, fullColor, progress);
    }
}
