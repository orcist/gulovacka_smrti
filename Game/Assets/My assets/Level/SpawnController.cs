using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnController : MonoBehaviour {
  public GameObject MouseObject, DogObject, MeerkatObject;
  public GameObject[] Players;
  public GameObject Cheese;
  public float RestTime;
  public float MinSpawnDelay, MaxSpawnDelay;
  public GameObject SpecialExit;

  private Transform[] spawns;
  private Dictionary<GameObject, int>[] waves;
  private int waveCount = 0;
  private bool spawning = false;

  void Start() {
    waves = new Dictionary<GameObject, int>[] {
      new Dictionary <GameObject, int> {
        {MouseObject, 2}, {DogObject, 1}, {MeerkatObject, 0}
      },
      new Dictionary <GameObject, int> {
        {MouseObject, 3}, {DogObject, 3}, {MeerkatObject, 1}
      },
      new Dictionary <GameObject, int> {
        {MouseObject, 5}, {DogObject, 6}, {MeerkatObject, 2}
      }
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

    GameObject ego = Instantiate(enemy) as GameObject;
    DogController dog = enemy.GetComponent<DogController>();

    if (dog == null) {
      ego.transform.position = spawns[Random.Range(0, spawns.Length)].transform.position;
    } else {
      dog.LevelExit = SpecialExit;
      ego.transform.position = SpecialExit.transform.position;
    }

    if (enemy.layer == LayerMask.NameToLayer("ObjectiveEnemy"))
      ego.GetComponent<EnemyController>().Targets = new GameObject[] { Cheese };
    else if (enemy.layer == LayerMask.NameToLayer("PlayerEnemy"))
      ego.GetComponent<EnemyController>().Targets = Players;

    spawning = false;
  }
}
