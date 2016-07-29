﻿using UnityEngine;

public class PlayerController : MonoBehaviour {
	public int PlayerNumber;
	public float Speed;
	public GameObject ElementProjectile;
	public float ProjectileSize;
	public float ManaRegenRate;
	public float FirePower;
	public RectTransform ManaBar, PowerFill;
  public bool Dead = false;

  private Rigidbody2D rb;
	private float mana = 0, power = 0;
	private Vector2 direction;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
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
		if (Input.GetButton("Fire"+PlayerNumber)) {
			power = Mathf.Min(power + 0.025f, mana);
		} else if (power > 0f) {
			GameObject projectile = Instantiate(ElementProjectile, transform.position, Quaternion.identity) as GameObject;
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
