using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelInfoPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text levelNameText;
    [SerializeField] Image levelImage;

    public void SetLevelInfo(string levelName, Sprite levelSprite)
    {
        levelNameText.text = levelName;
        levelImage.sprite = levelSprite;
    }

    public void PlayButtonClick()
    {
        //transition to the level name
        SceneManager.LoadScene(levelNameText.text);
    }
}
