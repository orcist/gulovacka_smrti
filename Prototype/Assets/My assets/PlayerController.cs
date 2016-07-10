﻿using UnityEngine;

public class PlayerController : MonoBehaviour {
	public int PlayerNumber;
	public float Speed;
	public GameObject ProjectileObject;
	public float ProjectileSize;
	public float FirePower;
	public RectTransform Powerfill;

	private Rigidbody2D rb;
	private float power;
	private Vector2 direction;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		power = 0;
	}
	void FixedUpdate() {
		Vector2 velocity = new Vector2(
				Input.GetAxis("Horizontal"+PlayerNumber),
				-Input.GetAxis("Vertical"+PlayerNumber)
			) * Speed;

		if (velocity.magnitude > 0)
			direction = velocity.normalized;
		else
			rb.velocity = velocity;

		if (Input.GetButton("Fire"+PlayerNumber)) {
			power = Mathf.Min(power + 0.025f, 1f);
			return;
		} else if (power > 0f) {
			GameObject projectile = Instantiate(ProjectileObject) as GameObject;
			projectile.transform.position = transform.position;
			projectile.transform.localScale *= ProjectileSize;
			projectile.GetComponent<Rigidbody2D>().AddForce(
					direction * power * FirePower
				);
			power = 0f;
		}

		rb.velocity = velocity;
	}
	void Update() {
		Powerfill.localScale = new Vector3(
				Mathf.Lerp(Powerfill.localScale.x, power, Time.deltaTime * 5),
				Powerfill.localScale.y,
				Powerfill.localScale.z
			);
	}
}