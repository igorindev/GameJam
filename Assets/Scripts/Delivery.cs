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

    private void OnCollisionEnter(Collision collision)
    {
        VisualEffect effect = Instantiate(GameManager.instance.effect);

        effect.transform.position = collision.GetContact(0).point;
        effect.Play();
    }
}
