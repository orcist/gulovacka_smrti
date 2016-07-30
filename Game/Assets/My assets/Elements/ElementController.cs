using UnityEngine;
using System.Collections.Generic;

public class ElementController : MonoBehaviour {
	public PlayerController Player1, Player2;
	public GameObject AirProjectile, AirAmmo;
	public Texture AirIcon;
	public GameObject EarthProjectile, EarthAmmo;
	public Texture EarthIcon;
	public GameObject FireProjectile, FireAmmo;
	public Texture FireIcon;
	public GameObject WaterProjectile, WaterAmmo;
	public Texture WaterIcon;

  public GameObject SteamExplosion;
  public GameObject HotPizzaSlice;
  public GameObject PotWithSoup;
  public GameObject IceCream;
  public GameObject NachosPool;
  public GameObject FrenchFries;

  private Dictionary<GameObject, GameObject> ammoMapping;
	private Dictionary<string, Texture> iconMapping;
  private Dictionary<string, GameObject> combinationMapping;

  void Start () {
		ammoMapping = new Dictionary<GameObject, GameObject>() {
			{AirProjectile, AirAmmo}, {EarthProjectile, EarthAmmo},
			{FireProjectile, FireAmmo}, {WaterProjectile, WaterAmmo},
		};
		iconMapping = new Dictionary<string, Texture>() {
			{"AirAmmo", AirIcon}, {"EarthAmmo", EarthIcon},
			{"FireAmmo", FireIcon}, {"WaterAmmo", WaterIcon},
		};
    combinationMapping = new Dictionary<string, GameObject>() {
      {"AF", SteamExplosion}, {"FE", HotPizzaSlice},
      {"FW", PotWithSoup}, {"WA", IceCream},
      {"WE", NachosPool}, {"EA", FrenchFries},
    };
  }

	public void DropAmmo(Vector3 position) {
		List<GameObject> availableAmmo = new List<GameObject>();
		foreach (GameObject p in ammoMapping.Keys) {
			if (p != Player1.Projectile && p != Player2.Projectile)
				availableAmmo.Add(ammoMapping[p]);
		}

		Instantiate(availableAmmo[Random.Range(0, availableAmmo.Count)], position, Quaternion.identity);
	}

	public GameObject GetProjectile(GameObject ammoObject) {
		foreach (GameObject p in ammoMapping.Keys)
			if (ammoMapping[p].tag == ammoObject.tag)
				return p;

		Debug.LogError("No such ammo object found.");
		return null;
	}

	public Texture GetIcon(GameObject ammoObject) {
		return iconMapping[ammoObject.tag];
	}

  public GameObject GetCombination(string tagA, string tagB) {
    if (combinationMapping.ContainsKey(tagA[0] + "" + tagB[0])) {
      return combinationMapping[tagA[0]+ "" + tagB[0]];
    } else {
      return null;
    }
  }
}

