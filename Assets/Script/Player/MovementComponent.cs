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

	private float dashCurrentTime;
	public float dashDuration;
	private bool onDash;

	public float jumpHeight;
	private float distToGround;
	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody> ();
		distToGround = GetComponent<Collider>().bounds.extents.y;
	}

	public void MoveToTarget (Vector3 direction)
	{
		if(onDash) return;
		this.direction = direction;	
		this.distance = currentSpeed;
	}

	public void Dash (Vector3 direction)
	{
		onDash = true;
		this.direction = direction;
		this.distance = dashSpeed * dashDuration;
		dashCurrentTime = 0f;
		print ("dash");
	}

	public void Jump ()
	{
		if(isGrounded ()){
			body.AddForce (Vector3.up * jumpHeight, ForceMode.VelocityChange);
		}
	}

	public bool isGrounded(){
		return Physics.Raycast (transform.position, Vector3.down, distToGround + 1.0f);
	}

	public void TeleportTo ()
	{
		
	}

    public void RotateTo(Quaternion quaternion)
    {
        if (onDash) return;
        transform.rotation = quaternion;
    }

	void FixedUpdate ()
	{
		if (onDash) {
			dashCurrentTime += Time.fixedDeltaTime;
			if (dashCurrentTime >= dashDuration) {
				onDash = false;
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
