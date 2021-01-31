using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] scores;
    [SerializeField] Button[] levelsButtons;
    [SerializeField] Slider geralVolume;
    [SerializeField] Slider effectsVolume;
    [SerializeField] Slider sense;
    [SerializeField] Toggle mute;
    private void Start()
    {
        Time.timeScale = 1;

        int[] saveScores = SaveGame.instance.Scores;
        bool[] levels = SaveGame.instance.Unlocked;

        for (int i = 0; i < saveScores.Length; i++)
        {
            scores[i].text = saveScores[i].ToString();
        }

        levelsButtons[0].interactable = true;
        for (int i = 1; i < levels.Length; i++)
        {
            levelsButtons[i].interactable = levels[i];
        }

        geralVolume.value = AudioManager.instance.Geral;
        effectsVolume.value = AudioManager.instance.Effect;
        sense.value = Sensibility.instance.SensibilityValue;
        mute.isOn = AudioManager.instance.MuteValue;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GeralVolume(float value)
    {
        AudioManager.instance.GeralVolume(value);
    }
    public void EffectsVolume(float value)
    {
        AudioManager.instance.EffectsVolume(value);
    }
    public void Mute(bool value)
    {
        AudioManager.instance.Mute(value);
    }
    public void Sense(float value)
    {
        Sensibility.instance.SensibilityValue = value;
    }
}
