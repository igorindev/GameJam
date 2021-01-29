using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{
    [SerializeField] Vector2 timeToNextBlackout;
    [SerializeField] Light mainLight;
    [SerializeField] Light switchLight;
    [SerializeField] ParticleSystem effect;

    bool on = false;

    private void Start()
    {
        LightsOn();
    }

    public void LightsOn()
    {
        if (!on)
        {
            on = true;
            mainLight.intensity = 4f;
            switchLight.color = Color.green;
            effect.gameObject.SetActive(false);
            RuffleDelayTime();
        }
    }

    void RuffleDelayTime()
    {
        float delay = Random.Range(timeToNextBlackout.x, timeToNextBlackout.y);

        StartCoroutine(Delay(delay));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        on = false;
        mainLight.intensity = 0f;
        switchLight.color = Color.red;
        effect.gameObject.SetActive(true);
    }
}
