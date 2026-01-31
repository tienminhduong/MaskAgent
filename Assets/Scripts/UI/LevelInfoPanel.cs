using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SOEventSystem;

public class LevelInfoPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text levelNameText;
    [SerializeField] Image levelImage;
    [SerializeField] private StringPublisher onLevelEntered;
    [SerializeField] private Button button;

    public void SetLevelInfo(string levelName, Sprite levelSprite)
    {
        levelNameText.text = levelName;
        levelImage.sprite = levelSprite;

        int level = int.Parse(levelName.Split(' ')[1]);
        if(level > LevelManager.Instance.currentHighestLevel)
        {
            button.interactable = false;
            levelNameText.text = "Locked";
        }else {
            button.interactable = true;
        }
    }

    public void PlayButtonClick()
    {
        //transition to the level name
        Debug.Log("Loading Level: " + levelNameText.text);
        onLevelEntered.RaiseEvent(levelNameText.text.ToString());
        SceneManager.LoadScene(levelNameText.text);
    }
}
