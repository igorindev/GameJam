using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Delivery[] allGameItens;
    [SerializeField] List<Delivery> allGameSpawned;
    [SerializeField] public int minutesDuration;

    public Delivery[] AllGameItens { get => allGameItens; set => allGameItens = value; }
    public List<Delivery> AllGameSpawned { get => allGameSpawned; set => allGameSpawned = value; }

    int minutes;
    int seconds;
    float startTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        minutes = minutesDuration;
    }

    void Update()
    {
        TimerUpdate();
    }

    void TimerUpdate(float delayTime = 1)
    {
        if (minutes >= 0)
        {
            if (Time.time - startTime > delayTime)
            {
                startTime = Time.time;

                if (seconds <= 0)
                {
                    seconds = 59;
                    minutes -= 1;
                }
                else
                {
                    seconds -= 1;
                }
            }

            timerText.text = $"{minutes}:{seconds}";
        }
        else
        {
            Debug.Log("Time up");
        }
    }

    void GameOver()
    {

    }

    void ShowPoints()
    {

    }
}
