using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : StateMachine {
	
	UnityEngine.AI.NavMeshAgent controler;
	/// Patrol
	int GoToPoint;
	public Transform[] points;
	//Chase
	public Transform enemytoFollow;
	// Investigate
	public Transform SoundToInvestigate;
	private Vector3 InvestigationSopt;
	private float timer = 0;
	public float investiagtionWait = 10;
	// variable for sight;
	public float eyeHeight = 2;
	public float SightDist = 10;
	void Start () {
		controler = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	void Update () {
		FSMUpdate ();
	}
	protected override void FSMUpdate()
	{		
		switch (curState)
		{
		case FSM_STATE.WALK: 
			UpdateWALKState(); 
			break;
		case FSM_STATE.RUN: 
			UpdateRUNState(); 
			break;
		case FSM_STATE.ATTACK: 
			UpdateATTACKState(); 
			break;
		case FSM_STATE.DEAD: 
			UpdateDEADState(); 
			break;
		case FSM_STATE.INVESTIGATE:
			UpdateINVESTIGATEState ();
			break;
		default : 
			UpdateIDLEState(); 
			break;
		}
	}
	Transform nearbyEnemy()
	{
		return enemytoFollow;
	}
	Transform nearbySound()
	{
		return SoundToInvestigate;
	}
	protected void UpdateIDLEState()
	{
		if(prevState != curState)
		{
			prevState = curState;
		}
		curState = FSM_STATE.WALK;
	}
	IEnumerator DelayNextMove()
	{
		yield return new WaitForSeconds (1f);
	}
	protected void UpdateWALKState()
	{
		if(prevState != curState)
		{
			prevState = curState;
		}
		if (nearbySound () != null) {
			curState = FSM_STATE.INVESTIGATE;
			InvestigationSopt = nearbySound ().gameObject.transform.position;
		}
		if (nearbyEnemy () == null) {
			Vector3 NextPoint = points [GoToPoint].position;
			controler.SetDestination (NextPoint);
			transform.LookAt (NextPoint);
			if (Vector3.Distance (transform.position, NextPoint) < 1) {
				GoToPoint += 1;
				if (GoToPoint + 1 > points.Length) {
					GoToPoint = 0;
				}
			}
		}

	}
	protected void UpdateRUNState()
	{
		if(prevState != curState)
		{
			prevState = curState;
		}
		if (nearbyEnemy () != null) {
			Vector3 targetPos = enemytoFollow.transform.position;
			controler.SetDestination (targetPos);
		} else {
			curState = FSM_STATE.WALK;
		}
	}
	protected void UpdateINVESTIGATEState ()
	{
		timer += Time.deltaTime;
		transform.LookAt (InvestigationSopt);
		controler.SetDestination (this.transform.position);
		if (timer >= investiagtionWait && curState != FSM_STATE.RUN) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position + Vector3.up * eyeHeight, transform.forward, out hit, SightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					enemytoFollow = hit.collider.gameObject.transform;
					curState = FSM_STATE.RUN;
				}
			}
			if (Physics.Raycast (transform.position + Vector3.up * eyeHeight,(transform.forward + transform.right).normalized, out hit, SightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					enemytoFollow = hit.collider.gameObject.transform;
					curState = FSM_STATE.RUN;
				}
			}
			if (Physics.Raycast (transform.position + Vector3.up * eyeHeight, (transform.forward - transform.right).normalized, out hit, SightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					enemytoFollow = hit.collider.gameObject.transform;
					curState = FSM_STATE.RUN;
				}
			}
			timer = 0;
			SoundToInvestigate = null;
			curState = FSM_STATE.WALK;
		}
	}
	protected void UpdateATTACKState()
	{
		if(prevState != curState)
		{
			prevState = curState;
		}
	}
	protected void UpdateDEADState()
	{
		if (prevState != curState) {
			prevState = curState;
		}
	}
	void OnTriggerEnter (Collider other)
	{
	}

	void FixedUpdate()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position + Vector3.up * eyeHeight, transform.forward, out hit, SightDist)) {
			if (hit.collider.gameObject.tag == "Player") {
				enemytoFollow = hit.collider.gameObject.transform;
				curState = FSM_STATE.RUN;
			}
		}
		if (Physics.Raycast (transform.position + Vector3.up * eyeHeight,(transform.forward + transform.right).normalized, out hit, SightDist)) {
			if (hit.collider.gameObject.tag == "Player") {
				enemytoFollow = hit.collider.gameObject.transform;
				curState = FSM_STATE.RUN;
			}
		}
		if (Physics.Raycast (transform.position + Vector3.up * eyeHeight, (transform.forward - transform.right).normalized, out hit, SightDist)) {
			if (hit.collider.gameObject.tag == "Player") {
				enemytoFollow = hit.collider.gameObject.transform;
				curState = FSM_STATE.RUN;
			}
		}

	}
}

