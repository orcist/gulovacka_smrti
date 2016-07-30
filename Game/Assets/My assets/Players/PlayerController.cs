using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public int PlayerNumber;
	public float Speed;
	public GameObject Projectile;
	public float ProjectileSize;
	public float ManaRegenRate;
	public float FirePower;
	public RectTransform ManaBar, PowerFill;
	public RawImage ElementIcon;
  public bool Dead = false;
	public float DeadSpeed;
	public CheeseController Cheese;
	public int RessurectionDamage;

  private Rigidbody2D rb;
	private ElementController elementController;
	private float mana = 0, power = 0;
	private Vector2 direction;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		elementController = transform.parent.GetComponent<ElementController>();
	}
	void FixedUpdate() {
		move();
		regenMana();
		handleFiring();
	}
	void Update() {
		ManaBar.localScale = new Vector3(
				Mathf.Lerp(ManaBar.localScale.x, mana, Time.deltaTime * 5),
				ManaBar.localScale.y,
				ManaBar.localScale.z
			);
		PowerFill.localScale = new Vector3(
				Mathf.Lerp(PowerFill.localScale.x, power, Time.deltaTime * 5),
				PowerFill.localScale.y,
				PowerFill.localScale.z
			);
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.gameObject.layer == LayerMask.NameToLayer("Ammo")) {
			Projectile = elementController.GetProjectile(c.gameObject);
			ElementIcon.texture = elementController.GetIcon(c.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D c) {
		if (
				Dead &&
				c.gameObject == Cheese.gameObject &&
				Cheese.HitPoints - Mathf.Abs(RessurectionDamage) > 0
			) {
			Cheese.HitPoints -= Mathf.Abs(RessurectionDamage);
			Dead = false;
			Speed /= DeadSpeed;
		}
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (c.gameObject.layer != LayerMask.NameToLayer("PlayerEnemy"))
			return;

		Dead = true;
		Speed *= DeadSpeed;
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

	private void regenMana() {
		if (PlayerNumber == 1) {
			if (rb.velocity.magnitude > 0)
				mana = Mathf.Min(mana + (ManaRegenRate * rb.velocity.magnitude), 1f);
		} else
			mana = Mathf.Min(mana + ManaRegenRate, 1f);
	}

	private void handleFiring() {
		if (Dead) {
			mana = 0;
			power = 0;
			return;
		}

		if (Input.GetButton("Fire"+PlayerNumber)) {
			power = Mathf.Min(power + 0.025f, mana);
		} else if (power > 0f) {
			GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
      projectile.GetComponent<ProjectileController>().ElementController = elementController;
			projectile.layer = LayerMask.NameToLayer("ProjectileBy"+PlayerNumber);
			projectile.transform.localScale *= ProjectileSize;
			projectile.GetComponent<Rigidbody2D>().AddForce(
					rb.velocity + direction * power * FirePower
				);

			mana-= power;
			power = 0f;
		}
	}
}
