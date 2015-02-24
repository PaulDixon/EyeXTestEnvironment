using UnityEngine;
using System.Collections;

public class StateMachine <T> : MonoBehaviour  {
	private T Owner;
	private state<T> CurrentState;
	private state<T> PreviousState;
	private state<T> GlobalState;
	
	public void Awake() {
		CurrentState = null;
		PreviousState = null;
		GlobalState = null;
	}
	
	public void Configure(T owner, state<T> InitialState) {
		Owner = owner;
		ChangeState(InitialState);
	}
	
	public void  Update() {
		if (GlobalState != null)  GlobalState.Execute(Owner);
		if (CurrentState != null) CurrentState.Execute(Owner);
	}
	
	public void  ChangeState(state<T> NewState) {
		PreviousState = CurrentState;
		if (CurrentState != null)
			CurrentState.Exit(Owner);
		CurrentState = NewState;
		if (CurrentState != null)
			CurrentState.Enter(Owner);
	}
	
	public void  RevertToPreviousState() {
		if (PreviousState != null)
			ChangeState(PreviousState);
	}
};