using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FVX1Spawner : MonoBehaviour
{
	public float leaders = 3;
	public float gapL = 10;
	public float gap = 10;
	public float followers = 1;
	public GameObject ssprefab;
	public GameObject warship;
	public GameObject expPrefab; //explosion prefab place on boid of missiles
	private Vector3 mshipsize;
	//Could create the list here and use at the AI script aswell

	private void Awake ()
	{
		warship = GameObject.Find ("Warship");
		mshipsize = warship.GetComponent<Collider> ().bounds.size;
		for (int i = 1; i <= leaders; i++) {
			Vector3 rndPos = new Vector3 (Random.Range ((-leaders * gapL), (leaders * gapL)), Random.Range ((-leaders * gapL), (leaders * gapL)), 0);
		}

		//Create Spaceship 
		GameObject leader = GameObject.Instantiate<GameObject> (ssprefab);
		leader.transform.position = this.transform.TransformPoint (new Vector3(0,-50,0));
		leader.transform.rotation = this.transform.rotation;

		//add components path,pathfollow,obsavoidance,
		//Path path = leader.AddComponent<Path> ();
		//path.isRandom = false;
		FollowPath fpath = leader.AddComponent<FollowPath> ();
		fpath.path = GameObject.Find("Spath").GetComponent<Path>();
		fpath.enabled = fpath.enabled;
		ObstacleAvoidance obavd = leader.AddComponent<ObstacleAvoidance> ();
		leader.GetComponent<Boid> ().maxSpeed = 30;


	}


	Vector3 RandomTarget () //missile launch random position of warship
	{
		return new Vector3 (warship.transform.position.x,
			warship.transform.position.y,
			Random.Range (-mshipsize.z / 2, mshipsize.z / 2) + warship.transform.position.z
		);
	}
	// Update is called once per frame
	void Update ()
	{

	}
}
