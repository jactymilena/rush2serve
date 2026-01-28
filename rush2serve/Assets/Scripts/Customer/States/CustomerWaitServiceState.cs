using UnityEngine;

public class CustomerWaitServiceState : CustomerState
{
    public CustomerWaitServiceState(string stateName, float waitingTime) : base(stateName, waitingTime)
    {
    }

    public override void Enter(Customer customer)
    {
        // TODO: play animation, start timer
        Customer = customer;
        GlobalClock globalClock = GlobalClock.Instance;
        globalClock.AddCustomer(WaitingTime, customer);

        Debug.Log(StateName);

    }

    public override CustomerState EnterNextState()
    {
        Exit();
        if (NextState != null && Customer != null)
        {
            NextState.Enter(Customer);
        }

        return NextState;
    }

    public override void Exit()
    {
        // TODO: stop animation 
    }

    public override void Update()
    {
        // Maybe not needed
    }
}
