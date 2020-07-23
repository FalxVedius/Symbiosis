using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour
{
    #region Public Variables
    public GameObject debugMenu;
    #endregion

    #region Private Variables
    bool isActive;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if(isActive == false)
            {
                ShowDebug();
            }
            else
            {
                HideDebug();
            }
        }
    }

    #region Private Functions
    void ShowDebug()
    {
 #if UNITY_EDITOR
 
        isActive = !isActive;
        debugMenu.SetActive(true);
#endif
    }

    void HideDebug()
    {
#if UNITY_EDITOR
        isActive = !isActive;
        debugMenu.SetActive(false);
#endif
    }
    #endregion

    #region Public Functions
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Loading Next Level");
    }

    public void RestartLevel()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
        Debug.Log("Reloading " + scene);
    }

    public void PlayButtonClickSound()
    {
        AudioManager.instance.PlaySound("TestSound");
    }
    #endregion
}
