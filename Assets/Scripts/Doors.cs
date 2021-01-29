using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] GameObject doorA;
    [SerializeField] GameObject doorB;
    [SerializeField] Transform lever;
    [SerializeField] GameObject effect;
    [SerializeField] Vector2 timeToNextCloseUp;
    bool isOpen = true;
    
    private void Start()
    {
        RuffleDelayTime();
    }

    public void Interact()
    {
        isOpen = !isOpen;

        doorA.SetActive(!isOpen);
        doorB.SetActive(!isOpen);
        effect.SetActive(!isOpen);

        if (isOpen)
        {
            lever.localEulerAngles = new Vector3(-90, 0, 0);
        }
        else
        {
            lever.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    void RuffleDelayTime()
    {
        float delay = Random.Range(timeToNextCloseUp.x, timeToNextCloseUp.y);

        StartCoroutine(Delay(delay));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        isOpen = false;
        doorA.SetActive(!isOpen);
        doorB.SetActive(!isOpen);
        effect.SetActive(!isOpen);
        lever.localEulerAngles = new Vector3(0, 0, 0);
    }
}
