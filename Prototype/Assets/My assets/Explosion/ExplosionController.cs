using UnityEngine;

public class ExplosionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("die", 5);
	}

	private void die() {
		Destroy(gameObject);
	}
}
