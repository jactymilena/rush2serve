using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class GlobalClock : MonoBehaviour
{
    public static GlobalClock Instance { get; private set; }

    private SortedDictionary<float, Customer> waitingCustomers;
    private float clock;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            waitingCustomers = new SortedDictionary<float, Customer>();
        }
    }

    void Start()
    {
        clock = Time.realtimeSinceStartup;
    }

    void Update()
    {
        clock = Time.realtimeSinceStartup;

        if (waitingCustomers != null && waitingCustomers.Count > 0)
        {
            List<float> waitingTimeToRemove = new List<float>();
            foreach (float waitingTime in waitingCustomers.Keys.ToList())
            {
                if (clock > waitingTime)
                {
                    waitingCustomers[waitingTime].WaitingTimeEnded();
                    waitingTimeToRemove.Add(waitingTime);
                }
                else
                {
                    break;
                }
            }

            foreach (float waitingTime in waitingTimeToRemove)
            {
                waitingCustomers.Remove(waitingTime);
            }
        }
    }

    public void AddCustomer(float waitingTime, Customer customer)
    {
        waitingCustomers[waitingTime + clock] = customer;
    }
}
