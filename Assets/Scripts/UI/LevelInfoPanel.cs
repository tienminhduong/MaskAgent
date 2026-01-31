using UnityEngine;
using UnityEngine.UI;

public class LevelInfoPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text levelNameText;
    [SerializeField] Image levelImage;

    public void SetLevelInfo(string levelName, Sprite levelSprite)
    {
        levelNameText.text = levelName;
        levelImage.sprite = levelSprite;
    }
}
