using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Delivery : MonoBehaviour
{
    [SerializeField] MaterialType materialValue;
    [SerializeField] Rigidbody rb;
    [SerializeField] Sprite displaySprite;
    [SerializeField] AudioClip audioClip;
    public MaterialType MaterialValue { get => materialValue; set => materialValue = value; }
    public Rigidbody Rb { get => rb; set => rb = value; }
    public bool InHand { get; set; } = false;
    public bool GetByRat { get; set; } = false;
    public Sprite DisplaySprite { get => displaySprite; }
    public AudioClip AudioClip { get => audioClip; }

    private void OnCollisionEnter(Collision collision)
    {
        if (InHand)
        {
            GameManager.instance.Effect.transform.position = collision.GetContact(0).point;
            GameManager.instance.Effect.Play();

            InHand = false;
        }
    }
}


public enum MaterialType
{
    Metal,
    Plastic,
}

