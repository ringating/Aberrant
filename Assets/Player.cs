using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	public static Player instance;

	[HideInInspector] public CharacterController cc;

	public float speed = 5;
	public int damage = 2;
	public float dodgeSpeed = 9;
	[HideInInspector] public float punchSpeed;
	[Tooltip("in degrees per second")]
	public float dodgeTurnRate = 360;
	public float dodgeCooldown = .25f;
	public AudioClipWithVolume dodgeSound;

	public int playerDodgeLayer;
	public int playerDefaultLayer;

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
	public GameObject dodgeVisuals;
	public GameObject punchVisuals;
	public GameObject punchExtensionVisuals; // used to tell whether the punch is active or not, lol
	public GameObject hitbox;

	public bool punchActive { get { return punchExtensionVisuals.activeInHierarchy; } } // like this

	Vector3 prevVelocity;
	float timeLastDodgeEnded;

	List<ButtonAndTime> inputBuffer;

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
		inputBuffer = new List<ButtonAndTime>();
	}

	private void Update()
	{
		UpdateInputBuffer();

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

			// apply override to direction if some external source requested it
			if (setFacingDirectionToInput && adjustedMoveInput.magnitude > 0.01f)
			{
				transform.rotation = Quaternion.LookRotation(adjustedMoveInput);
			}
		}
		setFacingDirectionToInput = false; // reset the value of the override

		// do any button state changes before the "state update" switch statement
		if (!skipButtonTransitions)
		{
			switch (currState)
			{
				case State.runOrIdle:
					if (Time.unscaledTime - timeLastDodgeEnded > dodgeCooldown && GetButtonDownLenient(Button.dodge))
					{
						currState = State.dodging;
					}
					else if (GetButtonDownLenient(Button.punch))
					{
						currState = State.punching;
						punchSpeed = 0;
					}
					break;
			}
		}
		skipButtonTransitions = false;

		// "state update"
		switch (currState)
		{
			case State.runOrIdle:
				Vector3 desiredMovement = speed * adjustedMoveInput;
				velocity += desiredMovement;

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

			case State.dodging:
				Vector3 dodgeDir = transform.forward;
				if (adjustedMoveInput.magnitude > 0.01f)
				{
					if (prevState != State.dodging)
					{
						// prev state wasn't dodge, so allow dodge to just go in the direction of input
						dodgeDir = adjustedMoveInput.normalized;
					}
					else
					{
						// previous state was dodge, so rotation is restricted
						dodgeDir = Vector3.RotateTowards(dodgeDir, adjustedMoveInput, dodgeTurnRate * Mathf.Deg2Rad * Time.deltaTime, 0);
					}
				}
				velocity += dodgeDir * dodgeSpeed;
				break;

			case State.punching:
				velocity += transform.forward * punchSpeed;
				break;
		}

		if (currState != State.punching || punchActive) // punch wants to move backwards until active, so don't rotate during that part
		{
			// rotate the player to face their velocity
			Vector3 velNoVert = new Vector3(velocity.x, 0, velocity.z);
			if (velNoVert.magnitude > 0.01f)
			{
				transform.rotation = Quaternion.LookRotation(velNoVert);
			}
		}

		// add fake gravity
		velocity += Vector3.down * 5f;

		// move the character controller once
		cc.Move(velocity * Time.deltaTime);
		prevVelocity = velocity;

		// call OnStateChange if state changed since last frame
		if (currState != prevState)
		{
			OnStateChanged(prevState, currState);
		}

		prevState = currState;
	}

	void OnStateChanged(State oldState, State newState)
	{
		// disable old visuals
		switch (oldState)
		{
			case State.runOrIdle:
				if (idleVisuals.activeSelf) idleVisuals.SetActive(false);
				if (runningVisuals.activeSelf) runningVisuals.SetActive(false);
				break;

			case State.dodging:
				if (dodgeVisuals.activeSelf) dodgeVisuals.SetActive(false);
				timeLastDodgeEnded = Time.unscaledTime;
				gameObject.layer = playerDefaultLayer;
				break;

			case State.punching:
				if (punchVisuals.activeSelf) punchVisuals.SetActive(false);
				break;
		}

		// enable new visuals, play sounds, maybe set some starting values?
		switch (newState)
		{
			case State.runOrIdle:
				// just enable idle
				if (!idleVisuals.activeSelf) idleVisuals.SetActive(true);
				if (runningVisuals.activeSelf) runningVisuals.SetActive(false);
				break;

			case State.dodging:
				if (!dodgeVisuals.activeSelf) dodgeVisuals.SetActive(true);
				Audio2DSingleton.instance.audioSource.PlayOneShot(dodgeSound.audioClip, dodgeSound.volume);
				gameObject.layer = playerDodgeLayer;
				break;

			case State.punching:
				if (!punchVisuals.activeSelf) punchVisuals.SetActive(true);
				break;
		}

		//print($"old state: {oldState}, new state: {newState}");
	}

	void UpdateInputBuffer()
	{
		if (Input.GetButtonDown("Punch"))
		{
			inputBuffer.Add(new ButtonAndTime(Button.punch, Time.unscaledTime));
		}

		if (Input.GetButtonDown("Dodge"))
		{
			inputBuffer.Add(new ButtonAndTime(Button.dodge, Time.unscaledTime));
		}

		if (Input.GetButtonDown("Interact"))
		{
			inputBuffer.Add(new ButtonAndTime(Button.interact, Time.unscaledTime));
		}

		while (inputBuffer.Count > 100)
		{
			inputBuffer.RemoveAt(0); // remove the oldest input
		}
	}

	bool GetButtonDownLenient(Button button, float leniencyInSec = 0.2f, bool clearInputBufferIfTrue = true)
	{
		bool found = false;

		foreach (ButtonAndTime bt in inputBuffer)
		{
			if (bt.button == button && Time.unscaledTime - bt.time < leniencyInSec)
			{
				found = true;
				break;
			}
		}

		if (found && clearInputBufferIfTrue)
		{
			inputBuffer.Clear();
		}

		return found;
	}

	public enum Button
	{
		punch,
		dodge,
		interact
	}

	class ButtonAndTime
	{
		public ButtonAndTime(Button button, float time)
		{
			this.button = button;
			this.time = time;
		}

		public Button button;
		public float time;
	}

	bool skipButtonTransitions;
	public void ReturnPlayerToIdle()
	{
		currState = State.runOrIdle;
		skipButtonTransitions = true;
	}

	bool setFacingDirectionToInput;
	public void SetFacingDirectionToInput()
	{
		setFacingDirectionToInput = true;
	}

	public bool powerBuffed;
	public void Hit(int damage)
	{
		if (powerBuffed)
		{
			/*RewindFrameDisplayer.*/
		}
		else
		{
			HPManager.instance.HitAndScreenShake(damage);
		}
	}
}
