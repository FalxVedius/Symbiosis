using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Loads specified scene when called. String is Scene Name.
    public void LoadScene(string scene)
    {
        if (scene != "")
        {
            SceneManager.LoadScene(scene);
           Debug.Log("Loading " + scene);
        }
        else
            Debug.Log("No Scene Specified");

    }

    //Closes Application when Clicked
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quiting Game");
        
        //if currently in Editor, stops playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
