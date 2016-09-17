using UnityEngine;

public class PlayerController : MonoBehaviour {
	public int PlayerNumber;
	public float Speed;
	public GameObject ProjectileObject;
	public float FireCooldown;
	public float FirePower;
	public float MaxAmmo;
	public RectTransform PowerFill;

  private Rigidbody2D rb;
	private float power = 0;
	private int ammo = 0;
	private bool reloading = false;
	private Vector2 direction;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		move();
		handleReloading();
		handleFiring();
	}

	void Update() {
		PowerFill.localScale = new Vector3(
				Mathf.Lerp(PowerFill.localScale.x, power, Time.deltaTime * 5),
				PowerFill.localScale.y,
				PowerFill.localScale.z
			);
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (c.gameObject.layer != LayerMask.NameToLayer("PlayerEnemy"))
			return;

		ammo = 0;
	}

	private void move() {
		Vector2 velocity = new Vector2(
				Input.GetAxis("Horizontal"+PlayerNumber),
				-Input.GetAxis("Vertical"+PlayerNumber)
			) * Speed;

		rb.velocity = velocity;

		if (velocity.magnitude > 0)
			direction = velocity.normalized;
	}

	private void handleReloading() {
		// handle reloading
	}

	private void handleFiring() {
		if (reloading) {
			return;
		}

		if (Input.GetButton("Fire"+PlayerNumber)) {
			power = Mathf.Min(power + 0.025f, 1f);
		} else if (power > 0f) {
			GameObject projectile = Instantiate(ProjectileObject, transform.position, Quaternion.identity) as GameObject;
			projectile.layer = LayerMask.NameToLayer("ProjectileBy"+PlayerNumber);
			projectile.GetComponent<Rigidbody2D>().AddForce(
				rb.velocity + direction * power * FirePower
			);

			power = 0f;
			ammo--;

			if (ammo == 0)
				reloading = true;
		}
	}
}
