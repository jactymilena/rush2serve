using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI stateText;

    public CustomerState State {  get; private set; }

    void Awake()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        stateText.text = " ";
    }

    void Start()
    {
        CustomerWaitServiceState waitForTable = new CustomerWaitServiceState("Wait for table", 2f);
        CustomerWaitServiceState moveToTable = new CustomerWaitServiceState("Move to Table", 2f);
        CustomerWaitServiceState waitToOrder = new CustomerWaitServiceState("Wait to order", 2f);


        waitForTable.SetNext(moveToTable)
                    .SetNext(waitToOrder);

        State = waitForTable;
        State.Enter(this);

        stateText.text = State.StateName;
    }

    void Update()
    {
        if (State == null)
            return;

        State.Update();
    }

    public void WaitingTimeEnded()
    {
        State = State.EnterNextState();
        stateText.text = State.StateName;
    }
}
