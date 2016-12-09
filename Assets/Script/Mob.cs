using UnityEngine;

public class Mob : MonoBehaviour {

	private bool stunned = false;
    public bool Stunned{
        get{ return stunned; }
    }

	private float stunTime = 0;

	//[SerializeField]
	public float health = 10;

	[SerializeField]
	private float damage = 20;

	private Rigidbody body;
	public Transform target;
	public bool attackMode;

	[SerializeField]
	private float attackTime;
	[SerializeField]
	private float attackRange;

	private float elapsed;

	public SkillConfig.MarkOfTheStorm markOfTheStorm;


	void Start(){
		body = GetComponent<Rigidbody> ();
		markOfTheStorm = new SkillConfig.MarkOfTheStorm ();
	}

	void Update () {
		if (stunCount())
			return;
		//se não estiver estunado, fazer o resto das ações abaixo

		markOfTheStorm.CheckStacks (Time.deltaTime);
		if (attackMode) {
			elapsed += Time.deltaTime;

			if (elapsed >= attackTime) {
				elapsed -= attackTime;
				float distance = Vector3.Distance (body.position, target.position);
				if (distance <= attackRange) {
					Attack (target.gameObject);
				}
			}
		}

	}

	private bool stunCount() {
		if (stunned) {
			stunTime -= Time.deltaTime;
			if (stunTime <= 0) {
				stunned = false;
			}
		}
		return stunned;
	}

	public void Stun(float time) {
		stunned = true;
		stunTime = time;
	}

	public virtual void takeDamage(float damage){
		health -= damage;
		//print (gameObject.name + " newHealth:" + health);

		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public void Attack(GameObject gameObject){
		gameObject.GetComponent<Player>().takeDamage(damage);
	}

	void OnCollisionEnter(Collision collider){
		if ((collider.gameObject.tag == "Player")){
			//Attack (collider.gameObject);
		}
	}

}
