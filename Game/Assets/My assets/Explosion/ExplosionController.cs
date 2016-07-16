using UnityEngine;

public class ExplosionController : MonoBehaviour {

	void Start () {
		Invoke("die", 0.5f);
	}

	private void die() {
		Destroy(gameObject);
	}
}
