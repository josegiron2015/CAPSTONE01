using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

	[Header("Movement Values")]
	public float RunSpeed;
	public float WalkSpeed;
	public float CrouchSpeed;
	public float JumpHeight;

	[Space]
	[Header("Crouch Values")]
	public Vector3 CrouchPosition;
	public float ControllerHeightOnCrouch;

	[Space]
	[Header("Booleans")]
	/*[HideInInspector]*/
	public bool CanMove;
	/*[HideInInspector]*/
	public bool CanCrouch;
	/*[HideInInspector]*/
	public bool CanJump;

	public Vector3 originalControllerPosition;
	public float originalControllerHeight;

	private Vector3 moveDirection;
	private bool isRunning;
	private bool isCrouching;
	private CharacterController controller;

	#region References
	//This is for player controller hit
	//private void OnControllerColliderHit(ControllerColliderHit hit)
	//{

	//}

	//Text mesh pro variable
	//TextMeshProUGUI text;
	//text.text = "asdasd";

	#endregion

	#region Unity Functions
	// Use this for initialization
	void Start()
	{
		controller = GetComponent<CharacterController>();

		originalControllerHeight = controller.height;
		originalControllerPosition = controller.center;

		controller.detectCollisions = true;

		CanMove = true;
	}

	// Update is called once per frame
	void Update()
	{
		CalculateGravity();
		Crouch();
		Jump();
		Move();

		if (Input.GetKey(KeyCode.LeftControl))
			isCrouching = true;
		else
		{
			if (isCrouching)
			{
				Ray ray = new Ray(transform.position + controller.center, Vector3.up);

				Debug.DrawRay(transform.position + controller.center, Vector3.up * 5);

				if (Physics.SphereCast(ray, controller.radius, 5))
				{
					isCrouching = true;
				}
				else
				{
					isCrouching = false;
				}
			}
		}
	}

	#endregion

	#region Movement

	private void Move()
	{
		//Retains gravity
		float currentGravityForce = moveDirection.y;

		//If player can move
		if (CanMove)
		{
			//Get movement inputs
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

			//Multiply the speed to the input
			if (isRunning)
				moveDirection *= RunSpeed;
			if (isCrouching)
				moveDirection *= CrouchSpeed;
			else
				moveDirection *= WalkSpeed;
		}
		//Else if player can't move
		else
		{
			//Set movement inputs to zero
			moveDirection = Vector3.zero;
		}

		//Then set back the current gravity
		moveDirection.y = currentGravityForce;

		controller.Move(moveDirection * Time.deltaTime);
	}

	private void Jump()
	{
		if (CanJump)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Debug.Log("Jump");
				moveDirection.y = JumpHeight;
				CanJump = false;
			}
		}
	}

	private void Crouch()
	{
		if (isCrouching)
		{
			controller.height = ControllerHeightOnCrouch;
			controller.center = CrouchPosition;
		}
		else
		{
			controller.height = originalControllerHeight;
			controller.center = originalControllerPosition;
		}
	}

	#endregion

	#region Gravity

	void CalculateGravity()
	{
		if (IsGrounded())
		{
			CanJump = true;
			moveDirection.y = Physics.gravity.y * Time.deltaTime;
		}
		else
		{
			CanJump = false;

			if (!CanJump || moveDirection.y > 0)
			{
				moveDirection.y += Physics.gravity.y * Time.deltaTime;
			}
			else if (!CanJump || moveDirection.y < 0)
			{
				moveDirection.y += (Physics.gravity.y * Time.deltaTime) * 2;
			}
		}
	}

	#endregion

	#region Checkers

	public bool IsGrounded()
	{
		return controller.isGrounded;
	}

	#endregion

}
