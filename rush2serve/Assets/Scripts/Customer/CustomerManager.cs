using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour, IWaitingObject
{
    [SerializeField]
    private float minEnqueueTime;
    [SerializeField]
    private float maxEnqueueTime;

    [SerializeField]
    private int maxCustomnerQueueSize;

    [SerializeField]
    GameObject customerPrefab;

    [SerializeField]
    private float spaceBetweenObjects;

    Queue<GameObject> customersQueue;

    bool isTimerOn = false;


    void Start()
    {
        customersQueue = new Queue<GameObject>();
        EnqueueCustomer();
    }

    void Update()
    {
        if (!isTimerOn && customersQueue.Count < maxCustomnerQueueSize)
        {
            float randTime = Random.Range(minEnqueueTime, maxEnqueueTime);
            GlobalClock.Instance.AddCustomer(randTime, this);
            isTimerOn = true;
        }
    }

    void EnqueueCustomer()
    {
        Vector3 pos = transform.position;
        pos.x -= spaceBetweenObjects * customersQueue.Count;
        GameObject customerObject = Instantiate(customerPrefab, pos, transform.rotation);
        customersQueue.Enqueue(customerObject);
    }

    public void WaitingTimeEnded()
    {
        if (isTimerOn)
        {
            isTimerOn = false;
            EnqueueCustomer();
        }
    }
}
