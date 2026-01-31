using SOEventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : PersistentSingleton<LevelManager>
{
    public int currentHighestLevel = 1;
    int currentPlayingLevel = -1;
    int maxLevel = 10;
    
    public void OnGameOver()
    {
       // Handle game over logic here
        Debug.Log("Game Over!");
    }

    public void OnLevelComplete()
    {

       // Handle level complete logic here
        Debug.Log("Level Complete!");
        if(currentPlayingLevel == currentHighestLevel && currentHighestLevel < maxLevel)
        {
            currentHighestLevel++;
        }
        SceneManager.LoadScene("LevelSelectionScene");


    }

    public void OnLevelEntered(string levelName)
    {
        Debug.Log(levelName);
        int level = int.Parse(levelName.Split(' ')[1]);
        currentPlayingLevel = level;
    }
}
