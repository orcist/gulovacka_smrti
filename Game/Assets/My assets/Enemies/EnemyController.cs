using UnityEngine;

public class EnemyController : MonoBehaviour {
  public float Speed;
  public GameObject[] Targets;

  private Rigidbody2D rb;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
  }
  void FixedUpdate() {
    if (Targets.Length == 0)
      Debug.LogError("Enemy without a target.");

    GameObject closest = Targets[0];
    foreach (GameObject target in Targets)
      if (distanceTo(target) < distanceTo(closest))
        closest = target;

    moveTowards(closest.transform.position);
  }

  private void OnCollisionEnter2D(Collision2D collision) {
	  if (onLayer(collision.gameObject, "ProjectileBy1") || onLayer(collision.gameObject, "ProjectileBy2"))
      Destroy(gameObject);
  }

  private void moveTowards(Vector3 target) {
    rb.velocity = new Vector2(
        target.x - transform.position.x,
        target.y - transform.position.y
      ).normalized * Speed;
  }
  private float distanceTo(GameObject other) {
    return (other.transform.position - transform.position).magnitude;
  }
  private bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }
}