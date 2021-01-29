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
    public MaterialType MaterialValue { get => materialValue; set => materialValue = value; }
    public Rigidbody Rb { get => rb; set => rb = value; }
    public bool InHand { get; set; } = false;
    public Sprite DisplaySprite { get => displaySprite; }

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


public enum MaterialType
{
    Metal,
    Plastic,
}

