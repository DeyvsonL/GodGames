using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour {
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
	[SerializeField]
	private float slow = 1;
	public float Slow {
		set{slow = value;}
	}

    private Animator anim;
    private GameManager gameManager;

    public AudioClip soundJump;
    public float volSoundJump;
    private AudioSource source;
	
	private ComboSystem leftDashCombo = new ComboSystem(new KeyCode[] {KeyCode.A, KeyCode.A});
	private ComboSystem rightDashCombo = new ComboSystem(new KeyCode[] {KeyCode.D, KeyCode.D});
	private ComboSystem frontDashCombo = new ComboSystem(new KeyCode[] {KeyCode.W, KeyCode.W});
	private ComboSystem backDashCombo = new ComboSystem(new KeyCode[] {KeyCode.S, KeyCode.S});
	private bool dash;
	public float dashDistance = 0.5f;
	public float dashSpeed = 10f;
	public float dashDuration = 1f;
	private float dashCurrentTime = 0f;
	private Vector3 dashDirection;

    void Start() {
        source = GetComponent<AudioSource>();

        body = GetComponent<Rigidbody>();
		player = GetComponent<Player>();
        anim = GetComponentInChildren<Animator>();

		body.mass = MobConfig.Weigth.medium;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void Update(){
		if (player.Dead || gameManager.Win)
			return;
		
		// Jump
		grounded  = Physics.Raycast(transform.position + transform.forward * 0.45f, -transform.up, 1.4f) 
			|| Physics.Raycast(transform.position - transform.forward * 0.45f, -transform.up, 1.4f) 
			|| Physics.Raycast(transform.position + transform.right * 0.45f, -transform.up, 1.4f) 
			|| Physics.Raycast(transform.position - transform.right * 0.45f, -transform.up, 1.4f) ;

        jumpInput = Input.GetButtonDown ("Jump") && grounded;
		
        if (jumpInput)
        {
            anim.SetTrigger("Jump");
            source.PlayOneShot(soundJump, volSoundJump);
        }
		
		// Move
		movementInput = new Vector2( Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			
		// Dash
		if (leftDashCombo.Check ()) {
			dash = true;
			dashCurrentTime = 0f;
			dashDirection = Quaternion.Euler(0,-90,0) * transform.forward ;
		}
		if (rightDashCombo.Check ()) {
			dash = true;
			dashCurrentTime = 0f;
			dashDirection = Quaternion.Euler(0,90,0) * transform.forward ;
		}
		if (frontDashCombo.Check ()) {
			dash = true;
			dashCurrentTime = 0f;
			dashDirection = transform.forward;
		}
		if (backDashCombo.Check ()) {
			dash = true;
			dashCurrentTime = 0f;
			dashDirection = -transform.forward;
		}
		
		// Look
        transform.rotation = new Quaternion(0, player.Cam.transform.rotation.y, 0, player.Cam.transform.rotation.w);

		// Animation
		anim.SetFloat("Horizontal", movementInput.x);
		anim.SetFloat("Vertical", movementInput.y);
		
//		if (!Input.GetKey(KeyCode.V)) {
//			anim.SetFloat ("Horizontal", movementInput.x);
//			anim.SetFloat ("Vertical", movementInput.y);
//			anim.ResetTrigger("skill4");
//		}else if(Input.GetKey(KeyCode.V)){
//			if(Input.GetKey(KeyCode.B)){
//				anim.SetTrigger("skill4");
//			}else{
//				anim.ResetTrigger("skill4");
//			}
//			anim.SetFloat ("Horizontal",2f);
////			anim.SetFloat ("Vertical",2f);
//		}else{
//			anim.ResetTrigger("skill4");
//		}

	}

    void FixedUpdate() {
        if (player.Dead || gameManager.Win)
            return;
		float currentSpeed = speed * slow;
		
		Vector3 targetPosition;
		if (dash) {
			currentSpeed *= dashSpeed;
			targetPosition = body.position + dashDirection * currentSpeed * Time.fixedDeltaTime;

			dashCurrentTime += Time.fixedDeltaTime;
			if (dashCurrentTime >= dashDuration) {
				dash = false;
				body.velocity = Vector3.zero;
				body.angularVelocity = Vector3.zero;
				// TODO: fix infinite movement after some collisions

				return;
			}

		} else {
			targetPosition = body.position + (((transform.forward * movementInput.y) + (transform.right * movementInput.x)) * currentSpeed)*Time.fixedDeltaTime;
		}

		RaycastHit moveRaycastHit;

		if (Physics.Linecast (body.position, targetPosition, out moveRaycastHit)) {
			targetPosition = moveRaycastHit.point;
		}

		Vector3 smoothedTargetPosition = targetPosition;
		body.MovePosition (smoothedTargetPosition);
		if (jumpInput) {
			body.AddForce (Vector3.up * jump, ForceMode.VelocityChange);
			jumpInput = false;
		}	
    }
}