using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    int currentLevel = 1;
    int maxLevel = 10;
    public GameObject levelButtonPrefab; // Prefab for the level button
    public Transform buttonContainer; // Parent object to hold buttons

    void Start()
    {
        // Load all level buttons at the beginning
        LoadLevelButtons();
    }

    void LoadLevelButtons()
    {
        for (int i = 1; i <= maxLevel; i++)
        {
            GameObject button = Instantiate(levelButtonPrefab, buttonContainer);
            button.GetComponentInChildren<TextMesh>().text = "Level " + i; // Set button text
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2((i - 1) * 100, 0); // Position buttons left to right
        }
    }
}
