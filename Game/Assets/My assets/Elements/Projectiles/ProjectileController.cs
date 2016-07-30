using UnityEngine;

public class ProjectileController : MonoBehaviour {
  public float LifetimeSeconds;
	public bool Destroyed;
  public ElementController ElementController;

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

		if (collision.gameObject.layer == LayerMask.NameToLayer("ObjectiveEnemy") ||
      collision.gameObject.layer == LayerMask.NameToLayer("PlayerEnemy")) {
      if (!collision.gameObject.GetComponent<EnemyController>().isFrozen()) {
        die();
      }
    } else {
      die();
      collision.gameObject.GetComponent<ProjectileController>().Destroyed = true;
      createCombination(gameObject, collision.gameObject);
      Destroy(collision.gameObject);
    }
	}

  private void createCombination(GameObject projectileA, GameObject projectileB) {
    GameObject combinationObject = ElementController.GetCombination(projectileA.tag, projectileB.tag);

    if (combinationObject != null) { 
      GameObject combination = Instantiate(combinationObject) as GameObject;
      combination.transform.position = projectileA.transform.position;
    }
  }

  private void die() {
		Destroy(gameObject);
	}
}
