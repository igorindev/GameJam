using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] GameObject[] doors;
    [SerializeField] BoxCollider[] triggers;

    [SerializeField] Transform lever;
    [SerializeField] GameObject effect;
    [SerializeField] Vector2 timeToNextCloseUp;
    [SerializeField] AudioSource audioSource;
    bool isOpen = true;

    private void Start()
    {
        RuffleDelayTime();
    }

    public void Interact()
    {
        isOpen = !isOpen;

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(!isOpen);
            triggers[i].enabled = isOpen;
        }

        effect.SetActive(!isOpen);

        if (isOpen)
        {
            lever.localEulerAngles = new Vector3(-90, 0, 0);
        }
        else
        {
            lever.localEulerAngles = new Vector3(0, 0, 0);
        }

        audioSource.Play();
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
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(!isOpen);
            triggers[i].enabled = isOpen;
        }

        effect.SetActive(!isOpen);
        lever.localEulerAngles = new Vector3(0, 0, 0);

        audioSource.Play();
    }
}
