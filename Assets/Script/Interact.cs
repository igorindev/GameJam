using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] Transform cameraPos;
    [SerializeField] Transform handPos;
    [SerializeField] LayerMask layers;
    [SerializeField] float followDelay = 1f;
    [SerializeField] float interactDistance = 1f;
    Rigidbody holdingItem;

    Vector3 velocity;

    Coroutine coroutine;

    public void InteractWithItem()
    {
        //shot raycast and pick item
        if (holdingItem == null)
        {
            if (Physics.Raycast(cameraPos.position, cameraPos.forward, out RaycastHit hit, interactDistance, layers))
            {
                holdingItem = hit.transform.GetComponent<Rigidbody>();
                holdingItem.transform.SetParent(handPos);
                holdingItem.useGravity = false;

                coroutine = StartCoroutine(MoveItemToHand());
            }
        }
        else
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            holdingItem.useGravity = true;

            holdingItem.transform.SetParent(null);
            holdingItem = null;
        }
    }

    IEnumerator MoveItemToHand()
    {
        while (holdingItem.transform.localPosition != Vector3.zero)
        {
            holdingItem.transform.localPosition = Vector3.SmoothDamp(holdingItem.transform.localPosition, Vector3.zero, ref velocity, followDelay);

            yield return null;
        }

        holdingItem.transform.localPosition = Vector3.zero;
    }
}
