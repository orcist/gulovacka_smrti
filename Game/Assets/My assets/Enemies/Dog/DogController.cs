using UnityEngine;

public class DogController : EnemyController {
  public GameObject LevelExit;

  void FixedUpdate() {
    GameObject closest = null;

    if (frozen || stunned) {
      moveTowards(transform.position);
    } else if (repeled) {
      handleRepel();
    } else {
      foreach (GameObject target in Targets) {
        if (onLayer(target, "Player") && target.GetComponent<PlayerController>().Dead)
          continue;

        if (closest == null || distanceTo(target) < distanceTo(closest))
          closest = target;
      }
      if (closest == null) {
        closest = LevelExit;
        if (distanceTo(closest) < 0.1f)
          Destroy(gameObject);
      }

      moveTowards(closest.transform.position);
    }   
  }
}