﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryZone : MonoBehaviour
{
    public string currentItemToDeliver;
    [SerializeField] float timeForNewDelivery = 2;
    [SerializeField] float waitDuration = 10;
    [SerializeField] Image timerBar;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] ParticleSystem effect;

    Coroutine timer;
    bool isColldown = false;

    private void Start()
    {
        Delivery item = GameManager.instance.GetItem();
        Create(item);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isColldown)
        {
            if (other.CompareTag("Delivery"))
            {
                if (other.GetComponent<Delivery>().name.Split(char.Parse("("))[0] == currentItemToDeliver)
                {
                    other.gameObject.SetActive(false);
                    ReceiveDelivery();
                }
            }
        }
    }

    void ReceiveDelivery()
    {
        //Give points
        itemName.text = "Delivered!";
        timerBar.transform.parent.gameObject.SetActive(false);
        effect.Play();

        if (timer != null)
        {
            StopCoroutine(timer);
        }

        isColldown = true;

        StartCoroutine(CreateNewDelivery());
    }

    IEnumerator CreateNewDelivery()
    {
        Delivery item = GameManager.instance.GetItem();

        if (item == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(timeForNewDelivery);
        isColldown = false;
        Create(item);
    }

    void Create(Delivery item)
    {
        //Ruffle new item to delivery
        currentItemToDeliver = item.name.Split(char.Parse("("))[0];

        itemName.text = currentItemToDeliver.ToString();

        timerBar.transform.parent.gameObject.SetActive(true);

        timer = StartCoroutine(RunTimer());
    }

    void TimesUp()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
        }

        isColldown = true;

        StartCoroutine(CreateNewDelivery());

        itemName.text = "Time's Up!";

        timerBar.transform.parent.gameObject.SetActive(false);
    }

    IEnumerator RunTimer()
    {
        float count = waitDuration;
        while (count > 0)
        {
            count -= Time.deltaTime;

            timerBar.fillAmount = count/waitDuration;

            yield return null;
        }

        timerBar.fillAmount = 0;

        TimesUp();
    }

    void ReduceTimerForNextDeliver()
    {

    }

    void ReduceTimerForDelivery()
    {

    }
}

public enum Itens
{
    Altere,
    Ancora,
    Banana,
    Banjo,
    Baralho,
    Bengala,
    BoiaAmarela,
    BoiaAzul,
    BoiaVermelha,
    BolaDeBolicheCinza,
}
