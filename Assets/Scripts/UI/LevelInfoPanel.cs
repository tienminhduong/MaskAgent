using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        // Fade out
        if(Transition.TryGetInstance())
            Transition.TryGetInstance().FadeOut();

        // Đợi 3 giây
        yield return new WaitForSeconds(1.5f);

        // Load scene
        SceneManager.LoadScene(levelNameText.text);
    }

}
