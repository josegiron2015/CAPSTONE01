using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

	protected virtual void Initialize() { }
	protected virtual void FSMUpdate() { }

	public enum FSM_STATE
	{
		NONE,
		SLOWWALK,
		INVESTIGATE,
		IDLE,
		WALK,
		CROWCH,
		ATTACK,
		RUN,
		DEAD,
		SKILL,
	}

	public FSM_STATE curState;
	public FSM_STATE prevState;
}
