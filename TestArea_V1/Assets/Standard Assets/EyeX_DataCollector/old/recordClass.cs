using UnityEngine;
using System.Collections;

abstract public class recordState <T>{
	
	abstract public void Enter (T entity);
	abstract public void Execute(sceneTrackdata entity, EyeXGazePoint trackPoint);
	abstract public void Exit(T entity);
}
