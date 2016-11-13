﻿using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerMovement : NetworkBehaviour {
    public float speed;
    public float jump;

	// Componesnts
	private Player player;

    private bool grounded;
    private bool hanging;
    private int floorCount;

	private Rigidbody body;

	private Vector2 movementInput;
	private bool jumpInput;
	private bool jumpTriggered;

	private Vector3 currentVelocity;

    void Start() {
        body = GetComponent<Rigidbody>();
		player = GetComponent<Player>();
    }

	void Update(){
		if (!isLocalPlayer)
			return;
		
		grounded  = Physics.Raycast(transform.position + transform.forward * 0.45f, -transform.up, 1.4f) 
			|| Physics.Raycast(transform.position - transform.forward * 0.45f, -transform.up, 1.4f) 
			|| Physics.Raycast(transform.position + transform.right * 0.45f, -transform.up, 1.4f) 
			|| Physics.Raycast(transform.position - transform.right * 0.45f, -transform.up, 1.4f) ;

		movementInput = new Vector2( Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		jumpInput = Input.GetButtonDown ("Jump") && grounded;

		transform.rotation = new Quaternion(0, player.Cam.transform.rotation.y, 0, player.Cam.transform.rotation.w);
	}

    void FixedUpdate() {
        if (!isLocalPlayer)
            return;

		Vector3 targetPosition = body.position + (((transform.forward * movementInput.y) + (transform.right * movementInput.x)) * speed);
		Vector3 smoothedTargetPosition = Vector3.SmoothDamp (body.position, targetPosition, ref currentVelocity, 0.2f, speed, Time.fixedDeltaTime);
		body.MovePosition (smoothedTargetPosition);
		if (jumpInput) {
			body.AddForce (Vector3.up * jump, ForceMode.VelocityChange);
			jumpInput = false;
		}	
    }
}