using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SOEventSystem;
using System.Collections;

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
