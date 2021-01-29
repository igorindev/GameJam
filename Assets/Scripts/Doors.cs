using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] Transform lever;
    bool isOpen = true;

    public void Interact()
    {
        isOpen = !isOpen;

        door.SetActive(!isOpen);

        if (isOpen)
        {
            lever.localEulerAngles = new Vector3(-90, 0, 0);
        }
        else
        {
            lever.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
