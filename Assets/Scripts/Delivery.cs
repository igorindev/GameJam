using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Delivery : MonoBehaviour
{
    [SerializeField] Itens itemName;
    [SerializeField] Rigidbody rb;
    public Itens ItemName { get => itemName; set => itemName = value; }
    public Rigidbody Rb { get => rb; set => rb = value; }
    public bool InHand { get; set; } = false;    

    private void OnCollisionEnter(Collision collision)
    {
        if (InHand)
        {
            GameManager.instance.effect.transform.position = collision.GetContact(0).point;
            GameManager.instance.effect.Play();

            InHand = false;
        }
    }
}
