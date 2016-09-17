using UnityEngine;

public class ProjectileController : MonoBehaviour {
  public float LifetimeSeconds;
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

    die();
		if (collision.gameObject.layer == LayerMask.NameToLayer("ObjectiveEnemy") ||
      collision.gameObject.layer == LayerMask.NameToLayer("PlayerEnemy")) {
      return;
    } else if (collision.gameObject.layer == LayerMask.NameToLayer("Powerup")){
      triggerPowerup(collision.gameObject); // actually code this.
    } else { // projectile collision
      collision.gameObject.GetComponent<ProjectileController>().Destroyed = true;
      createPowerup();
      Destroy(collision.gameObject);
    }
	}

  private void createPowerup() {
    // create powerup
  }

  private void triggerPowerup(GameObject GO) {
    // trigger powerup that is GO
  }

  private void die() {
		Destroy(gameObject);
	}
}
