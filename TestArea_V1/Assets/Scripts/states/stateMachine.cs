using UnityEngine;
using System.Collections;

public class StateMachine <T>
{
	private T Owner;
	private state_a<T> CurrentState;
	private state_a<T> PreviousState;
	private state_a<T> GlobalState;

	
	public void Configure(T owner, state_a<T> InitialState) 
	{
		CurrentState = null;
		PreviousState = null;
		GlobalState = null;

		Owner = owner;
		ChangeState(InitialState);
	}
	
	public void  Update() 
	{
		if (GlobalState != null)  GlobalState.Execute(Owner);
		if (CurrentState != null) CurrentState.Execute(Owner);
	}
	
	public void  ChangeState(state_a<T> NewState) 
	{
		PreviousState = CurrentState;
		if (CurrentState != null)
			CurrentState.Exit(Owner);
		CurrentState = NewState;
		if (CurrentState != null)
			CurrentState.Enter(Owner);
	}
	
	public void  RevertToPreviousState() 
	{
		if (PreviousState != null)
			ChangeState(PreviousState);
	}
	public state_a<T> getState()
	{
		return CurrentState;
	}
};