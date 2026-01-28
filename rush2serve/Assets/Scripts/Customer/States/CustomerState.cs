using UnityEngine;

public abstract class CustomerState
{
    public Customer Customer { get; protected set; }
    public string StateName { get; protected set; }
    public float WaitingTime { get; set; }
    public CustomerState NextState { get; set; }

    public CustomerState(string stateName, float waitingTime)
    {
        StateName = stateName;
        WaitingTime = waitingTime;
    }

    public CustomerState SetNext(CustomerState nextState)
    {
        NextState = nextState;
        return nextState;
    }

    public abstract void Enter(Customer customer);
    public abstract void Update();
    public abstract void Exit();
    public abstract CustomerState EnterNextState();
}
