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
    [SerializeField] float force = 10f;
    [SerializeField] float breakLimit = 10f;
    Delivery holdingItem;

    Coroutine coroutine;

    public void InteractWithItem(bool value)
    {
        //shot raycast and pick item
        if (value)
        {
            if (holdingItem == null)
            {
                if (Physics.Raycast(cameraPos.position, cameraPos.forward, out RaycastHit hit, interactDistance, layers))
                {
                    holdingItem = hit.transform.GetComponent<Delivery>();
                    holdingItem.transform.SetParent(handPos);
                    holdingItem.Rb.isKinematic = true;

                    coroutine = StartCoroutine(MoveItemToHand());
                }
            }
        }
        else
        {
            if (holdingItem != null)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }

                holdingItem.Rb.useGravity = true;
                holdingItem.Rb.isKinematic = false;
                holdingItem.InHand = true;

                holdingItem.transform.SetParent(null);
                holdingItem = null;
            }
        }
    }

    public void Throw()
    {
        if (holdingItem != null)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            Rigidbody rb = holdingItem.Rb;

            rb.useGravity = true;
            rb.isKinematic = false;

            holdingItem.InHand = true;
            holdingItem.transform.SetParent(null);
            holdingItem = null;

            rb.AddForce(cameraPos.forward * force);
        }
    }


    private void FixedUpdate()
    {
        

        
    }

    IEnumerator MoveItemToHand()
    {
        while (true)
        {
            Vector3 v = Vector3.zero;
            //lerp
            holdingItem.Rb.MovePosition(Vector3.SmoothDamp(holdingItem.transform.position, handPos.position, ref v, 0.6f));

            yield return new WaitForFixedUpdate();
        }
    }
}
