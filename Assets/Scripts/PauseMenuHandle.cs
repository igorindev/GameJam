using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandle : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] PlayerInputHandle InputHandle;
    PlayerInput inputActions;

    bool paused;

    void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.Enable();
        inputActions.Pause.Pause.performed += ctx => Pause();
    }

    public void Pause()
    {
        paused = !paused;

        Time.timeScale = paused ? 0 : 1;

        InputHandle.enabled = !paused;

        pauseMenu.SetActive(paused);
    }
}
