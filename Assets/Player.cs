using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	public static Player instance;

	[HideInInspector] public CharacterController cc;

	public float speed = 5;

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
	}

	private void Update()
	{
		Vector3 velocity = Vector3.zero;

		CRTCamera crtCam = CRTCameraSwitcher.instance.currentCamera.gameObject.GetComponent<CRTCamera>();

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

			Vector3 desiredMovement = speed * ((moveInput.y * forwardNoVertical) + (moveInput.x * rightNoVertical));

			velocity += desiredMovement * Time.deltaTime;
		}

		// add fake gravity
		velocity += Vector3.down * 0.1f;

		// move the character controller once
		cc.Move(velocity);
	}
}
