using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Delivery[] allGameItens;
    [SerializeField] List<Delivery> allGameSpawned;

    public Delivery[] AllGameItens { get => allGameItens; set => allGameItens = value; }
    public List<Delivery> AllGameSpawned { get => allGameSpawned; set => allGameSpawned = value; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        
    }

    void Timer()
    {

    }
}
