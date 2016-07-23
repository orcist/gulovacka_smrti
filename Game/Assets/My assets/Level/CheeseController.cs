using UnityEngine;

public class CheeseController : MonoBehaviour {
  public int HitPoints;

  void OnCollisionEnter2D(Collision2D collision) {
    // player resurrection
  }

  void OnCollisionStay2D(Collision2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("ObjectiveEnemy")) {
      HitPoints--;
      if (HitPoints <= 0) die();
    }
  }

  private void die() {
    Destroy(gameObject);
  }
}
