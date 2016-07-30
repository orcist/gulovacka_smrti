using UnityEngine;

public class EnemyController : MonoBehaviour {
  public float Speed;
  public GameObject[] Targets;
  public ElementController ElementController;
  public float DropChance;
  public float RepelDistance;
  public float FreezeTime;

  protected Rigidbody2D rb;
  protected bool repeled = false;
  protected bool frozen = false;
  protected bool stunned = false;

  private Vector3 repelTarget;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate() { 
    if (Targets.Length == 0)
      Debug.LogError("Enemy without a target.");

    if (frozen || stunned) {
      moveTowards(transform.position);
    } else if (repeled) {
      handleRepel();
    } else {
      GameObject closest = Targets[0];
      foreach (GameObject target in Targets)
        if (distanceTo(target) < distanceTo(closest))
          closest = target;

      moveTowards(closest.transform.position);
    }
  }

  private void unfreeze() {
    frozen = false;
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (onLayer(collision.gameObject, "ProjectileBy1") || onLayer(collision.gameObject, "ProjectileBy2")) {
      die();
    } else if (collision.gameObject.tag == "PotWithSoup") {
      die();
    } else if (onLayer(collision.gameObject, "Wall")) {
      repeled = false;
    }
  }

  protected void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "HotPizzaSlice") {
      die();
    } else if (collision.gameObject.tag == "IceCream") {
      frozen = true;
      Invoke("unfreeze", FreezeTime);
    } else if (collision.gameObject.tag == "NachosPool") {
      stunned = true;
    } else if (collision.gameObject.tag == "SteamExplosion") {
      repeled = true;
      repelTarget = transform.position +
      Quaternion.AngleAxis(180, -Vector3.forward) *
      (collision.gameObject.transform.position - transform.position).normalized
      * RepelDistance;
    }
  }

  protected void OnTriggerExit2D(Collider2D collision) {
    if (collision.gameObject.tag == "NachosPool") {
      stunned = false;
    }
  }

  protected void handleRepel() {
    moveTowards(repelTarget);
    if ((repelTarget - transform.position).magnitude < 0.1f) {
      repeled = false;
    }
  }

  protected void die() {
    if (Random.Range(0f, 1f) < DropChance)
      ElementController.DropAmmo(transform.position);
    Destroy(gameObject);
  }

  protected void moveTowards(Vector3 target) {
    rb.velocity = new Vector2(
        target.x - transform.position.x,
        target.y - transform.position.y
      ).normalized * Speed;
  }

  protected float distanceTo(GameObject other) {
    return (other.transform.position - transform.position).magnitude;
  }

  protected bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }

  public bool isFrozen() {
    return frozen;
  }
}