using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMovement : MonoBehaviour
{
    Rigidbody rb;

	public Enemy myEnemy { get; set; } // set when instantiated

	public float speed = 1;
	[Tooltip("in degrees")]
	public float turnRate = 30;
	public int layerAfterHittingNonPlayer;

	public Collider trigger;
	public Collider particleCollider;

	bool stopped;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
    {
		if (!stopped)
		{
			Vector3 toPlayer = (Player.instance.transform.position + (Vector3.up * 0.4f)) - transform.position;
			Vector3 newForward = Vector3.RotateTowards(transform.forward, toPlayer, Time.deltaTime * turnRate * Mathf.Deg2Rad, 0);

			rb.rotation = Quaternion.LookRotation(newForward);
			rb.velocity = transform.forward * speed;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		print($"bolt hit {other.name}");

		if (other.name == "Player")
		{
			Player player = other.GetComponent<Player>();
			player.Hit(myEnemy.damage);

			Destroy(gameObject); // fine for now
		}
		else
		{
			stopped = true;
			rb.useGravity = true;
			rb.velocity = Vector3.zero;
			gameObject.layer = layerAfterHittingNonPlayer;
			trigger.enabled = false;
			particleCollider.enabled = true;
		}
	}
}
