using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneScript : MonoBehaviour
{
    public string levelName = "";
    public void OpenLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void OpenLevel()
    {
        if (levelName == "")
            return;
        SceneManager.LoadScene(levelName);
    }
}
