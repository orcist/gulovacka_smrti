using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float Speed;
    public GameObject[] Targets;

    private Rigidbody2D rb;
	private Vector2 direction;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
        move(Targets[0].transform.position);
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("ProjectileBy1") || collision.collider.gameObject.layer == LayerMask.NameToLayer("ProjectileBy2")) {
            die();
        }
    }

    private void move(Vector3 target) {
        Vector2 velocity = new Vector2(
                target.x - transform.position.x,
                target.y - transform.position.y
            ) * Speed;
        rb.velocity = velocity;
    }

    private void die() {
        Destroy(gameObject);
    }
}