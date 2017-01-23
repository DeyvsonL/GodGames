using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{

	private MovementComponent movementComponent;
	public Vector3 movementInput;
	public Vector3 dashDirection;

	private ComboSystem leftDashCombo = new ComboSystem (new KeyCode[] { KeyCode.A, KeyCode.A });
	private ComboSystem rightDashCombo = new ComboSystem (new KeyCode[] { KeyCode.D, KeyCode.D });
	private ComboSystem frontDashCombo = new ComboSystem (new KeyCode[] { KeyCode.W, KeyCode.W });
	private ComboSystem backDashCombo = new ComboSystem (new KeyCode[] { KeyCode.S, KeyCode.S });


	static private KeyCode[] validKeyCodes;


	//	public ComboSystem comboSystem;
	// Use this for initialization
	void Start ()
	{
		movementComponent = GetComponent<MovementComponent> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

	
		if (leftDashCombo.Check ()) {
			dashDirection = -transform.right;
			movementComponent.Dash (dashDirection);
		}
		if (rightDashCombo.Check ()) {
			dashDirection = transform.right;
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
			
		movementInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector3 direction = transform.right * movementInput.x + transform.forward * movementInput.y;
		movementComponent.MoveToTarget (direction);

		if (Input.GetButtonDown ("Jump")){
			movementComponent.Jump ();
		}

        movementComponent.RotateTo(new Quaternion(0,Camera.main.transform.rotation.y,0,Camera.main.transform.rotation.w));
	}

}