using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Itens itemName;
    [SerializeField] Rigidbody rb;
    public Itens ItemName { get => itemName; set => itemName = value; }
    public Rigidbody Rb { get => rb; set => rb = value; }
}
