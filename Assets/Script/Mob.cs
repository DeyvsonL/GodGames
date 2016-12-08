using UnityEngine;

public class Mob : MonoBehaviour {

	private bool stunned = false;

	private float stunTime = 0;

	//[SerializeField]
	public float health = 10;

	[SerializeField]
	private float damage = 20;

	void Update () {
		if (stunCount())
			return;
		//se não estiver estunado, fazer o resto das ações abaixo

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

	void OnCollisionEnter(Collision collider){
		if ((collider.gameObject.tag == "Player")){
			collider.gameObject.GetComponent<Player>().takeDamage(damage);
		}
	}

}
