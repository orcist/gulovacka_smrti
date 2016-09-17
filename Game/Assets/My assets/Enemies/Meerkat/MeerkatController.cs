using UnityEngine;

public class MeerkatController : EnemyController {
  public float DodgeCooldown;
  public float DodgeSpeed;
  public float DodgeDistance;

  private Vector3 dodgeTarget;
  private bool canDodge = true;
  private bool dodging = false;

	void FixedUpdate() {
    if (dodging) {
      moveTowards(dodgeTarget);

      if ((dodgeTarget - transform.position).magnitude < 0.1f) {
        Speed /= DodgeSpeed;
        dodging = false;
        Invoke("resetDodge", DodgeCooldown);
      }
    } else {
      moveTowards(Targets[0].transform.position);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (onLayer(collision.gameObject, "ProjectileBy1") || onLayer(collision.gameObject, "ProjectileBy2")) {
      die();
    }
    dodging = false;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (
      (onLayer(collision.gameObject, "ProjectileBy1") ||
      onLayer(collision.gameObject, "ProjectileBy2"))
      && canDodge) {
      dodge(collision.transform.position);
    }
  }

  private void dodge(Vector3 projectilePosition) {
    float op = (Random.Range(0, 2) % 2 == 0) ? 1 : -1;
    dodgeTarget = transform.position +
      Quaternion.AngleAxis(90 * op, -Vector3.forward) *
      (projectilePosition - transform.position).normalized
      * DodgeDistance;

    dodging = true;
    canDodge = false;
    Speed *= DodgeSpeed;
  }

  private void resetDodge() {
    canDodge = true;
  }
}