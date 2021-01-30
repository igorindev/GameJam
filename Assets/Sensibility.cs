using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensibility : MonoBehaviour
{
    public static Sensibility instance;

    public float SensibilityValue { get; set; } = 7;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ja existe um Audio manager");
            return;
        }
        instance = this;
    }
}
