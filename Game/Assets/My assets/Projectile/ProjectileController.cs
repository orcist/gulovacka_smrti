using UnityEngine;

public class ProjectileController : MonoBehaviour {
	public float LifetimeSeconds;
	public GameObject CombinationObject;
	public bool Destroyed;
    public int type;

	private Rigidbody2D rb;
	private bool counting;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		counting = false;
		Destroyed = false;
	}
	void FixedUpdate() {
		if (!counting && rb.velocity.magnitude == 0f) {
			Invoke("die", LifetimeSeconds);
			counting = true;
		}
	}
	void OnCollisionEnter2D(Collision2D collision) {
        if (Destroyed)
			return;

		collision.gameObject.GetComponent<ProjectileController>().Destroyed = true;

		GameObject combination = Instantiate(CombinationObject) as GameObject;
		combination.transform.position = transform.position;

		Destroy(collision.gameObject);
		Destroy(gameObject);
	}

	private void die() {
		Destroy(gameObject);
	}
}
