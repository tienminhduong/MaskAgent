using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] GameObject levelInfoPanel;
    [SerializeField] Sprite levelImage;

    public void OnClick()
    {
        Debug.Log("Loading level: " + levelName);
        levelInfoPanel.gameObject.SetActive(true);
        // send string and image to info panel
        levelInfoPanel.GetComponent<LevelInfoPanel>().SetLevelInfo(levelName, levelImage);
    }
}
