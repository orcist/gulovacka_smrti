using UnityEngine;

public class DogController : EnemyController {
  public GameObject Exit;

  void FixedUpdate() {
    GameObject closest = Targets[0];

    bool hasTarget = false;
    foreach (GameObject target in Targets) {
      if (target.GetComponent<PlayerController>().Dead) 
        continue;
      else 
       // hasTarget = true;

      if (distanceTo(target) < distanceTo(closest))
        closest = target;
    }
    if (!hasTarget) closest = Exit;

    moveTowards(closest.transform.position);
  }
}