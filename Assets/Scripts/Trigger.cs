using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(Enable(other));
        }

        other.transform.position = GameManager.instance.SpawnPoint.position;
    }

    IEnumerator Enable(Collider other)
    {
        yield return new WaitForSeconds(0.3f);
        other.transform.GetComponent<PlayerController>().enabled = true;
    }
}
