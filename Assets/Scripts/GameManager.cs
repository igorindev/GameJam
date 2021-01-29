using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;
using NineRealmsTools;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public VisualEffect effect;
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] FadeAnimation bonusTimer;
    [SerializeField] Delivery[] allGameItens;
    [SerializeField] List<Delivery> allGameSpawned;
    [SerializeField] List<Delivery> allGameNotSpawned;
    [SerializeField] public int minutesDuration;
    [SerializeField] public int numOfStartItens;
    [SerializeField] public int numOfDropItens;

    float totalPoints; 

    public Delivery[] AllGameItens { get => allGameItens; set => allGameItens = value; }
    public List<Delivery> AllGameSpawned { get => allGameSpawned; set => allGameSpawned = value; }
    public Transform SpawnPoint { get => spawnPoint[Random.Range(0, spawnPoint.Length)]; }

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

    public void GiveMoreTime()
    {
        seconds += 5;
        bonusTimer.StarFade();
    }

    public void UpdatePoints()
    {
        totalPoints += 10;
        points.text = totalPoints.ToString();
    }
}
