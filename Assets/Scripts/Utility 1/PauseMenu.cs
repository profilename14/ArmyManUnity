using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject waveManager;
    bool pauseActive = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (pauseActive)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        waveManager.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseActive = true;
    }

    public void Resume()
    {
        waveManager.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseActive = false;
    }
}
