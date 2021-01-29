using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandle : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] PlayerInputHandle InputHandle;
    bool paused;

    public void Pause()
    {
        paused = !paused;

        Time.timeScale = paused ? 0 : 1;

        InputHandle.enabled = !paused;

        pauseMenu.SetActive(paused);
    }
}
