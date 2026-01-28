using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerState State {  get; private set; }

    void Start()
    {
        CustomerWaitServiceState waitForTable = new CustomerWaitServiceState("Wait for table", 2f);
        CustomerWaitServiceState moveToTable = new CustomerWaitServiceState("Move to Table", 2f);
        CustomerWaitServiceState waitToOrder = new CustomerWaitServiceState("Wait to order", 2f);


        waitForTable.SetNext(moveToTable)
                    .SetNext(waitToOrder);

        State = waitForTable;
        State.Enter(this);
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
    }
}
