using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryZone : MonoBehaviour
{
    public Itens currentItemToDeliver;
    [SerializeField] float timeForNewDelivery = 2;
    [SerializeField] float waitDuration = 30;
    [SerializeField] Image timerBar;

    Coroutine timer;
    bool isColldown = false;

    private void Start()
    {
        Create();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isColldown)
        {
            if (other.CompareTag("Delivery"))
            {
                if (other.GetComponent<Delivery>().ItemName == currentItemToDeliver)
                {
                    Destroy(other.gameObject);
                    ReceiveDelivery();
                }
            }
        }
    }

    void ReceiveDelivery()
    {
        //Give points

        if (timer != null)
        {
            StopCoroutine(timer);
        }

        isColldown = true;

        StartCoroutine(CreateNewDelivery());
    }

    IEnumerator CreateNewDelivery()
    {
        yield return new WaitForSeconds(timeForNewDelivery);
        isColldown = false;
        Create();
    }

    void Create()
    {
        //Ruffle new item to delivery
        Itens i = (Itens)Random.Range(0, 2);
        currentItemToDeliver = i;

        timer = StartCoroutine(RunTimer());
    }

    void TimeUp()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
        }

        isColldown = true;

        StartCoroutine(CreateNewDelivery());
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

        TimeUp();
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
    Box,
    Cube
}
