using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuHandle : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] PlayerInputHandle InputHandle;
    [SerializeField] Slider geralVolume;
    [SerializeField] Slider effectsVolume;
    [SerializeField] Slider sense;
    [SerializeField] Toggle mute;

    bool paused;

    private void Start()
    {
        geralVolume.value = AudioManager.instance.Geral;
        effectsVolume.value = AudioManager.instance.Effect;
        sense.value = Sensibility.instance.SensibilityValue;
        mute.isOn = AudioManager.instance.MuteValue;
    }

    public void Pause()
    {
        paused = !paused;

        Time.timeScale = paused ? 0 : 1;

        InputHandle.enabled = !paused;

        pauseMenu.SetActive(paused);
    }
}
