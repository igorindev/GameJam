using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Spawn and move to random obj, catch, move throw waypoints

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform[] waypoints;
    [SerializeField] Transform holder;
    [SerializeField] Transform mesh;
    [SerializeField] float speed;
    [SerializeField] Vector2 timeToNextRat;
    Delivery toTryGet;
    Delivery holding;

    Coroutine c;

    private void Start()
    {
        RuffleDelayTime();
    }

    IEnumerator Logic(float delay)
    {
        yield return new WaitForSeconds(delay);

        transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        while (holding == null)
        {
            if (toTryGet == null)
            {
                GetItem();
            }
            else if (toTryGet.InHand)
            {
                GetItem();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, toTryGet.transform.position, speed * Time.deltaTime);

                Vector3 dir = (toTryGet.transform.position - transform.position).normalized;

                transform.rotation = Quaternion.LookRotation(dir);

                //create 2 vectors and compare without Y

                if (Vector3.Distance(transform.position, toTryGet.transform.position) < 1.5f)
                {
                    holding = toTryGet;
                    holding.transform.SetParent(holder);
                    holding.transform.localPosition = Vector3.zero;
                    holding.Rb.isKinematic = true;
                    holding.GetByRat = true;
                }
            }

            yield return null;
        }

        //move around
    }

    IEnumerator GoAway()
    {
        Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        while (Vector3.Distance(transform.position, pos) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

            Vector3 dir = (pos - transform.position).normalized;

            transform.rotation = Quaternion.LookRotation(dir);
            yield return null;
        }

        RuffleDelayTime();
    }

    void GetItem()
    {
        toTryGet = GameManager.instance.AllGameSpawned[Random.Range(0, GameManager.instance.AllGameSpawned.Count)];
    }

    public void RemoveHolding()
    {
        if (c != null)
        {
            StopCoroutine(c);
        }

        toTryGet = null;
        holding.GetByRat = false;
        holding.transform.SetParent(null);
        holding = null;

        StartCoroutine(GoAway());
    }

    void RuffleDelayTime()
    {
        float delay = Random.Range(timeToNextRat.x, timeToNextRat.y);

       c = StartCoroutine(Logic(delay));
    }
}
