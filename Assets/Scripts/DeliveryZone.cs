using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryZone : MonoBehaviour
{
    public Itens currentItemToDeliver;
    [SerializeField] float timeForNewDelivery = 2;
    [SerializeField] float waitDuration = 10;
    [SerializeField] Image timerBar;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] ParticleSystem effect;

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
        yield return new WaitForSeconds(timeForNewDelivery);
        isColldown = false;
        Create();
    }

    void Create()
    {
        //Ruffle new item to delivery
        Delivery item = GameManager.instance.GetItem();

        currentItemToDeliver = item.ItemName;

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
    Box,
    Cube
}
