using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform spawnPoint;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Delivery[] allGameItens;
    [SerializeField] List<Delivery> allGameSpawned;
    [SerializeField] public int minutesDuration;
    [SerializeField] public int numOfStartItens;
    [SerializeField] public int numOfDropItens;

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

        Init();
    }

    void Update()
    {
        TimerUpdate();
    }

    void Init()
    {
        for (int i = 0; i < numOfStartItens; i++)
        {
            int value = Random.Range(0, allGameItens.Length);

            Delivery item = Instantiate(allGameItens[value], spawnPoint.position + Random.insideUnitSphere * Random.Range(0, 1.5f), Quaternion.identity);

            allGameSpawned.Add(item);
        }

        InvokeRepeating("NewOrders", 1, 8);
    }

    public Delivery GetItem()
    {
        int value = Random.Range(0, allGameSpawned.Count);
        Delivery d = allGameSpawned[value];
        allGameSpawned.RemoveAt(value);
        return d;
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

    void GiveMoreTime()
    {
        seconds += 5;
    }

    void NewOrders()
    {
        for (int i = 0; i < numOfDropItens; i++)
        {
            int value = Random.Range(0, allGameItens.Length);

            Delivery item = Instantiate(allGameItens[value], spawnPoint.position + Random.insideUnitSphere * Random.Range(0, 1.5f), Quaternion.identity);

            allGameSpawned.Add(item);
        }
    }
}
