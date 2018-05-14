using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScene : MonoBehaviour
{

	public GameObject fvx1prefab;
	public GameObject enemyprefab;
	public GameObject expPrefab;
	private List<Path> paths = new List<Path> ();
	private List<GameObject> eships = new List<GameObject> ();
	private List<GameObject> fvx1ships = new List<GameObject> ();
	// Use this for initialization
	void Start ()
	{
		paths.Clear ();
		eships.Clear ();
		fvx1ships.Clear ();
		foreach (Path p in gameObject.GetComponentsInChildren(typeof(Path))) {
			paths.Add (p);
		}


		//Creates the enemy to get shot by
		GameObject leader = GameObject.Instantiate<GameObject> (enemyprefab);
		leader.transform.parent = this.transform;
		leader.transform.position = this.transform.GetChild (1).position; //position at the start of the path
		leader.transform.rotation = this.transform.GetChild (1).rotation; 
		FollowPath fpath = leader.AddComponent<FollowPath> ();
		fpath.path = paths [0]; //follows the path
		fpath.enabled = fpath.enabled;
		eships.Add (leader);

		//Creates the enemy that will shoot the fvx1 fighter plane
		leader = GameObject.Instantiate<GameObject> (enemyprefab);
		leader.transform.parent = this.transform;
		leader.transform.position = this.transform.GetChild (0).TransformPoint (new Vector3 (0, 0, -10)); //position at the start of the apath with offset
		leader.transform.rotation = this.transform.GetChild (0).rotation;
		eships.Add (leader);

        //Creates the fvx1 fighter plane that will shoot the enemy
		leader = GameObject.Instantiate<GameObject> (fvx1prefab);
		leader.transform.parent = this.transform;
		leader.transform.position = this.transform.position;
		leader.transform.rotation = this.transform.rotation;

		fvx1ships.Add (leader);
	}


	public IEnumerator Enemykill ()
	{
		yield return new WaitForSeconds (2);
		//first plane shoot enemy
		fvx1ships [0].transform.Find ("Bombs").GetChild (5).gameObject.GetComponent<Pursue> ().enabled = enabled;
		yield return new WaitForSeconds (1);
		fvx1ships [0].transform.Find ("Bombs").GetChild (5).gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		StartCoroutine (Av8Kill ());
		yield break;
	}

	public IEnumerator Av8Kill ()//first plane shoot enemy
	{
		yield return new WaitForSeconds (1);
		fvx1ships [0].GetComponent<FollowPath> ().enabled = enabled;
		yield return new WaitForSeconds (1.7f);
		eships [1].GetComponent<FireBullets> ().Fire();
		yield break;
	}

	void Update(){
		if (fvx1ships [0] != null) {
			eships [1].transform.LookAt (fvx1ships [0].transform);
		}
	}
}
