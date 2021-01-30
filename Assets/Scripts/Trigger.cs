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
        else
        {
            other.transform.position = GameManager.instance.SpawnPoint.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        
    }

    IEnumerator Enable(Collider other)
    {
        other.transform.position = GameManager.instance.SpawnPoint.position;
        yield return new WaitForSeconds(0.3f);
        other.transform.GetComponent<PlayerController>().enabled = true;
    }
}
