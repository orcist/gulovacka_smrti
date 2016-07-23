using UnityEngine;

public class MeerkatController : EnemyController {
  public float DodgeCooldown;
  public float DodgeSpeed;
  public float DodgeDistance;

  private Vector3 dodgeTarget;
  private bool canDodge = true;
  private bool isDodging = false;

	void FixedUpdate() {
    if (isDodging) {
      moveTowards(dodgeTarget);

      if ((dodgeTarget - transform.position).magnitude < 0.1) {
        Speed /= DodgeSpeed;
        isDodging = false;
        Invoke("resetDodge", DodgeCooldown);
      }
    } else {
      moveTowards(Targets[0].transform.position);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if ((onLayer(collision.gameObject, "ProjectileBy1") || onLayer(collision.gameObject, "ProjectileBy2")) && canDodge) {
      dodge(collision.transform.position);
    }
  }

  private void dodge(Vector3 projectilePosition) {
    float op = (Random.Range(0,2) % 2 == 0) ? 1 : -1;
    dodgeTarget = transform.position +
      Quaternion.AngleAxis(90 * op, -Vector3.forward) *
      (projectilePosition - transform.position).normalized
      * DodgeDistance;

    isDodging = true;
    canDodge = false;
    Speed *= DodgeSpeed;
  }

  private void resetDodge() {
    canDodge = true;
  }
}