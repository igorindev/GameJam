using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Caller : MonoBehaviour
{
    [SerializeField] UnityEvent callEvent;

    public UnityEvent CallEvent { get => callEvent; set => callEvent = value; }
}
