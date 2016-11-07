using UnityEngine;

public class Mob : MonoBehaviour {

    private bool stunned = false;

    private float stunTime = 0;

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
}
