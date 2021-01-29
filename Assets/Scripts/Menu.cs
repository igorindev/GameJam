using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] SaveGame save;
    [SerializeField] TextMeshProUGUI[] scores;

    private void Start()
    {
        save.Load();

        int[] saveScores = save.Scores;

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
}
