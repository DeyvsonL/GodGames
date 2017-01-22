using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour {

	MovementComponent movementComponent;
	public Vector3 movementInput;
	public Vector3 dashDirection;

	private ComboSystem leftDashCombo = new ComboSystem(new KeyCode[] {KeyCode.A, KeyCode.A});
	private ComboSystem rightDashCombo = new ComboSystem(new KeyCode[] {KeyCode.D, KeyCode.D});
	private ComboSystem frontDashCombo = new ComboSystem(new KeyCode[] {KeyCode.W, KeyCode.W});
	private ComboSystem backDashCombo = new ComboSystem(new KeyCode[] {KeyCode.S, KeyCode.S});
//	public ComboSystem comboSystem;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		KeyCode key = Input.GetKey ();
		if (leftDashCombo.Check (key)) {
			dashDirection = -transform.right ;
			movementComponent.Dash (dashDirection);
		}
		if (rightDashCombo.Check ()) {
			dashDirection = transform.right ;
			movementComponent.Dash (dashDirection);
		}
		if (frontDashCombo.Check ()) {
			dashDirection = transform.forward;
			movementComponent.Dash (dashDirection);
		}
		if (backDashCombo.Check ()) {
			dashDirection = -transform.forward;
			movementComponent.Dash (dashDirection);
		}

		movementInput = new Vector2( Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector3 direction = transform.right * movementInput.x + transform.forward * movementInput.y;
		movementComponent.MoveToTarget (direction);
	}
}
