using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public Itens currentItemToDeliver;

    public void ReceiveDelivery()
    {
        //Give points

        //Ruffle new item to delivery
    }

    private void OnTriggerEnter(Collider other)
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

public enum Itens
{
    Box,
    Cube
}
