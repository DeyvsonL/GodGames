using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
	public float dashSpeed;
	public float currentSpeed;
	Rigidbody body;
	public Vector3 direction;
	public float distance;

	public float dashCurrentTime;
	public float dashDuration;
	public bool dash;

	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody> ();
	}

	public void MoveToTarget (Vector3 direction)
	{
		if(dash) return;
		this.direction = direction;	
		this.distance = currentSpeed;
	}

	public void Dash (Vector3 direction)
	{
		this.direction = direction;
		this.distance = dashSpeed * dashDuration;
		dash = true;
		dashCurrentTime = 0f;
	}

	public void Jump ()
	{
		
	}

	public void TeleportTo ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void FixedUpdate ()
	{
		if (dash) {
			dashCurrentTime += Time.fixedDeltaTime;
			if (dashCurrentTime >= dashDuration) {
				dash = false;
				body.velocity = Vector3.zero;
				body.angularVelocity = Vector3.zero;
				// TODO: fix infinite movement after some collisions
				return;
			}
		}
		Vector3 calculatedDestination = body.position + direction * distance * Time.fixedDeltaTime;

		RaycastHit moveRaycastHit;
		if (Physics.Linecast (body.position, calculatedDestination, out moveRaycastHit)) {
			calculatedDestination = moveRaycastHit.point;
		}

		body.MovePosition (calculatedDestination);
	}
}
