using UnityEngine;

public class AmmoController : MonoBehaviour {
	public float LifetimeSeconds;
	void Start() {
		Invoke("die", LifetimeSeconds);
	}

	void OnTriggerEnter2D(Collider2D c) {
		die();
	}

	private void die() {
		Destroy(gameObject);
	}
}
