using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /*public enum ScenesEnum
    {
        StartScreen,EndScreen,Level1
    }
    public static void Load(ScenesEnum scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    */
    public void LoadGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void LoadEndScreen()
    {
        SceneManager.LoadScene("EndScreen");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
