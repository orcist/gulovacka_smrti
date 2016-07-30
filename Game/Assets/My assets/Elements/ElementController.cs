using UnityEngine;
using System.Collections.Generic;

public class ElementController : MonoBehaviour {
	public PlayerController Player1, Player2;
	public GameObject AirProjectile, AirAmmo;
	public GameObject EarthProjectile, EarthAmmo;
	public GameObject FireProjectile, FireAmmo;
	public GameObject WaterProjectile, WaterAmmo;

	private Dictionary<GameObject, GameObject> ammoMapping;

	void Start () {
		ammoMapping = new Dictionary<GameObject, GameObject>() {
			{AirProjectile, AirAmmo}, {EarthProjectile, EarthAmmo},
			{FireProjectile, FireAmmo}, {WaterProjectile, WaterAmmo},
		};
	}

	public void DropAmmo(Vector3 position) {
		List<GameObject> availableAmmo = new List<GameObject>();
		foreach (GameObject p in ammoMapping.Keys) {
			if (p != Player1.ElementProjectile && p != Player2.ElementProjectile)
				availableAmmo.Add(ammoMapping[p]);
		}

		Instantiate(availableAmmo[Random.Range(0, availableAmmo.Count)], position, Quaternion.identity);
	}
}
