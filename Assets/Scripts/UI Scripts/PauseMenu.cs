using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPaused;
    public GameObject PauseUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            isPaused = !isPaused;
            PauseUI.SetActive(true);
            DefaultPauseState();
        }
    }

    public void ResumeGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            isPaused = !isPaused;
            PauseUI.SetActive(false);
        }
    }

    void DefaultPauseState()
    {
        if(PauseUI.transform.GetChild(1).gameObject.activeInHierarchy == false)
        {
            PauseUI.transform.GetChild(1).gameObject.SetActive(true);
            PauseUI.transform.GetChild(2).gameObject.SetActive(false);
            PauseUI.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
