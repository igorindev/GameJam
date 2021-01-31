using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] pages;
    [SerializeField] PlayerInputHandle input;

    int count = -1;
    void Start()
    {
        Time.timeScale = 0;
        input.enabled = false;
        Next();
    }

    // Update is called once per frame
    public void Next()
    {
        if (count > -1)
        {
            pages[count].SetActive(false);
        }

        count += 1;

        if (count == pages.Length)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            input.enabled = true;
            return;
        }

        pages[count].SetActive(true);
    }
}
