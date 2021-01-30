using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] scores;

    private void Start()
    {
        Time.timeScale = 1;

        int[] saveScores = SaveGame.instance.Scores;

        for (int i = 0; i < saveScores.Length; i++)
        {
            scores[i].text = saveScores[i].ToString();
        }
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
