using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnController : MonoBehaviour {
  public GameObject EnemyObject; // todo add more enemies
  public GameObject[] Players;
  public GameObject Goal;
  public float RestTime;
  public float MinSpawnDelay, MaxSpawnDelay;

  private Transform[] spawns;
  private Dictionary<GameObject, int>[] waves;
  private int waveCount = 0;
  private bool spawning = false;

  void Start() {
    waves = new Dictionary<GameObject, int>[] {
      new Dictionary <GameObject, int> {
        {EnemyObject, 3},
      },
    };
    spawns = GetComponentsInChildren<Transform>().Where(
      s => { return s != transform; }
    ).ToArray();
  }

  void FixedUpdate() {
    if (!spawning && GameObject.FindGameObjectsWithTag("enemy").Length == 0) {
      Invoke("spawnWave", RestTime);
      spawning = true;
    }
  }

  private void spawnWave() {
    if (waveCount >= waves.Length) {
      Debug.LogError("No more waves."); // @todo replace this with switching scene
      return; // Victoryyyy!
    }

    foreach (GameObject enemy in waves[waveCount].Keys)
      for (int i = 0; i < waves[waveCount][enemy]; i++)
        StartCoroutine(spawnEnemy(enemy));

    waveCount++;
  }

  IEnumerator<WaitForSeconds> spawnEnemy(GameObject enemy) {
    yield return new WaitForSeconds(
      Random.Range(MinSpawnDelay, MaxSpawnDelay)
    );

    GameObject enemyGO = Instantiate(enemy) as GameObject;
    EnemyController enemyGOController = enemyGO.GetComponent<EnemyController>();

    enemyGO.transform.position = spawns[Random.Range(0, spawns.Length)].transform.position;

    if (enemy.layer == LayerMask.NameToLayer("ObjectiveEnemy"))
      enemyGOController.Targets = new GameObject[] { Goal };
    else if (enemy.layer == LayerMask.NameToLayer("PlayerEnemy"))
      enemyGOController.Targets = Players;

    spawning = false;
  }
}
