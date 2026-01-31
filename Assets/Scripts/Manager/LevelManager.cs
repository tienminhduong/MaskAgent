using SOEventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    
    public void OnGameOver()
    {
       // Handle game over logic here
        Debug.Log("Game Over!");
    }

    public void OnLevelComplete()
    {

       // Handle level complete logic here
        Debug.Log("Level Complete!");
        SceneManager.LoadScene("LevelSelectionScene");
    }
}
