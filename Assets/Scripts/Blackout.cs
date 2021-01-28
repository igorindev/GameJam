using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{
    [SerializeField] Vector2 timeToNextBlackout;
    [SerializeField] Light[] lights;

    private void Start()
    {
        RuffleDelayTime();
    }

    public void RuffleDelayTime()
    {
        float delay = Random.Range(timeToNextBlackout.x, timeToNextBlackout.y);

        StartCoroutine(Delay(delay));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
