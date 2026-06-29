using UnityEngine;

public class ProgressRestorer : MonoBehaviour
{
    [Header("Puertas")]
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject doorPanels;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;

    [Header("Llaves")]
    [SerializeField] private GameObject key1;
    [SerializeField] private GameObject key2;
    [SerializeField] private GameObject key3;

    [Header("Paneles")]
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;

    private void Awake()
    {
        SaveData data = SaveSystem.Load();

        int mission = data.missionIndex;

        if (mission >= 3)
        {
            door1.SetActive(false);

            if (key1 != null)
                key1.SetActive(false);
        }

        if (mission >= 4)
        {
            doorPanels.SetActive(false);

            if (panel1 != null)
                panel1.SetActive(false);

            if (panel2 != null)
                panel2.SetActive(false);
        }

        if (mission >= 7)
        {
            door2.SetActive(false);

            if (key2 != null)
                key2.SetActive(false);
        }

        if (mission >= 9)
        {
            door3.SetActive(false);

            if (key3 != null)
                key3.SetActive(false);
        }
    }
}
