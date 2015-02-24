using UnityEngine;
using System.Collections;

abstract public class state_a <T> {
	abstract public void Enter (T entity);
	abstract public void Execute (T entity);
	abstract public void Exit(T entity);
	abstract public string getInfo();
}
