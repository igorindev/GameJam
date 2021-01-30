using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;
using NineRealmsTools;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] VisualEffect effect;
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] PlayerInputHandle playerInputHandle;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] FadeAnimation bonusTimer;
    [SerializeField] Animator anim;

    [Header("Game Over")]
    [SerializeField] TextMeshProUGUI scoreEnd;
    [SerializeField] GameObject endGame;

    [Header("Game Itens")]
    [SerializeField] Delivery[] allGameItens;

    [SerializeField] List<Delivery> allGameSpawned;
    [SerializeField] List<Delivery> allGameNotSpawned;

    [Header("Level Configs")]
    [SerializeField] int minutesDuration;
    [SerializeField] int numOfStartItens;
    [SerializeField] int numOfDropItens;

    int totalPoints; 

    public Delivery[] AllGameItens { get => allGameItens; set => allGameItens = value; }
    public List<Delivery> AllGameSpawned { get => allGameSpawned; set => allGameSpawned = value; }
    public Transform SpawnPoint { get => spawnPoint[Random.Range(0, spawnPoint.Length)]; }
    public VisualEffect Effect { get => effect; }

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

        Init();
    }

    public void RemoveDelivery(Delivery delivery)
    {
        allGameNotSpawned.Add(delivery);
    }

    void Update()
    {
        TimerUpdate();
    }

    void Init()
    {
        for (int i = 0; i < allGameItens.Length; i++)
        {
            int value = i;

            Delivery item = Instantiate(allGameItens[value]);

            item.gameObject.SetActive(false);

            allGameNotSpawned.Add(item);
        }

        InvokeRepeating("NewOrders", 1, 8);
    }

    void NewOrders()
    {
        for (int i = 0; i < numOfDropItens; i++)
        {
            if (allGameNotSpawned.Count <= 0) { return; }

            int value = Random.Range(0, allGameNotSpawned.Count);
            allGameNotSpawned[value].transform.position = SpawnPoint.position + Random.insideUnitSphere * Random.Range(0, 1.5f);
            allGameNotSpawned[value].gameObject.SetActive(true);

            allGameSpawned.Add(allGameNotSpawned[value]);
            allGameNotSpawned.RemoveAt(value);
        }
    }

    //The dlivery chose a spawned item to be delivered
    public Delivery GetItem()
    {
        if (allGameSpawned.Count >= 0)
        {
            int value = Random.Range(0, allGameSpawned.Count);
            Delivery d = allGameSpawned[value];
            allGameSpawned.RemoveAt(value);
            return d;
        }
        else
        {
            return null;
        }
    }

    void TimerUpdate(float delayTime = 1)
    {
        if (minutes >= 0)
        {
            timerText.text = minutes.ToString("d2") + ":" + seconds.ToString("d2");
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
        }
        else
        {
            Debug.Log("Time up");
            timerText.text = "00:00";
            GameOver();
        }
    }

    void GameOver()
    {
        SaveGame.instance.AddScoreToLevel(totalPoints);
        scoreEnd.text = totalPoints.ToString();
        playerInputHandle.enabled = false;
        Time.timeScale = 0;
        endGame.SetActive(true);
        SaveGame.instance.Save();
    }

    void ShowPoints()
    {

    }

    public void GiveMoreTime()
    {
        seconds += 5;
        bonusTimer.StarFade();
    }

    public void UpdatePoints()
    {
        totalPoints += 10;
        points.text = totalPoints.ToString();

        anim.SetTrigger("Estrela");
    }
}
