using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	public static Player instance;

	[HideInInspector] public CharacterController cc;

	public float speed = 5;

	public enum State
	{
		runOrIdle,
		punching,
		dodging
	}

	public State currState;
	private State prevState;

	public GameObject idleVisuals;
	public GameObject runningVisuals;

	private void Awake()
	{
		// singleton stuff
		if (instance)
		{
			Debug.LogError("An instance of Player already exists! Destroying this instance!");
			Destroy(this);
			return;
		}
		instance = this;

		// get components
		cc = GetComponent<CharacterController>();

		// set values
		prevState = currState;
	}

	private void Update()
	{
		Vector3 velocity = Vector3.zero;

		CRTCamera crtCam = CRTCameraSwitcher.instance.currentCamera.gameObject.GetComponent<CRTCamera>();
		Vector3 adjustedMoveInput = Vector3.zero;

		if (crtCam)
		{
			Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			if (moveInput.magnitude > 1)
			{
				moveInput.Normalize();
			}

			Vector3 camForward = crtCam.myCamera.transform.forward;
			Vector3 camRight = crtCam.myCamera.transform.right;
			Vector3 forwardNoVertical = new Vector3(camForward.x, 0, camForward.z).normalized;
			Vector3 rightNoVertical = new Vector3(camRight.x, 0, camRight.z).normalized;

			adjustedMoveInput = (moveInput.y * forwardNoVertical) + (moveInput.x * rightNoVertical);
		}

		// do state-specific stuff
		switch (currState)
		{
			case State.runOrIdle:
				Vector3 desiredMovement = speed * adjustedMoveInput;
				velocity += desiredMovement * Time.deltaTime;

				if (desiredMovement.magnitude > 0.01f)
				{
					// running visuals
					if (idleVisuals.activeSelf) idleVisuals.SetActive(false);
					if (!runningVisuals.activeSelf) runningVisuals.SetActive(true);
				}
				else
				{
					// idle visuals
					if (runningVisuals.activeSelf) runningVisuals.SetActive(false);
					if (!idleVisuals.activeSelf) idleVisuals.SetActive(true);
				}

				break;
		}

		// rotate the player to face their velocity
		Vector3 velNoVert = new Vector3(velocity.x, 0, velocity.z);
		if (velNoVert.magnitude > 0.01f)
		{
			transform.rotation = Quaternion.LookRotation(velNoVert);
		}

		// add fake gravity
		velocity += Vector3.down * 0.1f;

		// move the character controller once
		cc.Move(velocity);

		// call OnStateChange if necessary
		if (currState != prevState)
		{
			OnStateChanged(prevState, currState);
		}

		prevState = currState;
	}

	void OnStateChanged(State oldState, State newState)
	{
		//asdf
	}
}
