﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryZone : MonoBehaviour
{
    public string currentItemToDeliver;
    [SerializeField] Transform clients;
    [SerializeField] float timeForNewDelivery = 2;
    [SerializeField] float waitDuration = 10;
    [SerializeField] Image timerBar;
    [SerializeField] Image iconImage;
    [SerializeField] ParticleSystem effectCongrats;
    [SerializeField] ParticleSystem effectMissed;
    [SerializeField] AudioClip newClient;
    [SerializeField] AudioClip delivered;
    [SerializeField] AudioClip missed;
    [SerializeField] AudioSource source;

    int activeClient;
    Coroutine timer;
    bool isColldown = false;

    private void Start()
    {
        Invoke("Init", 1f);
    }

    void Init()
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
                Delivery delivery = other.GetComponent<Delivery>();

                if (delivery.name.Split(char.Parse("("))[0] == currentItemToDeliver)
                {
                    delivery.gameObject.SetActive(false);
                    delivery.gameObject.layer = 8;
                    delivery.Rb.velocity = Vector3.zero;
                    GameManager.instance.RemoveDelivery(delivery);
                    ReceiveDelivery();
                }
            }
        }
    }

    void ReceiveDelivery()
    {
        //Give points
        clients.GetChild(activeClient).gameObject.SetActive(false);
        source.PlayOneShot(delivered);
        timerBar.transform.parent.gameObject.SetActive(false);
        iconImage.transform.parent.gameObject.SetActive(false);
        effectCongrats.Play();

        GameManager.instance.GiveMoreTime();
        GameManager.instance.UpdatePoints();

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

        while (item == null)
        {
            yield return new WaitForSeconds(2);
            item = GameManager.instance.GetItem();
        }

        yield return new WaitForSeconds(timeForNewDelivery);
        isColldown = false;
        Create(item);
    }

    void Create(Delivery item)
    {
        activeClient = Random.Range(0, clients.childCount);

        clients.GetChild(activeClient).gameObject.SetActive(true);
        source.PlayOneShot(newClient);

        //Ruffle new item to delivery
        currentItemToDeliver = item.name.Split(char.Parse("("))[0];

        if (item.DisplaySprite != null)
        {
            iconImage.sprite = item.DisplaySprite;
        }

        timerBar.transform.parent.gameObject.SetActive(true);
        iconImage.transform.parent.gameObject.SetActive(true);
        timer = StartCoroutine(RunTimer());
    }

    void TimesUp()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
        }
        clients.GetChild(activeClient).gameObject.SetActive(false);
        isColldown = true;
        source.PlayOneShot(missed);
        StartCoroutine(CreateNewDelivery());

        iconImage.transform.parent.gameObject.SetActive(false);
        effectMissed.Play();
        timerBar.transform.parent.gameObject.SetActive(false);
    }

    IEnumerator RunTimer()
    {
        float count = waitDuration;
        while (count > 0)
        {
            count -= Time.deltaTime;

            timerBar.fillAmount = count/waitDuration;
            timerBar.color = Color.Lerp(Color.red, Color.green, count / waitDuration);

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
