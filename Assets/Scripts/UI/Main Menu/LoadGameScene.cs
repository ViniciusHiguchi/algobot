using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    public static string levelToLoad;
    public void GameScene(string level)
    {
        levelToLoad = level;
        SceneManager.LoadScene("Scenes/GameScene");
    }
}
