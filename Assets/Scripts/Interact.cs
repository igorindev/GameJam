using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] Transform cameraPos;
    [SerializeField] Transform handPos;
    [SerializeField] LayerMask layers;
    [SerializeField] float interactDistance = 1f;
    [SerializeField] float pullForce = 10f;
    [SerializeField] float force = 10f;
    Delivery holdingItem;


    Material hitted;

    public Delivery HoldingItem { get => holdingItem; }

    public void InteractWithItem(bool value)
    {
        //shot raycast and pick item
        if (value)
        {
            if (holdingItem == null)
            {
                if (Physics.Raycast(cameraPos.position, cameraPos.forward, out RaycastHit hit, interactDistance, layers))
                {
                    if (hit.collider.CompareTag("Switch"))
                    {
                        hit.transform.parent.GetComponent<Blackout>().LightsOn();
                    }
                    else
                    {
                        holdingItem = hit.transform.GetComponent<Delivery>();

                        holdingItem.Rb.isKinematic = true;

                        hitted.SetFloat("Boolean_Outline", 0);
                        hitted = null;
                    }
                }
            }
        }
        else
        {
            if (holdingItem != null)
            {
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
            Rigidbody rb = holdingItem.Rb;

            rb.useGravity = true;
            rb.isKinematic = false;

            holdingItem.InHand = true;
            holdingItem.transform.SetParent(null);
            holdingItem = null;

            rb.AddForce(cameraPos.forward * force);
        }
    }


    private void Update()
    {
        if (holdingItem != null)
        {
            float rbMass = pullForce;
            
            Vector3 pos = new Vector3(handPos.position.x, Mathf.Clamp(handPos.position.y, 1, 100), handPos.position.z);

            Vector3 movePosition = Vector3.Lerp(holdingItem.transform.position, pos, rbMass * Time.deltaTime);

            Quaternion rot = Quaternion.Lerp(holdingItem.transform.localRotation, handPos.rotation, 10 * Time.deltaTime);

            holdingItem.transform.position = movePosition;
            holdingItem.transform.rotation = rot;
            return;
        }

        if (Physics.Raycast(cameraPos.position, cameraPos.forward, out RaycastHit hit, interactDistance, layers))
        {
            if (hitted != null)
            {
                hitted.SetFloat("Boolean_Outline", 0);
            }

            if (hit.transform.TryGetComponent(out Renderer r))
            {
                hitted = r.material;
            }
            else
            {
                return;
            }
           
            hitted.SetFloat("Boolean_Outline", 1);
            return;
        }

        if (hitted != null)
        {
            hitted.SetFloat("Boolean_Outline", 0);
            hitted = null;
        }
    }
}
