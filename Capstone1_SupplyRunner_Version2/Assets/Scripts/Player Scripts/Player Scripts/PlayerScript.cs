using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : StateMachine {
	CharacterController controler;
	//shooting 
	public Rigidbody projecti;
	public Transform SpawnPoint;
	// player movement variables
	public float HorizontalRotation;
	float PlayerHorizontalRotation;
	float VerticalRotation = 0;
	public float upDownRange = 60.0f;
	public float jumpSpeed;
	public float stand = 1; // changed from 0.3 to 1
    public float crowch = 0.8f; // changed from 0.15 to 0.5
	float verticalVelocity = 0;
	public bool CanMove = false;
    public bool CanJump = true;
    public bool IsCrouching = false;
    public Camera playerCamera;
	//player sound Spawn
	public GameObject SoundPrefab;
	Vector3 soundsize;
	public float soundSpawn = 0.0f;
	public float MovementSpeed = 5;
    // Use this for initialization

    void Start () {
		playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		soundsize = new Vector3 (1,1,1);
		Cursor.lockState = CursorLockMode.Locked;
		controler = GetComponent<CharacterController> ();
	}
	
    public void PlayerCanMove()
    {
        CanMove = true;
        //return;
    }

    public void PlayerCantMove()
    {
        CanMove = false;
        return;
    }

    // Update is called once per frame
    void Update () {
		playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
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
        jumpSpeed = 8;
        curState = FSM_STATE.WALK;
	}
	protected void UpdateWALKState()
	{
		if(prevState != curState)
		{
			prevState = curState;
		}
		soundsize = new Vector3 (0.4f,0.4f,0.4f);
		//transform.localScale = new Vector3 (1,2,1);
		MovementSpeed = 5.0f;
		PlayerMovement ();
	}
	protected void UpdateCROWCHState()
	{
		if (prevState != curState)
		{
			prevState = curState;
		}
        CanJump = false;
        soundsize = new Vector3 (0.2f,0.2f,0.2f);
		MovementSpeed = 2.5f;
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
        jumpSpeed = 10;
		soundsize = new Vector3 (0.6f,0.6f,0.6f);
		transform.localScale = new Vector3 (1, 1, 1); // // changed from 0.3 to 1
        MovementSpeed = 15.0f;
		PlayerMovement ();
	}

	void PlayerMovement()
	{
		if (!CanMove) {
			//shooting 
			Shooting();
			//camera rotaion
			PlayerHorizontalRotation = Input.GetAxis ("RightStickX") * 5;
			HorizontalRotation += Input.GetAxis ("RightStickX") * 5;
			//HorizontalRotation = Input.GetAxis ("Mouse X") * 5;
			
			VerticalRotation -= Input.GetAxis ("RightStickY") * -5;
			VerticalRotation -= Input.GetAxis ("Mouse Y") * -5;
			Debug.Log (VerticalRotation);
			VerticalRotation = Mathf.Clamp (VerticalRotation, -upDownRange, upDownRange);
			playerCamera.transform.localRotation = Quaternion.Euler (VerticalRotation, HorizontalRotation, 0);
			transform.Rotate(0, PlayerHorizontalRotation, 0);
            //movement
            float ForwardSpeed = Input.GetAxis ("Vertical") * -MovementSpeed;
			float SideSpeed = Input.GetAxis ("Horizontal") * MovementSpeed;
            // Jump
            if (CanJump)
            {
                if (controler.isGrounded && Input.GetButtonDown("Cross") || Input.GetKeyDown(KeyCode.Space))
                {
                    verticalVelocity = jumpSpeed;
                }
                if (!controler.isGrounded)
                {
                    verticalVelocity += (Physics.gravity.y * 2.3f) * Time.deltaTime;
                }
            }
			///////////////
			//Crouching shit
			if ( Input.GetButton("Left Bumper") && controler.isGrounded || Input.GetKey(KeyCode.LeftControl) && controler.isGrounded)
			{
                IsCrouching = true;
                
				soundsize = new Vector3(1, 1, 1);
				Vector3 CrowchSize = Vector3.Lerp (transform.localScale,new Vector3(0.4f, 0.4f, 0.4f),Time.deltaTime *5.0f); // changed from 0.3 to 0.5
               // transform.localScale = Vector3.Slerp(transform.localScale, CrowchSize, 1.0f);
                transform.localScale = CrowchSize; //new Vector3(0.3f, 0.2f, 0.3f)
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				curState = FSM_STATE.CROWCH;
			}
			else if (  Input.GetButtonUp("Left Bumper")||Input.GetKeyUp(KeyCode.LeftControl))
			{
                IsCrouching = false;
               // CanJump = true;
                transform.localScale = Vector3.Slerp(transform.localScale, new Vector3(1, 1, 1), 1.0f);
                //transform.localScale = Vector3.Slerp (transform.localScale,new Vector3(0.3f,stand, 0.3f),1.0f);
                //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				curState = FSM_STATE.WALK;
			}
            //
            if (!IsCrouching)
            {
                CanJump = true;
                if (Input.GetButtonDown("R2") || Input.GetKeyDown(KeyCode.LeftShift))
                {
                    curState = FSM_STATE.RUN;
                }
                else if (Input.GetButtonUp("R2") || Input.GetKeyUp(KeyCode.LeftShift))
                {
                    
                    curState = FSM_STATE.WALK;
                }
            }

			//if ( Input.GetKey (KeyCode.LeftAlt)) {
			//	curState = FSM_STATE.SLOWWALK;
			//	} else if(Input.GetKeyUp(KeyCode.LeftAlt)){
			//	curState = FSM_STATE.WALK;
			//}
			Vector3 speed = new Vector3 (SideSpeed,verticalVelocity,ForwardSpeed);
			speed = transform.rotation * speed;

			controler.Move (speed*Time.deltaTime);
		}
	}

	void SoundSpawner()
	{
		soundSpawn += Time.deltaTime;
		if ((CanMove = true && Input.GetAxis ("Vertical") != 0 || CanMove && Input.GetAxis ("Horizontal") != 0) && soundSpawn>=1.0f) {
			GameObject soun;
			soun  = (GameObject)Instantiate (SoundPrefab,transform.position,Quaternion.identity);
			soun.transform.parent = gameObject.transform;
			soun.transform.localScale = soundsize;
			soundSpawn = 0.0f;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "TutorialDoor1") //If we collide with an item that we can pick up
		{
			// inventory.AddItem(other.GetComponent<Item>()); //Adds the item to the inventory.
			// this.transform.position = Spawnpoint.position
		}
	}
	void Shooting()
	{
		
		if (Input.GetButtonDown("Circle") || Input.GetMouseButtonDown(0))
		{
			Rigidbody clone;
			clone = (Rigidbody)Instantiate(projecti, (SpawnPoint.position + new Vector3(0, 0, 0)), projecti.rotation);
			float point = -playerCamera.transform.localRotation.x * 40;
			if (point < 0) {
				point = 1.0f;
			}
			clone.velocity = SpawnPoint.TransformDirection(new Vector3(0, point, 10));
		}
		if (Input.GetMouseButtonDown (1) || Input.GetButtonDown("R1"))  {
			playerCamera.fieldOfView = Mathf.Lerp(60.0f,30.0f,2f);
		}
		else if (Input.GetMouseButtonUp(1) || Input.GetButtonUp("R1")) {
			playerCamera.fieldOfView = Mathf.Lerp(30.0f,60.0f,2f);
		}

	}



}
