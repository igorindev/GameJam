using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public Itens currentItemToDeliver;
    [SerializeField] float timeForNewDelivery = 2;

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

    IEnumerator RunTimer()
    {
        yield return new WaitForSeconds(timeForNewDelivery);
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
