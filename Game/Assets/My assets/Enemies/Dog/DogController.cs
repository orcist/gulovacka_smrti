using UnityEngine;

public class DogController : EnemyController {
  public GameObject LevelExit;

  void FixedUpdate() {
    GameObject closest = Targets[0];

    bool hasTarget = false;
    foreach (GameObject target in Targets) {
      if (onLayer(target, "Player") && target.GetComponent<PlayerController>().Dead)
        continue;

      hasTarget = true;
      if (distanceTo(target) < distanceTo(closest))
        closest = target;
    }
    if (!hasTarget) closest = LevelExit;

    moveTowards(closest.transform.position);
  }
}