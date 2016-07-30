using UnityEngine;

public class ProjectileController : MonoBehaviour {
	public float LifetimeSeconds;
	public GameObject CombinationObject;
	public bool Destroyed;

	private Rigidbody2D rb;
	private bool dying;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		Destroyed = false;
		dying = false;
	}
	void FixedUpdate() {
		if (!dying && rb.velocity.magnitude == 0f) {
			Invoke("die", LifetimeSeconds);
			dying = true;
		}
	}
	void OnCollisionEnter2D(Collision2D collision) {
    if (Destroyed)
			return;
		Destroy(gameObject);

		if (collision.gameObject.layer == LayerMask.NameToLayer("ObjectiveEnemy") ||
      collision.gameObject.layer == LayerMask.NameToLayer("PlayerEnemy"))
      return;

		collision.gameObject.GetComponent<ProjectileController>().Destroyed = true;
		GameObject combination = Instantiate(CombinationObject) as GameObject;
		combination.transform.position = transform.position;

		Destroy(collision.gameObject);
	}

	private void die() {
		Destroy(gameObject);
	}
}
