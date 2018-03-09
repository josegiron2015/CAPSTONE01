using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class FSM : StateMachine {
	CharacterController controler;
    public Transform Spawnpoint;
    public GameObject sounded;
	float HorizontalRotation;
	float VerticalRotation = 0;
	public float upDownRange = 60.0f;
	public float jumpSpeed = 7;
	Vector3 soundsize;
	public float soundSpawn = 0.0f;
	public float MovementSpeed = 5;

	float verticalVelocity = 0;

	public bool CanMove = true;

	void Start () {
		soundsize = new Vector3 (1,1,1);
		Cursor.lockState = CursorLockMode.Locked;
		controler = GetComponent<CharacterController> ();
	}
	// Update is called once per frame
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
		case FSM_STATE.SLOWWALK: 
			UpdateSLOWWALKState(); 
			break;
		case FSM_STATE.RUN: 
			UpdateRUNState(); 
			break;
		case FSM_STATE.CROWCH: 
			UpdateCROWCHState(); 
			break;
		case FSM_STATE.ATTACK: 
			UpdateATTACKState(); 
			break;
		case FSM_STATE.DEAD: 
			UpdateDEADState(); 
			break;
		default : 
			UpdateIDLEState(); 
			break;
		}

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
		soundsize = new Vector3 (0.4f,0.4f,0.4f);
		transform.localScale = new Vector3 (1,2,1);
		MovementSpeed = 5.0f;
		PlayerMovement ();
	}

	protected void UpdateSLOWWALKState()
	{
		if(prevState != curState)
		{
			prevState = curState;
		}
		soundsize = new Vector3 (0.2f,0.2f,0.2f);
		MovementSpeed = 2f;
		PlayerMovement ();
	}

	protected void UpdateRUNState()
	{
		if(prevState != curState)
		{
			prevState = curState;
		}
		soundsize = new Vector3 (0.6f,0.6f,0.6f);
		transform.localScale = new Vector3 (1,2,1);
		MovementSpeed = 10.0f;
		PlayerMovement ();
	}

	protected void UpdateCROWCHState()
	{
		if (prevState != curState)
		{
			prevState = curState;
		}
		soundsize = new Vector3 (0.2f,0.2f,0.2f);
		MovementSpeed = 2.5f;
		PlayerMovement ();
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
        if (other.tag == "Item") //If we collide with an item that we can pick up
        {
//            inventory.AddItem(other.GetComponent<Item>()); //Adds the item to the inventory.
        }

        if (other.tag == "enemy") //If we collide with an item that we can pick up //TEMPORARY
        {
            this.transform.position = Spawnpoint.position;
        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item") //If we collide with an item that we can pick up
        {
       //     inventory.AddItem(collision.gameObject.GetComponent<Item>()); //Adds the item to the inventory.

            Destroy(collision.gameObject);
        }
    }

    void PlayerMovement()
	{
		////camera rotaion
		soundSpawn += Time.deltaTime;
		if ((CanMove = true && Input.GetAxis ("BoobStickY") != 0 || CanMove && Input.GetAxis ("BoobStickX") != 0) && soundSpawn>=1.0f) {
			GameObject soun;
			soun  = (GameObject)Instantiate (sounded,transform.position,Quaternion.identity);
			soun.transform.parent = gameObject.transform;
			soun.transform.localScale = soundsize;
			soundSpawn = 0.0f;

		}
		HorizontalRotation = Input.GetAxis ("Mouse X") *5;
		transform.Rotate (0,HorizontalRotation,0);

		VerticalRotation -= Input.GetAxis ("Mouse Y") *5;
		VerticalRotation = Mathf.Clamp (VerticalRotation,-upDownRange,upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler (VerticalRotation,0,0);
		/////movement

		float ForwardSpeed = Input.GetAxis ("Vertical") * MovementSpeed;
		float SideSpeed = Input.GetAxis ("Horizontal") * MovementSpeed;

		if (controler.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpSpeed;
		}
		if (!controler.isGrounded) {
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
		}
        
        ///////////////
        //Crouching shit
		if ( Input.GetKey(KeyCode.LeftControl))
        {
            soundsize = new Vector3(1, 1, 1);
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            curState = FSM_STATE.CROWCH;
        }
		else if (  Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, 2, 1);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            curState = FSM_STATE.WALK;
        }

		if ( Input.GetKey (KeyCode.LeftShift)) {
			curState = FSM_STATE.RUN;
		} else if(Input.GetKeyUp(KeyCode.LeftShift)){
			curState = FSM_STATE.WALK;
		}

		if ( Input.GetKey (KeyCode.LeftAlt)) {
			curState = FSM_STATE.SLOWWALK;
		} else if(Input.GetKeyUp(KeyCode.LeftAlt)){
			curState = FSM_STATE.WALK;
		}
			
		Vector3 speed = new Vector3 (SideSpeed,verticalVelocity,ForwardSpeed);
		speed = transform.rotation * speed;


		controler.Move (speed*Time.deltaTime);
	}
		
}
