using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] GameObject levelInfoPanel;
    [SerializeField] Sprite levelImage;

    private void Start()
    {
        int level = int.Parse(levelName.Split(' ')[1]);
        if(level > LevelManager.Instance.currentHighestLevel)
        {
            GetComponent<Image>().color = Color.gray;
        }
    }

    public void OnClick()
    {
        Debug.Log("Loading level: " + levelName);
        // send string and image to info panel
        levelInfoPanel.GetComponent<LevelInfoPanel>().SetLevelInfo(levelName, levelImage);
    }

}
