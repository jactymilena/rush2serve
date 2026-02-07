using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class GlobalClock : MonoBehaviour
{
    public static GlobalClock Instance { get; private set; }

    private SortedDictionary<float, IWaitingObject> waitingObjects;
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
            waitingObjects = new SortedDictionary<float, IWaitingObject>();
        }
    }

    void Start()
    {
        clock = Time.realtimeSinceStartup;
    }

    void Update()
    {
        clock = Time.realtimeSinceStartup;

        if (waitingObjects != null && waitingObjects.Count > 0)
        {
            List<float> waitingTimeToRemove = new List<float>();
            foreach (float waitingTime in waitingObjects.Keys.ToList())
            {
                if (clock > waitingTime)
                {
                    waitingObjects[waitingTime].WaitingTimeEnded();
                    waitingTimeToRemove.Add(waitingTime);
                }
                else
                {
                    break;
                }
            }

            foreach (float waitingTime in waitingTimeToRemove)
            {
                waitingObjects.Remove(waitingTime);
            }
        }
    }

    public void AddCustomer(float waitingTime, IWaitingObject waitingObject)
    {
        waitingObjects[waitingTime + clock] = waitingObject;
    }
}
