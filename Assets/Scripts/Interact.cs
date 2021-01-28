﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] Transform cameraPos;
    [SerializeField] Transform handPos;
    [SerializeField] LayerMask layers;
    [SerializeField] float followDelay = 1f;
    [SerializeField] float interactDistance = 1f;
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

                holdingItem.transform.SetParent(null);
                holdingItem = null;
            }
        }
    }

    IEnumerator MoveItemToHand()
    {
        float delay = 0;

        while (delay < followDelay)
        {
            if (holdingItem == null)
            {
                yield break;
            }

            delay += Time.deltaTime;
            holdingItem.transform.localPosition = Vector3.Lerp(holdingItem.transform.localPosition, Vector3.zero, delay);
            holdingItem.transform.localRotation = Quaternion.Lerp(holdingItem.transform.localRotation, Quaternion.identity, delay);

            yield return null;
        }

        holdingItem.transform.localPosition = Vector3.zero;
    }
}
