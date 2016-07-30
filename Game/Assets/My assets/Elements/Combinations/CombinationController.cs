using UnityEngine;

public class CombinationController : MonoBehaviour {
  public float LifetimeSeconds;

  void Start () {
		Invoke("prepareToDie", LifetimeSeconds);
	}

  private void prepareToDie() {
    // hack for function onTriggerExit2D()
    gameObject.transform.position = new Vector3(1000, 1000, 1000);
    Invoke("die", 1.0f);
  }

  private void die() {
		Destroy(gameObject);
	}
}
